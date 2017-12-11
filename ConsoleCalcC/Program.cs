using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ConsoleCalcC
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string inputString = null;

            Console.WriteLine("Введите выражение:");
            inputString = Console.ReadLine();

            try
            {
                MatchCollection resultMatches = StringToMatches(inputString);
                ArrayList resultBackPolskRec = BackPolskRec(resultMatches);
                string result = Calc(resultBackPolskRec).ToString();
                Console.WriteLine("Результат: " + result);
                Console.ReadKey();
            }
            catch (Exception)
            {

                Console.WriteLine("Ошибка выражения!");
                Console.ReadKey();
            }

        }

        public static MatchCollection StringToMatches(string inputString)
        {
            Regex newReg = new Regex(ModuleOperators.pattern());
            MatchCollection matches = newReg.Matches(inputString);

            return matches;
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static ArrayList BackPolskRec(MatchCollection inputMatches)
        {
            ArrayList result = new ArrayList();
            Stack tmpStack = new Stack();

            foreach (Match item in inputMatches)
            {
                if (IsNumeric(item.Value))
                {
                    result.Add(item.Value);
                }
                else
                {
                    switch (item.Value)
                    {
                        case "(":
                            tmpStack.Push(item.Value);
                            break;
                        case ")":
                            do
                            {
                                if (tmpStack.Count > 0)
                                {
                                    string tmpString = tmpStack.Peek().ToString();
                                    if (tmpString == "(") break;
                                    result.Add(tmpStack.Pop());
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            } while (true);
                            tmpStack.Pop();
                            break;
                        default:
                            while (tmpStack.Count > 0 && GetOperatorPrioritet(item.Value) <= GetOperatorPrioritet(tmpStack.Peek().ToString()))
                            {
                                result.Add(tmpStack.Pop());
                            }

                            tmpStack.Push(item.Value);
                            break;
                    }
                }
            }
            while (tmpStack.Count > 0)
            {
                if (tmpStack.Peek().ToString() != ")" & tmpStack.Peek().ToString() != "(")
                {
                    result.Add(tmpStack.Pop());
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            return result;
        }

        public static float Calc(ArrayList inputArrayList)
        {
            float result = 0;
            Stack tmpStack = new Stack();

            foreach (string item in inputArrayList)
            {
                if (IsNumeric(item))
                {
                    tmpStack.Push(item);
                }
                else
                {
                    if (tmpStack.Count >= 2)
                    {
                        float x = float.Parse(tmpStack.Pop().ToString());
                        float y = float.Parse(tmpStack.Pop().ToString());
                        float z = GetOperatorCalc(item, x, y);
                        tmpStack.Push(z);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
            result = float.Parse(tmpStack.Pop().ToString());
            return result;
        }

        public static int GetOperatorPrioritet(string symbol)
        {
            foreach (classSymbol item in ModuleOperators.Operators())
            {
                if (item.Symbol == symbol) return item.Prioritet;
            }
            throw new ArgumentException();
        }

        public static float GetOperatorCalc(string symbol, float x, float y)
        {
            foreach (classSymbol item in ModuleOperators.Operators())
            {
                if (item.Symbol == symbol) return item.Calc(x, y);
            }
            throw new ArgumentException();
        }

    }
}
