using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleCardsGame
{   //我认为单位设计有很大问题
    internal class CN
    {
        public string Name { get; set; }
        public int Owner { get; set; }//所有者
        public float Hp { get; set; }//生命值
        public float MaxHp { get; set; }
        public short Armor { get; set; }
        public short ArmorReducesDamage { get; set; }
        public StDefaultWeaponFrame StDefaultWeaponFrame { get; set; }//武器
        public ArmorType ArmorTypeU { get; set; }//护甲类型
        public PlanarArrays PlanarArraysU { get; set; }
        public SpecialAttributes SpecialAttributesU { get; set; }
        public int SurviveOrDied { get; set; }//存活状态
        public int ActionPoints { get; set; }//行动点
        //括号内是可选参数 没有指定的数据传入时 指定的参数会被赋值成指定的可选参数的值，仅在类实例化的时候执行一次
        public CN(string name = "Unknown", int owner = 0, float hp = 15, float maxhp = 15, short armor = 0 , short armorReducesDamage = 1, ArmorType armorTypeU = ArmorType.无, PlanarArrays planarArraysU = PlanarArrays.地面单位,
            SpecialAttributes specialAttributesU = SpecialAttributes.无, int surviveOrDied = 1, int actionPoints = 1, StDefaultWeaponFrame stDefaultWeaponFrame = new StDefaultWeaponFrame())
        {
            //前缀this代表是类的变量 不是传入的参数
            this.Name = name;
            this.Owner = owner;
            this.Hp = hp;
            this.MaxHp = maxhp;
            this.Armor = armor;
            this.ArmorReducesDamage = armorReducesDamage;
            this.ArmorTypeU = armorTypeU;
            this.PlanarArraysU = planarArraysU;
            this.SpecialAttributesU = specialAttributesU;
            this.SurviveOrDied = surviveOrDied;
            this.ActionPoints = actionPoints;
            this.StDefaultWeaponFrame = stDefaultWeaponFrame;
        }
        public CN(List<CN> cardsPools, int i)
        {
            this.Name = cardsPools[i].Name;
            this.Hp = cardsPools[i].Hp;
            this.MaxHp = cardsPools[i].MaxHp;
            this.Armor = cardsPools[i].Armor;
            this.ArmorReducesDamage = cardsPools[i].ArmorReducesDamage;
            this.ArmorTypeU = cardsPools[i].ArmorTypeU;
            this.PlanarArraysU = cardsPools[i].PlanarArraysU;
            this.SpecialAttributesU = cardsPools[i].SpecialAttributesU;
            this.ActionPoints = cardsPools[i].ActionPoints;
            this.StDefaultWeaponFrame = cardsPools[i].StDefaultWeaponFrame;
        }
        //废案 最初单位是用结构体写的 后来改成类了
        //public struct CNS
        //{
        //    public int owner;
        //    public int unitClasses;
        //    public float hp;
        //    public float atk;
        //    public float def;
        //    public int surviveOrdied;
        //}
    }
}
