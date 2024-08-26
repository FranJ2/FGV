using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ConsoleCardsGame
{
    internal class Start
    {
        /// <summary>
        /// 应用程序主入口 输出开场注意事项 字符串变量go读取用户在控制台上输入的值 并将值传递给Draw_Cards方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            XmlLoad load = new XmlLoad();
            load.GetFolder();
            load.AddToCardPool();
            Console.WriteLine("It is a card game made based on the console application 这是一个控制台卡牌游戏");
            Console.WriteLine("please Input all any char come DrawCards 输入任意数字来开始游戏 注意:卡牌的最大索引为10 我方卡牌在下面 敌方卡牌在上面");
            string go=Console.ReadLine();
            Draw_Cards(go);
        }
        /// <summary>
        /// 直接调用DrawCards方法 ，没错 刚才传过来的参数没啥用
        /// </summary>
        /// <param name="go"></param>
        public static void Draw_Cards(string go) => GameRuleAndRumtimeAction.DrawCards();
        /// <summary>
        /// 玩家输入更新
        /// </summary>
        public static void InputUpdate()
        {
            //查找玩家卡组内所有行动点大于0的卡牌的计数
            int Apds = GameRuleAndRumtimeAction.playerCards.FindAll(x=>x.ActionPoints>0).Count();
            if(Apds > 0) 
            {
                Console.WriteLine($"有{Apds}张卡牌拥有未使用的行动点 是否要继续发布战斗指令 输入1 为继续战斗 输入2 为抽牌 输入意外的字符默认为抽牌(输完后请按回车不要空格)");
                //为什么 因为我不想分割字符串 这样做省事 千万别学我
                Console.Write("下列卡牌还具有行动点: (输入编号的时候 输完一个请换行 不然会异常)");
                //查找并输出 所有行动点为1的卡牌的索引
                for(int i = 0;i<GameRuleAndRumtimeAction.playerCards.Count;i++)
                {
                    if(GameRuleAndRumtimeAction.playerCards[i].ActionPoints==1)
                    {
                        Console.Write($"{i}  ");
                    }
                }
                Console.WriteLine();
                int tj = 0;
                //异常处理代码块 try包含可能出现异常的代码
                try
                {
                    //读取用户输出并转换为整数
                     tj = Convert.ToInt32(Console.ReadLine());
                }
                //catch 处理try出现的异常 Exception是所有异常类的基类 里面写这个异常类意味着捕获所有异常
                catch(Exception ex)
                {
                    //如有异常则设置tj为0
                    tj = 0;
                }
                if (tj == 1)
                {
                    //string shuzi = Console.ReadLine();

                    //读取两个用户输入并转换为整数
                    int a = Convert.ToInt32(Console.ReadLine());
                    int b = Convert.ToInt32(Console.ReadLine());
                    //检测输入的卡牌索引行动点是否为0
                    if (GameRuleAndRumtimeAction.playerCards[a].ActionPoints == 0)
                    {
                        Console.Write("指定的攻击卡牌行动点为0 请重新选择");
                        InputUpdate();
                    }
                    //输入异常字符或超出双方卡组的计数 默认抽卡
                    else if (a > GameRuleAndRumtimeAction.playerCards.Count || b > GameRuleAndRumtimeAction.enemyCards.Count)
                    {
                        GameRuleAndRumtimeAction.DrawCards();
                    }
                    //执行 战斗行动 ......
                    GameRuleAndRumtimeAction.CombatAction(a, b);
                }
                //否则 抽卡
                else
                {
                    GameRuleAndRumtimeAction.DrawCards();
                } 
            }
            else GameRuleAndRumtimeAction.DrawCards();
        }
        /// <summary>
        /// 这个虽然是写了 但是实际上没用到
        /// </summary>
        public static void StopDrawKards() => Console.WriteLine("停止发牌");
        /// <summary>
        /// 清空所有卡牌 敌方卡牌 玩家卡牌 卡池内的所有元素
        /// 输出 你胜利了并线程休眠3000毫秒(3秒) 然后强制彻底的中断所有进程
        /// </summary>
        public static void Win()
        {
            GameRuleAndRumtimeAction.allCards.Clear();
            GameRuleAndRumtimeAction.enemyCards.Clear();
            GameRuleAndRumtimeAction.playerCards.Clear();
            GameRuleAndRumtimeAction.CardsPools.Clear();
            Console.WriteLine("you win!");
            Thread.Sleep(3000);
            Process.GetCurrentProcess().Kill();
        }
        /// <summary>
        /// 清空所有卡牌 敌方卡牌 玩家卡牌 卡池内的所有元素
        /// 输出 游戏失败并线程休眠3000毫秒(3秒) 然后强制彻底的中断所有进程
        /// </summary>
        public static void Lose()
        {
            GameRuleAndRumtimeAction.allCards.Clear();
            GameRuleAndRumtimeAction.enemyCards.Clear();
            GameRuleAndRumtimeAction.playerCards.Clear();
            GameRuleAndRumtimeAction.CardsPools.Clear();
            Console.WriteLine("Game Over");
            Thread.Sleep(3000);
            Process.GetCurrentProcess().Kill();
        }
    }
}
