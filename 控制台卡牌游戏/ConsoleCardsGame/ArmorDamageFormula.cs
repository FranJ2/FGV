using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCardsGame
{
    internal static class ArmorDamageFormula
    {
        public static float jg = 0f;
        public static Dictionary<ArmorType, float> hj = null;
        public static Dictionary<SpecialAttributes, float> ts = null;
        public static ArmorType hj2 = ArmorType.无;
        public static SpecialAttributes ts2 = SpecialAttributes.无;
        public static float a,b,c,d=0f;
        public static ArmorType sav = ArmorType.无;
        public static SpecialAttributes sav2 = SpecialAttributes.无;
        public static (float,float) ArmorDamageCompute(CN cN,CN cN2)
        {
            hj = cN.StDefaultWeaponFrame.GetArmorDamageBuffUF();
            ts = cN.StDefaultWeaponFrame.GetSpecialDamageBuffUF();
            hj2 = cN2.ArmorTypeU;
            ts2 = cN2.SpecialAttributesU;
            jg = cN.StDefaultWeaponFrame.Damage;
            try
            {
                sav = hj.Keys.Where(x => x == hj2).Max();
                a = hj[sav];
                sav2 = ts.Keys.Where(x => x == ts2).Max();
                b = ts[sav2];
            }
            catch (InvalidOperationException io)
            {
                a = 0f; b = 0f;
            }
            c = jg + a + b - (cN2.Armor * cN2.ArmorReducesDamage);
            d = cN2.Hp + (cN2.Armor * cN2.ArmorReducesDamage) - c;
            return (c,d);
        }
    }
}