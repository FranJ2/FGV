using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCardsGame
{
    internal static class EnemyAI
    {
        private static List<float> temp = new List<float>();
        /* 废案private static List<float> temp2 = new List<float>();
        private static List<int> bgjzlb = new List<int>();*/
        public static void EnemyFight()
        {
            //查找敌方卡牌里所有行动点大于0的卡牌的计数
            int Apds = GameRuleAndRumtimeAction.enemyCards.FindAll(x => x.ActionPoints > 0).Count();
            //判断Apds是否大于-1 是则进行AI行为 这边Apds>-1 逻辑可能有误
            if (Apds > -1)
            {
                /* 废案
                //float[,] min = new float[GameRuleAndRumtimeAction.enemyCards.Count, GameRuleAndRumtimeAction.playerCards.Count];
                //float[] minjs = new float[0];
                //List<float> te = new List<float>();
                //int allekbl = 0;
                //for (; allekbl <= GameRuleAndRumtimeAction.enemyCards.Count;)
                //{
                //    for (int i = 1; i <= GameRuleAndRumtimeAction.playerCards.Count;)
                //    {
                //        float gjjs = GameRuleAndRumtimeAction.enemyCards[allekbl].atk
                //            - (GameRuleAndRumtimeAction.enemyCards[allekbl].atk * (0.05f * GameRuleAndRumtimeAction.playerCards[i].def));
                //        float sxhp = GameRuleAndRumtimeAction.playerCards[i].hp - gjjs;;
                //        min[allekbl, i] = sxhp;
                //        i++;
                //    }
                //    allekbl++;
                //}
                //for (int m1 = 1; m1 < GameRuleAndRumtimeAction.enemyCards.Count; m1++)
                //{
                //    for (int m2 = 0; m2 < GameRuleAndRumtimeAction.playerCards.Count; m2++)
                //    {
                //        te.Add(min[m1, m2]);
                //    }
                //}
                //minjs = te.ToArray();
                //int zxsy = Array.IndexOf(minjs, minjs.Min());
                ///*int ywsy = 0;
                ////#region 一维索引获取 ek
                ////if (zxsy <= 9) ywsy = 0;
                ////else if (zxsy <= 19 && zxsy >= 10) ywsy = 1;
                ////else if (zxsy <= 29 && zxsy >= 20) ywsy = 2;
                ////else if (zxsy <= 39 && zxsy >= 30) ywsy = 3;
                ////else if (zxsy <= 49 && zxsy >= 40) ywsy = 4;
                ////else if (zxsy <= 59 && zxsy >= 50) ywsy = 5;
                ////else if (zxsy <= 69 && zxsy >= 60) ywsy = 6;
                ////else if (zxsy <= 79 && zxsy >= 70) ywsy = 7;
                ////else if (zxsy <= 89 && zxsy >= 80) ywsy = 8;
                ////else if (zxsy <= 99 && zxsy >= 90) ywsy = 9;
                ////else ywsy = 0;
                ////#endregion
                ////#region 二维索引获取 pk
                ////int ewsj = 0;
                ////switch (ywsy)
                ////{
                ////    case 0: ewsj = zxsy; break;
                ////    case 1: ewsj = zxsy - (ywsy * 10); break;
                ////    case 2: ewsj = zxsy - (ywsy * 10); break;
                ////    case 3: ewsj = zxsy - (ywsy * 10); break;
                ////    case 4: ewsj = zxsy - (ywsy * 10); break;
                ////    case 5: ewsj = zxsy - (ywsy * 10); break;
                ////    case 6: ewsj = zxsy - (ywsy * 10); break;
                ////    case 7: ewsj = zxsy - (ywsy * 10); break;
                ////    case 8: ewsj = zxsy - (ywsy * 10); break;
                ////    case 9: ewsj = zxsy - (ywsy * 10); break;
                ////    default:
                ////        ewsj = zxsy - (ywsy * 10); break;
                ////}
                ////#endregion
                //int ewsj = 0;
                //for (int i1 = 1; i1 <= GameRuleAndRumtimeAction.enemyCards.Count;)
                //{
                //    int i = 0;
                //    i = minjs.Count() / GameRuleAndRumtimeAction.enemyCards.Count;
                //    if (zxsy % i != 0)
                //    {
                //        ewsj = zxsy % i;
                //    }
                //    else if(zxsy % i == 0)
                //    {
                //        ewsj = zxsy % i;
                //    }
                //    i1++;
                //}
                */
                #region 贪婪AI逻辑
                /*废案 float[] zhi = new float[0];
                //float[] zhiNL = new float[0];
                //List<float> tempNL = new List<float>();
                
                //for(int i = 0; i < GameRuleAndRumtimeAction.playerCards.Count; i++)
                //{
                //    tempNL.Add(i);
                //}
                //zhiNL = tempNL.ToArray();
                //int bsjz = zhiNL.Length;
                //int sjz = 0;
                //for (int b = 0, i = 0 ; b < bsjz;)
                //{
                //    i = GameRuleAndRumtimeAction.enemyCards.FindIndex(x => x.ActionPoints > 0);
                //    for (; i < GameRuleAndRumtimeAction.enemyCards.Where(x => x.ActionPoints > 0).Count() && b < bsjz;)
                //    {
                //        temp.Add(GameRuleAndRumtimeAction.enemyCards[i].atk - (GameRuleAndRumtimeAction.enemyCards[i].atk * (0.05f * GameRuleAndRumtimeAction.playerCards[b].def)));
                //        bgjzlb.Add(b);
                //        b++;
                //    }
                //    if(i > GameRuleAndRumtimeAction.enemyCards.Where(x => x.ActionPoints > 0).Count())
                //    {
                //        i++;
                //    }
                //}
                //temp2.AddRange(temp);
                
                //for (int i = 0; i < temp2.Count;)
                //{
                //    if (temp2[i] < 0)
                //    {
                //        if (GameRuleAndRumtimeAction.enemyCards[i].ActionPoints>0)
                //        {
                //            temp2[i] = Math.Abs(temp2[i]);
                //        }
                //    }
                //    i++;
                //}*/
                int gjz = 0;
                float knx = 0;
                //设置gjz等于敌方卡池中行动点大于0的卡牌的索引 优先匹配最先找到的
                gjz = GameRuleAndRumtimeAction.enemyCards.FindIndex(x => x.ActionPoints > 0);
                float[] min = new float[0];
                //gjz大于-1才会执行
                if (gjz > -1)
                {
                    for (int i = 0; i < GameRuleAndRumtimeAction.playerCards.Count;)
                    {
                        //用当前gjz索引指向的卡牌的攻击减去 玩家卡牌索引i的防御值造成的伤害减免 结果就是得到对哪个玩家卡牌攻击造成的伤害最多
                        (_,knx) = ArmorDamageFormula.ArmorDamageCompute(GameRuleAndRumtimeAction.enemyCards[i], GameRuleAndRumtimeAction.playerCards[i]);
                        //将knx添加进列表里
                        temp.Add(knx);
                        i++;
                    }
                }
                else
                    GameRuleAndRumtimeAction.DrawCards();
                //将列表转换为一维数组
                min = temp.ToArray();
                //查找被攻击者的索引 索引是max数组中最大的值
                int bgjz = Array.IndexOf(min,min.Min());
                //清空列表
                temp.Clear();
                #endregion
                //如果玩家持有的卡牌大于0 执行 游戏规则 传输敌方卡牌的攻击者 玩家卡牌的被攻击者 被攻击者索引 攻击者索引
                if(GameRuleAndRumtimeAction.playerCards.Count > 0)
                {
                    GameRuleAndRumtimeAction.GameRule(GameRuleAndRumtimeAction.enemyCards[gjz], GameRuleAndRumtimeAction.playerCards[bgjz], bgjz, gjz);
                }
                //否则执行 DrawCards进行抽卡
                else
                {
                    GameRuleAndRumtimeAction.DrawCards();
                }
            }
            //否则 抽卡
            else GameRuleAndRumtimeAction.DrawCards();
        }
    }
}
