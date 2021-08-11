using System;
using System.Collections.Generic;

namespace NumberSystemAnalysis
{
    class Program
    {
        static void Main()
        {
            List<NumberSystem> Systems = new List<NumberSystem>();
            Systems.Add(new BaseTen());
            Systems.Add(new BaseTwo());
            Systems.Add(new BaseSixteen());
            Systems.Add(new BaseTwelve());
            Systems.Add(new BaseEight());
            Systems.Add(new Unary());
            Systems.Add(new Roman());
            Systems.Add(new Egyptian());

            Menu(Systems);
        }

        public static void Menu(List<NumberSystem> Systems)
        {
            bool quit = false;
            do
            {
                WriteMenu();
                string option = Console.ReadLine();
                Console.Clear();
                if (option == "1")
                {
                    WriteNumberSystems(Systems);
                }
                else if (option == "2")
                {
                    CompareMenu(Systems);
                }
                else if (option == "E")
                {
                    TestPerfect();
                }
                else if (option == "X" || option == "x")
                {
                    quit = true;
                }
                Console.Clear();
            } while (quit == false);
        }

        public static void WriteMenu()
        {
            Console.WriteLine("1 - Learn About Number Systems");
            Console.WriteLine("2 - Compare Number Systems");
            Console.WriteLine("X - Quit");
        }

        public static void WriteNumberSystems(List<NumberSystem> Systems)
        {
            foreach (NumberSystem Sys in Systems)
            {
                Console.WriteLine(Sys.Name() + ":");
                Sys.Describe();
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public static void TestPerfect()
        {
            /*PerfectSystem perfect = new PerfectSystem();
            for (int i = 0; i < 10000; i++)
            {
                if (Math.Round(perfect.RepresentablePoints(i * 0.00001), 3) == 1)
                {
                    Console.WriteLine(i);
                }
                
                Console.WriteLine(i + " : " + perfect.RepresentablePoints(i * 0.00001));
            }
            Console.ReadLine();*/
        }

        public static void CompareMenu(List<NumberSystem> Systems)
        {
            bool quit = false;
            do
            {
                WriteCompareMenu();
                string option = Console.ReadLine();
                Console.Clear();
                if (option == "1")
                {
                    AverageCharacterNumber(Systems);
                }
                else if (option == "2")
                {
                    CharacterNumberPoints(Systems);
                }
                else if (option == "3")
                {
                    TotalCharacterNumber(Systems);
                }
                else if (option == "4")
                {
                    UniqueCharacterPoints(Systems);
                }
                else if (option == "5")
                {
                    ProportionalFractionNumber(Systems);
                }
                else if (option == "6")
                {
                    RepresentableFractionsPoints(Systems);
                }
                else if (option == "7")
                {
                    FinalPoints(Systems);
                }
                else if (option == "X" || option == "x")
                {
                    quit = true;
                }
                if (quit == false)
                {
                    Console.ReadLine();
                }
                Console.Clear();
            } while (quit == false);
        }

        public static void WriteCompareMenu()
        {
            Console.WriteLine("1 - Average number of characters");
            Console.WriteLine("2 - Average character points");
            Console.WriteLine("3 - Number of unique characters");
            Console.WriteLine("4 - Unique character points");
            Console.WriteLine("5 - Proportion of representable fractions");
            Console.WriteLine("6 - Representable fractions points");
            Console.WriteLine("7 - Final Points");
            Console.WriteLine("X - Return to menu");
        }

        public static void AverageCharacterNumber(List<NumberSystem> Systems)
        {
            Console.WriteLine("What number do you want to compare up to? (includes 0)");
            int number = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < Systems.Count; i++)
            {
                int total = 0;
                for (int j = 0; j < number + 1; j++)
                {
                    total += Systems[i].ReturnNumber(j).Length;
                }
                float average = (float)total / (number + 1);
                Console.WriteLine(Systems[i].Name() + " : " + average);
            }
        }

        public static void CharacterNumberPoints(List<NumberSystem> Systems)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                Console.WriteLine(Systems[i].Name() + " : " + Systems[i].CharacterPoints());
            }
        }

        public static void TotalCharacterNumber(List<NumberSystem> Systems)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                Console.WriteLine(Systems[i].Name() + " : " + Systems[i].TotalCharacters());
            }
        }

        public static void UniqueCharacterPoints(List<NumberSystem> Systems)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                Console.WriteLine(Systems[i].Name() + " : " + Systems[i].UniquePoints());
            }
        }

        public static void ProportionalFractionNumber(List<NumberSystem> Systems)
        {
            Console.WriteLine("What denomiantor do you want to compare up to?");
            int number = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < Systems.Count; i++)
            {
                int total = 0;
                for (int j = 2; j < number + 1; j++)
                {
                    if (Systems[i].Representable(j))
                    {
                        total++;
                    }
                }
                float average = (float)total / (number - 1);
                Console.WriteLine(Systems[i].Name() + " : " + average);
            }
        }

        public static void RepresentableFractionsPoints(List<NumberSystem> Systems)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                Console.WriteLine(Systems[i].Name() + " : " + Systems[i].RepresentablePoints());
            }
        }

        public static void FinalPoints(List<NumberSystem> Systems)
        {
            for (int i = 0; i < Systems.Count; i++)
            {
                double points = Math.Round((Systems[i].CharacterPoints() + Systems[i].RepresentablePoints() + Systems[i].UniquePoints()) / 3, 3);
                Console.WriteLine(Systems[i].Name() + " : " + points);
            }
        }
    }
}
