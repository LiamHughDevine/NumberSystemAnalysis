using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSystemAnalysis
{
    public abstract class NumberSystem
    {
        public abstract string Name();
        public abstract void Describe();
        public abstract string ReturnNumber(int number);

        // Points are awarded for having as few characters as possible representing each number
        // Smaller value numbers are weighted more than larger numbers
        // i.e. it is worth more if 1 is represented by 1 character than 100
        public virtual double CharacterPoints()
        {
            double a = 0.001;
            double points = 0;
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    double increase = a * Math.Pow(Math.E, -i * 0.001) / ReturnNumber(i).Length;
                    if (!double.IsInfinity(increase))
                    {
                        points += increase;
                    }                    
                }
                catch
                {
                    // No points are awarded if the number system cannot represent the number and all future numbers are
                    // assumed to be unrepresentable (to reduce processing time)
                    i = 10000;
                }
            }
            return Math.Round(points, 3);
        }

        public abstract int TotalCharacters();

        // Points are awarded for having as few unique characters as possible
        public virtual double UniquePoints()
        {
            double a = 1.051;
            return Math.Round(a * Math.Pow(Math.E, -0.05 * TotalCharacters()), 3);
        }

        public abstract bool Representable(int denominator);

        // Points are awarded for being able to represent numbers as decimals
        // It is worth more for larger fractions to be representable
        // i.e. 1/3 being representable is worth more than 1/5
        public virtual double RepresentablePoints()
        {
            double a = 0.01015;
            double points = 0;
            for (int i = 2; i < 1000; i++)
            {
                if (Representable(i))
                {
                    points += a * Math.Pow(Math.E, -i * 0.01);
                }
            }
            return Math.Round(points, 3);
        }
    }

    public abstract class Positional : NumberSystem
    {
        public override bool Representable(int denominator)
        {
            for (int i = 1; i < 20; i++)
            {
                if (Math.Pow(TotalCharacters(), i) % denominator == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Describe()
        {
            Console.WriteLine("This is the most common structure of numbers. " +
                "Each digit represents how many of a number there is. The value of each column, " +
                "starting from the right is 2^[the base number - 1]");
        }
    }

    public class BaseTen : Positional
    {
        public override string Name()
        {
            return "Decimal";
        }

        public override string ReturnNumber(int number)
        {
            if (number == 0)
            {
                return "0";
            }
            return (number.ToString());
        }

        public override int TotalCharacters()
        {
            return 10;
        }
    }

    public class BaseTwo : Positional
    {
        public override string Name()
        {
            return "Binary";
        }

        public override string ReturnNumber(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string output = "";
            bool end = false;
            int remainder;
            do
            {
                remainder = number % 2;
                output = remainder.ToString() + output;
                number /= 2;
                if (number == 0)
                {
                    end = true;
                }
            } while (end == false);

            return output;
        }

        public override int TotalCharacters()
        {
            return 2;
        }
    }

    public class BaseSixteen : Positional
    {
        public override string Name()
        {
            return "Hexadecimal";
        }

        public override string ReturnNumber(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string output = "";
            bool end = false;
            char nextNumber;
            int remainder;
            do
            {
                remainder = number % 16;
                if (remainder < 10)
                {
                    nextNumber = remainder.ToString()[0];
                }
                else
                {
                    nextNumber = Convert.ToChar(remainder + 55);
                }
                output = nextNumber + output;
                number /= 16;
                if (number == 0)
                {
                    end = true;
                }
            } while (end == false);

            return output;
        }

        public override int TotalCharacters()
        {
            return 16;
        }
    }
    public class BaseTwelve : Positional
    {
        public override string Name()
        {
            return "Duodecimal";
        }

        public override string ReturnNumber(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string output = "";
            bool end = false;
            char nextNumber;
            int remainder;
            do
            {
                remainder = number % 12;
                if (remainder < 10)
                {
                    nextNumber = remainder.ToString()[0];
                }
                else
                {
                    nextNumber = Convert.ToChar(remainder + 55);
                }
                output = nextNumber + output;
                number /= 12;
                if (number == 0)
                {
                    end = true;
                }
            } while (end == false);

            return output;
        }

        public override int TotalCharacters()
        {
            return 12;
        }
    }

    public class BaseEight: Positional
    {
        public override string Name()
        {
            return "Octal";
        }

        public override string ReturnNumber(int number)
        {
            if (number == 0)
            {
                return "0";
            }

            string output = "";
            bool end = false;
            char nextNumber;
            int remainder;
            do
            {
                remainder = number % 8;
                nextNumber = remainder.ToString()[0];
                output = nextNumber + output;
                number /= 8;
                if (number == 0)
                {
                    end = true;
                }
            } while (end == false);

            return output;
        }

        public override int TotalCharacters()
        {
            return 8;
        }
    }

    public abstract class SignValue : NumberSystem
    {

    }

    public class Unary : SignValue
    {
        public override void Describe()
        {
            Console.WriteLine("A number system based off tallies where the total number" +
                "of characters is equal to the number");
        }

        public override string Name()
        {
            return "Tally";
        }

        public override string ReturnNumber(int number)
        {
            string tally = "";
            for (int i = 0; i < number; i++)
            {
                tally += "1";
            }
            return tally;
        }

        public override int TotalCharacters()
        {
            return 1;
        }

        public override double CharacterPoints()
        {
            return 0;
        }

        public override bool Representable(int denominator)
        {
            return false;
        }
    }

    public class PerfectSystem : NumberSystem
    {
        public override string Name()
        {
            return "Perfect";
        }

        public override void Describe()
        {
            Console.WriteLine("A theoretically perfect number system that is not possible in reality");
        }

        public override string ReturnNumber(int number)
        {
            return "A";
        }

        public override int TotalCharacters()
        {
            return 1;
        }

        public override double CharacterPoints()
        {
            return 1;
        }

        public override double UniquePoints()
        {
            return 1;
        }

        public override bool Representable(int denominator)
        {
            return true;
        }
    }

    public class Roman : SignValue
    {
        private Dictionary<int, string> Numerals = new Dictionary<int, string>();
        private int LargestValue;

        public Roman()
        {
            Numerals.Add(0, "");
            Numerals.Add(1, "I");
            Numerals.Add(5, "V");
            Numerals.Add(10, "X");
            Numerals.Add(50, "L");
            Numerals.Add(100, "C");
            Numerals.Add(500, "D");
            Numerals.Add(1000, "M");
            LargestValue = 1000;
        }

        public override void Describe()
        {
            Console.WriteLine("The number system used by the Romans. The numerals are either added together or subtracted depending" +
                "on whether they appear before or after more significant numerals");
        }

        public override string Name()
        {
            return "Roman";
        }

        public override bool Representable(int denominator)
        {
            return false;
        }

        public override string ReturnNumber(int number)
        {
            try
            {
                if (number >= LargestValue * 4)
                {
                    throw new Unrepresentable();
                }

                // if it is the numeral then it returns the exact numeral
                if (Numerals.ContainsKey(number))
                {
                    return Numerals[number];
                }
                // Checks to see whether the number is between 1xxx - 5xxx or 5xxx - 10xxx
                int characters = Convert.ToString(number).Length;
                int floor = Convert.ToInt32(Math.Pow(10, characters - 1));
                if (number > floor * 5)
                {
                    // Checks if the number is above 9xxx
                    if (number >= floor * 9)
                    {
                        return Numerals[floor * 5] + Numerals[floor * 10] + ReturnNumber(number - floor * 9);
                    }
                    else
                    {
                        return Numerals[floor * 5] + ReturnNumber(number - floor * 5);
                    }
                }
                else
                {
                    // Checks if the number is above 4xxx
                    if (number >= floor * 4)
                    {
                        return Numerals[floor] + Numerals[floor * 5] + ReturnNumber(number - floor * 4);
                    }
                    else
                    {
                        return Numerals[floor] + ReturnNumber(number - floor);
                    }
                }
                throw new Exception();
            }
            catch (KeyNotFoundException e)
            {
                throw new Unrepresentable();
            }
        }

        public override int TotalCharacters()
        {
            return Numerals.Count;
        }
    }

    public class Egyptian : SignValue
    {
        public override void Describe()
        {
            Console.WriteLine("An evolution of tallies where each character represents a greater total. All characters are" +
                "added together");
        }

        public override string Name()
        {
            return "Egyptian";
        }

        public override bool Representable(int denominator)
        {
            return false;
        }

        public override string ReturnNumber(int number)
        {
            string result = "";

            while (number > 1000000)
            {
                result += "G";
                number -= 1000000;
            }
            while (number > 100000)
            {
                result += "F";
                number -= 100000;
            }
            while (number > 10000)
            {
                result += "E";
                number -= 10000;
            }
            while (number > 1000)
            {
                result += "D";
                number -= 1000;
            }
            while (number > 100)
            {
                result += "C";
                number -= 100;
            }
            while (number > 10)
            {
                result += "B";
                number -= 10;
            }
            while (number >= 1)
            {
                result += "A";
                number -= 1;
            }

            return result;
        }

        public override int TotalCharacters()
        {
            return 7;
        }
    }

    public class Unrepresentable : Exception { };
}
