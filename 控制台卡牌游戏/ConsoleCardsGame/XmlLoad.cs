using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ConsoleCardsGame
{
    internal class XmlLoad
    {
        private readonly string def = @"\DataSave";
        string DefxmlPaths = null;
        string XmlPaths = null;
        readonly string exePath = Application.StartupPath;
        public static string unitSavePath;
        List<XDocument> XmlFileGroup = new List<XDocument>();
        List<string> moreXmlPath = new List<string>();
        public XmlLoad()
        {
            XmlPaths = @exePath+@def;
        }
        public XmlLoad(string xmlPath)
        {
            if (xmlPath == null)
            {
                xmlPath = exePath + def;
                DefxmlPaths = xmlPath;
            }
            XmlPaths = xmlPath;
            DefxmlPaths = exePath + xmlPath;
        }
        public XmlLoad(List<string> moreXmlPath = null)
        {
            this.moreXmlPath = moreXmlPath;
            this.moreXmlPath.Insert(0, @exePath + @def);
        }
        /// <summary>
        /// 获取指定目录的文件夹内所有xml文档
        /// </summary>
        public void GetFolder()
        {
            if (moreXmlPath != null) 
            {
                for (int i = 0; i < moreXmlPath.Count; ++i)
                {
                    unitSavePath = moreXmlPath[i];
                    IEnumerable<string> files = Directory.GetFiles(unitSavePath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".xml"));
                    for (int b = 0; b < files.Count(); ++b)
                    {
                        string filePaths = files.ElementAt(b);
                        XmlPaths = @filePaths;
                        XDocument xmlDoc = XDocument.Load(XmlPaths);
                        XmlFileGroup.Add(xmlDoc);
                    }
                }
            }
            unitSavePath = @XmlPaths;//加@意味着强制不转译
            //var files = Directory.GetFiles(unitSavePath, "*.xml", SearchOption.AllDirectories);
            IEnumerable<string> file = Directory.GetFiles(unitSavePath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".xml"));
            /*通配符 * 零或多个字符 ? 正好一个字符 *t搜索路径中所有以字母t结尾的名称 s*搜索路径中所有以字母s开头的名称
            恰好为三字符返回三或三字符以上的文件 小于三或大于三字符的只返回扩展名恰好相等的文件*/
            for (int i = 0; i < file.Count(); ++i)
            {
                string filePaths = file.ElementAt(i);
                XmlPaths = @filePaths;
                XDocument xmlDoc = XDocument.Load(XmlPaths);
                XmlFileGroup.Add(xmlDoc);
                /*IEnumerable<XElement> address = from el in xmlDoc.Elements("List")where (string)el.Attribute("type") == "List"select el;
                //if (address.ToString() == "武器")
                //{
                //    LoadWeapons();
                //}
                //else
                //{
                //    LoadUnit();
                //}*/
            }
        }

        public CN Create(XAttribute id)
        {
            int save = 0;
            for(int l = 0; l < XmlFileGroup.Count; ++l)
            {
                if(XmlFileGroup[l].Root.XPathSelectElement($".//Unit[@id='{(string)id}']") != null)
                {
                    save = l; break;
                }
            }
            CN cN = new CN();
            XElement root = XmlFileGroup[save].Root;
            cN.Name = id.Value;
            XAttribute hp = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("Hp").Attributes("value").First();
            cN.Hp = float.Parse(hp.Value);
            XAttribute maxHp = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("MaxHp").Attributes("value").First();
            cN.MaxHp = float.Parse(maxHp.Value);
            XAttribute armor = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("Armor").Attributes("value").First();
            cN.Armor = (short)armor;
            if (root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("ArmorReducesDamage").Attributes("value").First() != null)
            {
                XAttribute armorReducesDamage = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("ArmorReducesDamage").Attributes("value").First();
                cN.ArmorReducesDamage = (short)armorReducesDamage;
            }
            if (root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("WeaponsArrays").Attributes("Link").First() != null)
            {
                XAttribute weapLink = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("WeaponsArrays").Attributes("Link").First();
                for (int l = 0; l < XmlFileGroup.Count; ++l)
                {
                    if (XmlFileGroup[l].Root.XPathSelectElement($".//Weapons[@id='{(string)weapLink}']") != null)
                    {
                        save = l; break;
                    }
                }
                XElement rw = XmlFileGroup[save].Root;
                StDefaultWeaponFrame we = new StDefaultWeaponFrame();
                we.Name = weapLink.Value;
                XAttribute damage = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("Damage").Attributes("value").First();
                we.Damage = float.Parse(damage.Value);
                if (rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("WeaponTargetingLimitations").Attributes("value").First() != null)
                {
                    we.WeaponTargetingLimitationsU = new List<WeaponTargetingLimitations>();
                    for (int i = 0; i < rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("WeaponTargetingLimitations").Count(); ++i)
                    {
                        XAttribute weaponTargetingLimitationsbl = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("WeaponTargetingLimitations").Attributes("value").First();
                        we.WeaponTargetingLimitationsUF(weaponTargetingLimitationsbl);
                    }
                }
                if (rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("ArmorDamageBuff").Attributes("index").First() != null)
                {
                    we.ArmorDamageBuff = new Dictionary<ArmorType, float>();
                    for (int i = 0; i < rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("ArmorDamageBuff").Count(); ++i)
                    {
                        XAttribute armorBuffObject = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("ArmorDamageBuff").ElementAt(i).Attribute("index");
                        XAttribute armorDamageBuff = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("ArmorDamageBuff").ElementAt(i).Attribute("value");
                        we.ArmorDamageBuffUF(armorBuffObject,armorDamageBuff);
                    }
                }
                if (rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("SpecialDamageBuff").Attributes("value").First() != null)
                {
                    we.SpecialDamageBuffU = new Dictionary<SpecialAttributes, float>();
                    for (int i = 0; i < rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("SpecialDamageBuff").Count(); ++i)
                    {
                        XAttribute SpecialBuffObject = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("SpecialDamageBuff").ElementAt(i).Attribute("index");
                        XAttribute SpecialDamageBuff = rw.XPathSelectElements($".//Weapons[@id='{(string)weapLink}']").Elements("SpecialDamageBuff").ElementAt(i).Attribute("value");
                        we.SpecialDamageBuffUF(SpecialBuffObject, SpecialDamageBuff);
                    }
                }
                cN.StDefaultWeaponFrame = we;
            }
            XAttribute armorType = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("ArmorType").Attributes("index").First();
            cN.ArmorTypeU = (ArmorType)Enum.Parse(typeof(ArmorType), (string)armorType);
            XAttribute actionPoints = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("ActionPoints").Attributes("value").First();
            cN.ActionPoints = (int)actionPoints;
            XAttribute planarArrays = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("PlanarArrays").Attributes("index").First();
            cN.PlanarArraysU = (PlanarArrays)Enum.Parse(typeof(PlanarArrays), (string)planarArrays);
            XAttribute specialAttributes = root.XPathSelectElements($".//Unit[@id='{(string)id}']").Elements("SpecialAttributes").Attributes("index").First();
            cN.SpecialAttributesU = (SpecialAttributes)Enum.Parse(typeof(SpecialAttributes), (string)specialAttributes);
            return cN;
        }

        public void AddToCardPool()
        {
            List<XDocument> cp = XmlFileGroup.Where(x => x.Elements("Unit") != null).ToList();
            for (int i = 0; i < cp.Count; ++i)
            {
                XElement root = cp[i].Root;
                IEnumerable<XElement> xecp = root.Elements("Unit");
                for (int j = 0; j < xecp.Count(); ++j)
                {
                    XElement fc = xecp.ElementAt(j);
                    XAttribute byd = fc.Attribute("id");
                    CN cn = new CN();
                    cn = Create(byd);
                    GameRuleAndRumtimeAction.CardsPools.Add(cn);
                }
            }
        }
        /*public void IsSelectNode()
        {
            /*string node = "Weapons";
            //using (XmlReader reader = XmlReader.Create(xmlPath))
            //{
            //    reader.Read();
            //    int i = 0;
            //    if(i>=3)
            //    {
            //        for (; i <= 3; ++i)
            //        {
            //            if (reader.Name == node)
            //                LoadWeapons(xmlPath);
            //            else LoadUnit();
            //        }
            //    }else LoadUnit(xmlPath);
            //}
        }*/
        /// <summary>
        /// 读取xml文档标签的值并实例化将其添加到卡池中
        /// </summary>
        /*
        public void LoadUnit(string xmlPath)
        {
            XDocument xmlDoc = XDocument.Load(xmlPath);
            XElement root = xmlDoc.Root;
            XAttribute ele = root.Element("Name").Attribute("value");
            XAttribute hp = root.Element("Hp").Attribute("value");
            XAttribute maxHp = root.Element("Maxhp").Attribute("value");
            XAttribute weapLink = root.Element("WeaponsArrays").Attribute("Link");
            if (weapLink != null)
            {

            }
            XAttribute armorType = root.Element("ArmorType").Attribute("index");
            CN cN = new CN()
            {
                Name = (string)ele,
            };

            /*XDocument xmlDoc = new XDocument();
            XElement root = xmlDoc.Root;
            XElement ele = root.Element("Unit");
            //获取指定标签的值
            XElement name = ele.Element("Unit");
            XElement hp = ele.Element("hp");
            XElement atk = ele.Element("atk");
            XElement def = ele.Element("def");
            XElement actionPoints = ele.Element("ActionPoints");
            CN cN = new CN()
            {
                Name = (string)name,
                Hp = (float)hp,
                Atk = (float)atk,
                Def = (float)def,
                ActionPoints = (int)actionPoints
            };
            GameRuleAndRumtimeAction.CardsPools.Add(cN);
        }
        public void LoadWeapons(string xmlPath)
        {

        }*/
    }
}