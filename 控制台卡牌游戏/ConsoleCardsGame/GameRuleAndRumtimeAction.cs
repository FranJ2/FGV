using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;

namespace ConsoleCardsGame
{
    internal static class GameRuleAndRumtimeAction
    {
        //创建 所有卡牌列表实例,玩家卡牌列表实例，敌方卡牌列表实例，卡池列表实例，抽卡随机数实例
        public static List<CN> allCards = new List<CN>();
        public static List<CN> playerCards = new List<CN>();
        public static List<CN> enemyCards = new List<CN>();
        public static List<CN> CardsPools = new List<CN>();
        public static Random randomDrawCards = new Random();
        /// <summary>
        /// 定义并限定关键数值 起始/现有卡牌数，最大卡牌数，敌方起始/现有卡牌数 ，敌方最大卡牌数 ，当前回合数 ，最大回合数
        /// </summary>
        public static short startAndNowKards = 0, maxKards = 7, esk = 0, esmk = 7, nowNumberOfRounds = 0, maxNumberOfRounds = 30;
        /// <summary>
        /// 抽卡机制
        /// </summary>
        public static void DrawCards()
        {
            /*裹在外面的if没实际用途
            int sjks = 0;
            if(sjks >= 0)
            {
                for (int i = 1; i <= Enum.GetValues(typeof(CC)).Length;)
                {
                    //获取枚举的值 并将值添加到卡池中
                    CardsPools.Add((CC)i-1);
                    i++;
                }
                sjks++;
            }*/
            //当前回合数小于最大回合数且当前回合数小于等于20才执行
            if (nowNumberOfRounds <= maxNumberOfRounds && nowNumberOfRounds <= 20)
            {
                //判断持有卡牌是否小于或等于最大卡牌 逻辑错误应为playerCards.Count<=maxKards 以下拥有类似此语句的均为逻辑错误
                if (startAndNowKards <= maxKards)
                {
                    //变量i等于卡池内含有的元素的长度或卡池内含有的元素计数
                    int i = CardsPools.Count;
                    //变量i等于随机抽卡生成的0~i之间的随机值
                    i = randomDrawCards.Next(0, i);

                    //第0回合给于三张牌
                    for (int js = 0; nowNumberOfRounds == 0 && js <= 0;)
                    {
                        CN card = new CN(CardsPools,i);
                        card.Owner = 1;
                        card.SurviveOrDied = 1;
                        playerCards.Add(card);
                        allCards.Add(card);
                        startAndNowKards++;
                        js++;
                        /*//判断i的值
                        //switch (i)
                        //{
                        //    //如果为0则
                        //    case 0:
                        //        //实例化并修改该类的unitClasses属性和owner属性
                        //        普通人 mrptr = new 普通人
                        //        {
                        //            unitClasses = (CC)0,
                        //            owner = 1
                        //        };
                        //        //将此卡牌添加到 在场所有卡牌 玩家持有卡牌
                        //        allCards.Add(mrptr);
                        //        playerCards.Add(mrptr);
                        //        //下面的s......++是逻辑错误
                        //        startsKards++;
                        //        break;
                        //    case 1:
                        //        工程兵 gcb = new 工程兵
                        //        {
                        //            unitClasses = (CC)1,
                        //            owner = 1
                        //        };
                        //        allCards.Add(gcb);
                        //        playerCards.Add(gcb);
                        //        startsKards++;
                        //        break;
                        //    default:
                        //        普通人 mr = new 普通人
                        //        {
                        //            unitClasses = (CC)0,
                        //            owner = 1
                        //        };
                        //        allCards.Add(mr);
                        //        playerCards.Add(mr);
                        //        startsKards++;
                        //        break;
                        //}
                        js++;*/
                    }
                    //判断当前回合数是否大于0
                    if (nowNumberOfRounds > 0)
                    {
                        CN card = new CN(CardsPools, i);
                        card.Owner = 1;
                        card.SurviveOrDied = 1;
                        playerCards.Add(card);
                        allCards.Add(card);
                        startAndNowKards++;
                        /*
                        switch (i)
                        {
                            case 0:
                                普通人 mrptr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 1
                                };
                                allCards.Add(mrptr);
                                playerCards.Add(mrptr);
                                startsKards++;
                                break;
                            case 1:
                                工程兵 gcb = new 工程兵
                                {
                                    unitClasses = (CC)1,
                                    owner = 1
                                };
                                allCards.Add(gcb);
                                playerCards.Add(gcb);
                                startsKards++;
                                break;
                            default:
                                普通人 mr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 1
                                };
                                allCards.Add(mr);
                                playerCards.Add(mr);
                                startsKards++;
                                break;
                        }*/
                    }
                }
                //以下与玩家一样
                if (esk <= esmk)
                {
                    int i = CardsPools.Count;
                    i = randomDrawCards.Next(0, i);
                    for (int js = 0; nowNumberOfRounds == 0 && js <= 0;)
                    {
                        CN card = new CN(CardsPools, i);
                        card.Owner = 2;
                        card.SurviveOrDied = 1;
                        enemyCards.Add(card);
                        allCards.Add(card);
                        esk++;
                        js++;
                        /*
                        switch (i)
                        {
                            case 0:
                                普通人 mrptr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 2
                                };
                                allCards.Add(mrptr);
                                enemyCards.Add(mrptr);
                                esk++;
                                break;
                            case 1:
                                工程兵 gcb = new 工程兵
                                {
                                    unitClasses = (CC)1,
                                    owner = 2
                                };
                                allCards.Add(gcb);
                                enemyCards.Add(gcb);
                                esk++;
                                break;
                            default:
                                普通人 mr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 2
                                };
                                allCards.Add(mr);
                                enemyCards.Add(mr);
                                esk++;
                                break;
                        }
                        js++;*/
                    }
                    if (nowNumberOfRounds > 0)
                    {
                        CN card = new CN(CardsPools, i);
                        card.Owner = 2;
                        card.SurviveOrDied = 1;
                        enemyCards.Add(card);
                        allCards.Add(card);
                        esk++;
                        /*
                        switch (i)
                        {
                            case 0:
                                普通人 mrptr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 2
                                };
                                allCards.Add(mrptr);
                                enemyCards.Add(mrptr);
                                esk++;
                                break;
                            case 1:
                                工程兵 gcb = new 工程兵
                                {
                                    unitClasses = (CC)1,
                                    owner = 2
                                };
                                allCards.Add(gcb);
                                enemyCards.Add(gcb);
                                esk++;
                                break;
                            default:
                                普通人 mr = new 普通人
                                {
                                    unitClasses = (CC)0,
                                    owner = 2
                                };
                                allCards.Add(mr);
                                enemyCards.Add(mr);
                                esk++;
                                break;
                        }*/
                    }
                }
            }
            //判断敌方卡牌总数大于0且当前回合数等于最大回合数 执行失败方法
            else if (enemyCards.Count > 0 && nowNumberOfRounds == maxNumberOfRounds)
            {
                Start.Lose();
            }
            //判断敌方卡牌总数小于1且当前回合数等于最大回合数 执行失败方法
            else if (enemyCards.Count < 1 && nowNumberOfRounds == maxNumberOfRounds)
            {
                Start.Win();
            }
            else Start.StopDrawKards();
            //当前回合数+1
            nowNumberOfRounds++;
            //清空控制台
            //Console.Clear();
            Thread.Sleep(100);
            //搜寻所有行动点属性并设置所有行动点属性的值为1 返回真
            playerCards.All(a => { a.ActionPoints = 1; return true; });
            enemyCards.All(a => { a.ActionPoints = 1; return true; });
            if (nowNumberOfRounds%2==0)
            {
                EnemyAI.EnemyFight();
            }
            else//调用 Update方法
                Update();
        }
        //第一个参数是攻击者 第二个参数是被攻击者 , 第一个是攻击者索引 第二个是被攻击者索引(AI传输过来的顺序是 被攻击者索引 攻击者索引)
        public static void GameRule(CN cns, CN cns2,int bh ,int bh2)
        {
            /*保存原单位atk的值
            float save = cns.Atk;
            //这个运算计算了 被攻击者防御造成了多少的伤害减免 如果为负 则会加血
            cns.Atk -= cns.Atk * (0.05f * cns2.Def);
            //使knx等于刚才计算的atk的值*/
            if (cns.StDefaultWeaponFrame.WeaponTargetingLimitationsU == null)
            {

            }
            else
            {
                bool zj = false;
                for (int i = 0; i < cns.StDefaultWeaponFrame.WeaponTargetingLimitationsU.Count; ++i)
                {
                    int a = (int)cns.StDefaultWeaponFrame.WeaponTargetingLimitationsU[i];
                    int b = (int)cns2.PlanarArraysU;
                    if(a==b)
                    {
                        zj = true;
                        break;
                    }
                    else zj = false;
                }
                if (zj==false)
                {
                    Console.WriteLine($"无法以{cns2.PlanarArraysU}为目标");
                    Start.InputUpdate();
                }    
            }
            (float knx,_) = ArmorDamageFormula.ArmorDamageCompute(cns, cns2);
            //检测knx是否小于0 如果是 则强制值为 0.1
            if (knx < 0)
            {
                knx = 0.1f;
            }
            //被攻击者的生命值减去当前的攻击力
            cns2.Hp -= knx;
            /*设置原单位的atk值为刚传输过来的值
            cns.Atk = save;*/
            //攻击者行动点减少1
            cns.ActionPoints--;
            Console.WriteLine($"玩家{cns2.Owner}的{cns2.Name}因被玩家{cns.Owner}的{cns.Name}的武器{cns.StDefaultWeaponFrame.Name}攻击而受到{knx}伤害");
                /*+
                $"由于{cns2.Name}的防御值为{cns2.Def},受到的伤害减少{cns.Atk * (0.05f * cns2.Def)}点," +
                $"所以实际受到的伤害为{knx},现剩余{cns2.Hp}点生命值");*/
            //检测被攻击者hp是否小于或等于0
            if (cns2.Hp <= 0)
            {
                //设置被攻击者存活状态为假 0
                cns2.SurviveOrDied = 0;
                Console.WriteLine($"玩家{cns2.Owner}的{cns2.Name}已经死亡    ");
                //如果当前回合数为偶数则执行
                if (nowNumberOfRounds % 2 == 0)
                {
                    //设置索引的攻击者的所有属性等于传输过来的攻击者属性
                    enemyCards[bh2] = cns;
                    //查找并清除玩家卡牌内所有存活状态为0的卡牌
                    playerCards.RemoveAll((dCNS) => dCNS.SurviveOrDied == 0);
                    //查找并清除所有卡牌内所有存活状态为0的卡牌
                    allCards.RemoveAll((dCNS) => dCNS.SurviveOrDied == 0);
                }
                else
                {
                    playerCards[bh] = cns;
                    //查找并清除敌方卡牌内所有存活状态为0的卡牌
                    enemyCards.RemoveAll((dCNS) => dCNS.SurviveOrDied == 0);
                    allCards.RemoveAll((dCNS) => dCNS.SurviveOrDied == 0);
                }
                Console.WriteLine($"现在 玩家剩余牌数：{playerCards.Count}，敌方剩余牌数：{enemyCards.Count}，场上总牌数：{allCards.Count}       ");
                Console.WriteLine();
            }
            else
            {
                if (nowNumberOfRounds % 2 == 0)
                {
                    playerCards[bh] = cns2;
                    enemyCards[bh2] = cns;
                }
                else
                {
                    playerCards[bh] = cns;
                    enemyCards[bh2] = cns2;
                }
            }
            if (playerCards.FindAll(x => x.ActionPoints > 0).Count > 0 || enemyCards.FindAll(x => x.ActionPoints > 0).Count > 0)
            {
                if(nowNumberOfRounds%2==0)
                {
                    EnemyAI.EnemyFight();
                }
                else if(playerCards.FindAll(x => x.ActionPoints > 0).Count > 0)
                    Start.InputUpdate();
                else DrawCards();
            }
            else Update();
        }
        public static void Update()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"当前回合数{nowNumberOfRounds}");
            Console.WriteLine("                                                            ");
            Console.WriteLine();
            int ecsl = enemyCards.Count;
            Console.Write("---------------    ");
            Console.WriteLine();
            //分步打印卡牌各属性需要提前获得卡牌的数量
            for (int i = 0; i < ecsl;) { Console.Write($"{enemyCards[i].Name} 编号:{i}        "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"生命值:{enemyCards[i].Hp}            "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"武器:{enemyCards[i].StDefaultWeaponFrame.Name}            "); i++;}
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"武器伤害:{enemyCards[i].StDefaultWeaponFrame.Damage}            "); i++; }
            Console.WriteLine();
            for (int i = 0,j = 0 ; i < ecsl;)
            {
                Console.Write($"武器目标限制:  ");
                int b = playerCards[i].StDefaultWeaponFrame.GetWeaponTargetingLimitationsUF();
                try
                {
                    for (; j < b;)
                    {
                        
                        Console.Write(enemyCards[i].StDefaultWeaponFrame.WeaponTargetingLimitationsU.ElementAt(j)); Console.Write("    ");
                        j++;
                    }
                    j = 0;
                }
                catch (Exception ex)
                {
                    break;
                }
                i++;
            }
            Console.WriteLine();
            for (int i = 0, j = 0; i < ecsl;) 
            {
                Console.Write($"护甲伤害加成: ");
                for (; j < enemyCards[i].StDefaultWeaponFrame.ArmorDamageBuff.Keys.Count; )
                {
                    Console.Write(enemyCards[i].StDefaultWeaponFrame.ArmorDamageBuff.ElementAt(j).Key); Console.Write(":");
                    Console.Write(enemyCards[i].StDefaultWeaponFrame.ArmorDamageBuff.ElementAt(j).Value); Console.Write("    ");
                    j++;
                }
                j = 0;
                i++;
            }
            Console.WriteLine();
            for (int i = 0, j = 0; i < ecsl;)
            {
                Console.Write($"属性伤害加成: ");

                for (; j < enemyCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.Keys.Count;)
                {
                    Console.Write(enemyCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.ElementAt(j).Key);
                    Console.Write(":");
                    Console.Write(enemyCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.ElementAt(j).Value);
                    Console.Write("    ");
                    j++;
                }
                j = 0;
                i++;
            }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"护甲类型:{enemyCards[i].ArmorTypeU} "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"护甲值:{enemyCards[i].Armor}           "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"每护甲伤害减免:{enemyCards[i].ArmorReducesDamage} "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"行动点:{enemyCards[i].ActionPoints}             "); i++; }
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n");
            int pcsl = playerCards.Count;
            Console.Write("---------------    ");
            Console.WriteLine();
            for (int i = 0; i < pcsl;) { Console.Write($"{playerCards[i].Name} 编号:{i}        ");i++; }
            Console.WriteLine();
            for (int i = 0; i < pcsl;) { Console.Write($"生命值:{playerCards[i].Hp}            ");i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"武器:{playerCards[i].StDefaultWeaponFrame.Name}            "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"武器伤害:{playerCards[i].StDefaultWeaponFrame.Damage}            "); i++; }
            Console.WriteLine();
            for (int i = 0, j = 0; i < ecsl;)
            {
                Console.Write($"武器目标限制:  ");
                int b = playerCards[i].StDefaultWeaponFrame.GetWeaponTargetingLimitationsUF();
                try
                {
                    for (; j < b;)
                    {
                        Console.Write(playerCards[i].StDefaultWeaponFrame.WeaponTargetingLimitationsU.ElementAt(j)); Console.Write("    "); j++;
                    }
                    j = 0;
                }
                catch (Exception ex)
                {
                    break;
                }
                i++;
            }
            Console.WriteLine();
            for (int i = 0, j = 0; i < ecsl;)
            {
                Console.Write($"护甲伤害加成: ");
                for (; j < playerCards[i].StDefaultWeaponFrame.ArmorDamageBuff.Keys.Count;)
                {
                    Console.Write(playerCards[i].StDefaultWeaponFrame.ArmorDamageBuff.ElementAt(j).Key); Console.Write(":");
                    Console.Write(playerCards[i].StDefaultWeaponFrame.ArmorDamageBuff.ElementAt(j).Value); Console.Write("    ");
                    j++;
                }
                j = 0;
                i++;
            }
            Console.WriteLine();
            for (int i = 0, j = 0; i < ecsl;)
            {
                Console.Write($"属性伤害加成: ");
                
                for (; j < playerCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.Keys.Count;)
                {
                    Console.Write(playerCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.ElementAt(j).Key);
                    Console.Write(":");
                    Console.Write(playerCards[i].StDefaultWeaponFrame.SpecialDamageBuffU.ElementAt(j).Value);
                    Console.Write("    ");
                    j++;
                }
                j = 0;
                i++;
            }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"护甲类型:{playerCards[i].ArmorTypeU} "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"护甲值:{playerCards[i].Armor} "); i++; }
            Console.WriteLine();
            for (int i = 0; i < ecsl;) { Console.Write($"每护甲伤害减免:{playerCards[i].ArmorReducesDamage} "); i++; }
            Console.WriteLine();
            for (int i = 0; i < pcsl;) { Console.Write($"行动点:{playerCards[i].ActionPoints}             ");i++; }
            Console.WriteLine();
            Console.WriteLine("                                                            ");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            if(nowNumberOfRounds%2==0)
            {
                EnemyAI.EnemyFight();
            }
            else
            {
                Console.WriteLine("请输入两个数字来决定使用几号卡来攻击敌方的几号卡(输完一个数后请回车)");
                Start.InputUpdate();
            }
        }
        //写了 好像没用
        public static void CombatAction(int bh, int bh2) => GameRule(playerCards[bh], enemyCards[bh2],bh,bh2);
    }
}