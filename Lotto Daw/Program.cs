using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto_Daw
{
    class Program
    {
        static List<string> bets;
        static void Main(string[] args)
        {
            bets = new List<string>();
            while (true)
            {
                Console.WriteLine("Welcome to IvyLotto 6/45");
                string res = GetResponse("Type 1 to place bet, Type 2 to draw : ");
                if (res.Equals("1"))
                {
                    EnterBet();
                }
                else if (res.Equals("2"))
                {
                    string ln = GenerateDraw();
                    CheckWinner(ln);
                    break;
                }
            }
            Console.ReadKey();
        }
        private static void CheckWinner(string luckyNumber)
        {
            Console.WriteLine("The lucky number for IvyLotto 6/45 is " + luckyNumber);
            string[] ln = luckyNumber.Split(',');
            for (int i = 0; i < bets.Count; i++)
            {
                string[] bet = bets[i].Split('|')[1].ToString().Split(',');
                int count = 0;
                for (int j = 0; j < ln.Length; j++)
                {
                    for (int k = 0; k < bet.Length; k++)
                    {
                        if (Convert.ToInt32(ln[j].ToString()).Equals(Convert.ToInt32(bet[k].ToString())))
                            count++;
                    }
                }
                SetCongratulatoryMessage(count, bets[i]);
            }
            Console.WriteLine("Thank you for playing");
        }
        private static void SetCongratulatoryMessage(int x, string nameBet)// congrats
        {
            if (x == 4)
                Console.WriteLine(nameBet.Split('|')[0] + " got the four number combination with the bet " + nameBet.Split('|')[1]);
            if(x == 5)
                Console.WriteLine(nameBet.Split('|')[0] + " got the five number combination with the bet " + nameBet.Split('|')[1]);
            if(x == 6)
                Console.WriteLine(nameBet.Split('|')[0] + " hit the jackpot with the bet " + nameBet.Split('|')[1]);
        }
        private static string GenerateDraw()// draw lots
        {
            Random r = new Random();
            int[] arr = new int[6];
            string luckyNumber = "";
            for (int i = 0; i < 6; i++)
            {
                int num = r.Next(1,46);
                if (IsExist(num, arr))
                    i--;
                else
                    arr[i] = num;
            }
            foreach (int i in arr)
            {
                luckyNumber += i + ",";
            }
            luckyNumber = luckyNumber.Remove(luckyNumber.Length - 1);
            return luckyNumber;
        }
        private static void EnterBet()// just enter
        {
            string name = GetResponse("Please enter your name : ");
            string bet = GetResponse("Please enter your bet separated by comma ex(1,2,3,4,5,6) : ");
            string[] numbers = bet.Split(',');
            if (numbers.Length == 6)
            {
                if (IsAllNumberValid(numbers))
                {
                    bets.Add(name + "|" + bet);
                }
            }
            else
                Console.WriteLine("Invalid Bet!");
        }
        private static bool IsAllNumberValid(string[] arr)// if bet is okay
        {
            try
            {
                foreach (string s in arr)
                {
                    if (Convert.ToInt32(s) > 45 && Convert.ToInt32(s) < 1)
                    {
                        Console.WriteLine("Invalid Bet!"); return false;
                    }
                }
                if (HasExistNumber(arr))
                {
                    Console.WriteLine("Invalid Bet!"); return false;
                }
                return true;
            }
            catch 
            {
                Console.WriteLine("Invalid Bet!");
                return false; 
            }
        }
        private static bool HasExistNumber(string[] arr)// for bet
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i].ToString().Equals(arr[j].ToString()))
                    {
                        Console.WriteLine("Invalid Bet!");
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool IsExist(int num, int[] arr)// for lucky number
        {
            for (int i = 0; i < arr.Length; i++)
                if (num == arr[i])
                    return true;
            return false;
        }
        private static string GetResponse(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
