using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleCardsGame
{
    public struct StDefaultWeaponFrame
    {
        public string Name { get; set; }
        public float Damage { get; set; }
        public List<WeaponTargetingLimitations> WeaponTargetingLimitationsU { get; set; }
        public Dictionary<ArmorType,float> ArmorDamageBuff { get; set; }
        public Dictionary<SpecialAttributes,float> SpecialDamageBuffU { get; set; }
        public void WeaponTargetingLimitationsUF(XAttribute xAttribute)
        {
            WeaponTargetingLimitations weaponTargetingLimitations = (WeaponTargetingLimitations)Enum.Parse(typeof(WeaponTargetingLimitations),(string)xAttribute);
            WeaponTargetingLimitationsU.Add(weaponTargetingLimitations);
        }
        public int GetWeaponTargetingLimitationsUF() => WeaponTargetingLimitationsU.Count;
        public void ArmorDamageBuffUF(XAttribute xAttribute,XAttribute xAttribute1)
        {
            ArmorType armorType = (ArmorType)Enum.Parse(typeof(ArmorType), (string)xAttribute);
            float armorDamageBuffbl = (float)xAttribute1;
            ArmorDamageBuff.Add(armorType, armorDamageBuffbl);
        }
        public Dictionary<ArmorType, float> GetArmorDamageBuffUF() => ArmorDamageBuff;
        public void SpecialDamageBuffUF(XAttribute xAttribute,XAttribute xAttribute1)
        {
            SpecialAttributes specialAttributes = (SpecialAttributes)Enum.Parse(typeof(SpecialAttributes), (string)xAttribute);
            float specialDamageBuffbl = (float)xAttribute1;
            SpecialDamageBuffU.Add(specialAttributes,specialDamageBuffbl);
        }
        public Dictionary<SpecialAttributes, float> GetSpecialDamageBuffUF() => SpecialDamageBuffU;
    }
}
