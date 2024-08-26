using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCardsGame
{
    internal class 工程兵:CN
    {
        /*完全废弃 请看父类 CardsClassess
        public 工程兵(int owner = 0, float hp = 50, float atk = 25, float def = 40, int surviveOrDied = 1, int ActionPoints = 1, CC unitClasses = (CC)1)
        {
            this.Name = Enum.GetName(typeof(CC), unitClasses);
            this.owner = owner;
            this.unitClasses = unitClasses;
            this.ActionPoints = ActionPoints;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.surviveOrDied = surviveOrDied;
        }*/



        //废案 最初单位是用结构体写的 后来改成类了
        //public override CN Kns(CN gcbs)
        //{
        //    if(zhi<1)
        //    {
        //        zhi++;
        //        gcbs.owner = 0;
        //        gcbs.unitClasses = (int)CC.工程兵;
        //        gcbs.def = 20; gcbs.atk = 15; gcbs.hp = 50;
        //        gcbs.surviveOrdied = 1;
        //        return gcbs;
        //    }
        //    return gcbs;
        //}
    }
}
