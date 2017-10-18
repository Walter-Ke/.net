//參考 https://dotblogs.com.tw/hatelove/archive/2012/06/07/csharp-linq-lambda-introduction.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var mom = new List<Mom> { new Mom_Charp_1(), new Mom_Charp_2(), new Mom_Charp_3() };
           
            Console.WriteLine("用120元買的鹽炒菜");
            MomCooking(mom, 120);
            
            Console.WriteLine("用80元買的鹽炒菜");
            MomCooking(mom, 80);

            Console.ReadKey();
        }

        private static void MomCooking(List<Mom> mom, int money)
        {
            foreach (var m in mom)
            {
                m.Cooking(money);
            }
        }
    }

    abstract public class Mom {

        abstract public void Cooking(int money);
    }

    public class Mom_Charp_1 : Mom
    {
        public delegate Salt GetSalt(int money);
        public override void Cooking(int money)
        {
            var joey = new Joey();
            GetSalt getSalt =  joey.BuySalt;

            Console.WriteLine("C#1.0的媽媽，花{1}元，透過Joey的BuySalt()，拿{0}炒菜", getSalt(money).Name, money.ToString());
        }
    }

    public class Mom_Charp_2 : Mom
    {
        public delegate Salt GetSalt(int money);

        public override void Cooking(int money)
        {
                               //delegate(參數){委派執行內容} 
            GetSalt getSalt = delegate(int moneyForSalt)
            {

                if (moneyForSalt > 100)
                {
                    return new RockSalt();
                }
                else
                {
                    return new SeaSalt();
                }
            };
            Console.WriteLine("C#2.0的媽媽, 花{1}元，透過匿名delegate方法結果，拿{0}炒菜", getSalt(money).Name, money.ToString());
        }
    }

    public class Mom_Charp_3 : Mom
    {
        public delegate Salt GetSalt(int money);
        public override void Cooking(int money)
        {
            GetSalt getSalt = x =>
            {
                if (x > 100)
                    return new RockSalt();
                else
                    return new SeaSalt();
            };
            Console.WriteLine("C#3.0的媽媽, 花{1}元，透過lambda敘述式結果，拿{0}炒菜", getSalt(money).Name, money.ToString()); 
        }
    }

    public class Joey
    {
        public Salt BuySalt(int money)
        {
            if (money > 100)
            {
                return new RockSalt();
            }
            else
            {
                return new SeaSalt();
            }
        }
    }

    abstract public class Salt
    {
        public string Name = "salt";
    }

    public class RockSalt : Salt
    {
        public RockSalt()
        {
            Name ="RockSalt";
        }
    }

    public class SeaSalt : Salt
    {
        
        public SeaSalt()
        {
            Name = "SeaSalt";
        }
        
    }
}
