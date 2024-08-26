using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCardsGame
{
    internal class 普通人:CN
    {
        /*完全废弃 请看父类 CardsClassess
        public 普通人(int owner = 0, float hp = 15, float atk = 5, float def = 10, int surviveOrDied = 1, int ActionPoints = 1, CC unitClasses = (CC)0)
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
        //public override CN Kns(CN ptrs)
        //{
        //    if (zhi < 1)
        //    {
        //        zhi++;
        //        ptr.owner = 0;
        //        ptr.unitClasses = (int)CC.普通人;
        //        ptr.def = 20; ptr.atk = 15; ptr.hp = 50;
        //        ptr.surviveOrdied = 1;
        //        return ptrs;
        //    }
        //    return ptrs;
        //}
    }
}
