using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCardsGame
{
    public enum CC//完全废弃
    {
        普通人,工程兵
        //TODO 废弃,步兵,坦克,反坦克步兵,机枪兵,轻型坦克,中型坦克,重型坦克,超重型坦克,牵引式火炮,牵引式反坦克炮,牵引式防空炮,牵引式火箭炮,战斗机,近距离支援机,战术轰炸机,战略轰炸机
    }
    //TODO 做完了
    public enum ArmorType
    {
        无,轻甲,中甲,重甲,轻装甲,中装甲,重装甲,建筑护甲
    }
    public enum PlanarArrays
    {
        地面单位,空中单位
    }
    public enum SpecialAttributes
    {
        无,建筑,生物,机械
    }
    public enum WeaponTargetingLimitations
    {
        对地,对空
    }
}