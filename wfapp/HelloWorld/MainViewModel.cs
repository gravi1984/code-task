using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OxyPlot;


namespace HelloWorld
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            // var input = "y=2*x*x+1";
            Console.WriteLine("***************************************");
            Console.Write("input for Part 1 e.g.(1+3/2)*(2/2+1): ");
            var input1 = Console.ReadLine();
            try
            {
                Console.WriteLine(Part1(input1));
            }
            catch (Exception e)
            {
                Console.WriteLine("contain invalid input.");
            }

            Console.WriteLine("***************************************");

            Console.Write("input for Part 2 e.g.23245: ");

            var input2 = Console.ReadLine();
            try
            {
                Part2(input2);
            }
            catch (Exception e)
            {
                Console.WriteLine("contain invalid input.");
            }


            Console.WriteLine("***************************************");
            Console.Write("input for Part 3 (e.g.y=3x*x+2): ");
            var input = Console.ReadLine();

            Console.WriteLine("plotting function in WPF...");

            this.Title = "Part 3";
            this.Points = new List<DataPoint>();

            try
            {
                for (int x = 1; x < 100; x++)
                {
                    Points.Add(
                        new DataPoint(x, Part1(input.Replace("y=", String.Empty).Replace("x", x.ToString())))
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("contain invalid input.");
                return;
            }
        }

        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }


        static double Part1(string input)
        {
            // test
            // string input = "(1+3/2)*(2/2 +1 +1)";

            var input_format = input.Replace(" ", String.Empty);

            //TODO input validation
            if (!Regex.IsMatch(input_format, @"[\d\(\)\+\-\*\/]"))
            {
                Console.WriteLine("invalid input format. ");
                throw new Exception();
            }

            /*
            if (Regex.IsMatch(input_format, @"\d{2,}"))
            {
                Console.WriteLine("no support multi-digit number calculation. ");
                throw new Exception();
            }
            */

            var add_space = Regex.Replace(input_format, @"([\+\-\*\/\(\)])", " $1 ");
            // add_space.Split(" ").ToList().ForEach(x=> { Console.WriteLine(x); });

            var words = add_space.Split(" ").ToList();
            
            Stack<double> num = new Stack<double>();
            Stack<string> opt = new Stack<string>();

            foreach (var c in words)
            {
                if (Regex.IsMatch(c.ToString(), @"\d"))
                {
                    num.Push(Convert.ToDouble(c.ToString()));
                    continue;
                }

                if (Regex.IsMatch(c.ToString(), @"[\+\-\*\/\(\)]"))
                {
                    if (c == "(")
                    {
                        opt.Push(c);
                        continue;
                    }

                    if (c == "*" || c == "/")
                    {
                        while (opt.Any() && (opt.Peek() == "/"))
                        {
                            var n2 = num.Pop();
                            var n1 = num.Pop();
                            var optTop = opt.Pop();

                            Cal(n1, n2, optTop, num);
                        }

                        opt.Push(c);
                        continue;
                    }

                    if (c == "+" || c == "-")
                    {
                        while (opt.Any() && (opt.Peek() == "*" || opt.Peek() == "/"))
                        {
                            var n2 = num.Pop();
                            var n1 = num.Pop();
                            var optTop = opt.Pop();

                            Cal(n1, n2, optTop, num);
                        }

                        opt.Push(c);

                        continue;
                    }

                    if (c == ")")
                    {
                        while (opt.Peek() != "(")
                        {
                            var n2 = num.Pop();
                            var n1 = num.Pop();
                            var optTop = opt.Pop();

                            Cal(n1, n2, optTop, num);
                        }

                        var leftBracket = opt.Pop();
                        continue;
                    }
                }
            }


            while (opt.Count > 0)
            {
                var n2 = num.Pop();
                var n1 = num.Pop();
                var optTop = opt.Pop();

                Cal(n1, n2, optTop, num);
            }

            double result = num.Pop();
            if (num.Any())
            {
                Console.WriteLine("invalid input format. ");
                throw new Exception();
            }

            /*Console.WriteLine(result);*/
            return result;
        }

        static void Cal(double n1, double n2, string opt, Stack<double> num)
        {
            switch (opt)
            {
                case "+":
                    num.Push(n1 + n2);
                    break;
                case "-":
                    num.Push(n1 - n2);
                    break;
                case "*":
                    num.Push(n1 * n2);
                    break;
                case "/":
                    if (n2 == 0)
                    {
                        throw new DivideByZeroException();
                        return;
                    }

                    num.Push(n1 / n2);
                    break;
                default:
                    break;
            }
        

    }

        static void Part2(string input)
        {
            // var test = "23245";
            var test = input;

            if (!Regex.IsMatch(input.Trim(), @"\d"))
            {
                Console.WriteLine("input contain non-digit. ");
                throw new Exception();
            }

            var sorted = test.ToList().OrderBy(x => x).ToList();
            if (test.ToList().SequenceEqual(sorted))
            {
                test.ToList().ForEach(x => { Console.Write(x); });
                return;
            }


            var t = "0" + test;

            var l = t.ToList().Select(s => int.Parse(s.ToString())).ToList();

            int[] offs = new int[l.Count];

            for (int i = 0; i < l.Count; i++)
            {
                var off = l[i + 1] - l[i];
                offs[i] = off;

                if (off < 0)
                {
                    for (int j = offs.Length - 1; j >= 0; j--)
                    {
                        if (offs[j] > 0)
                        {
                            for (int k = 0; k < l.Count; k++)
                            {
                                if (k < j + 1)
                                {
                                    l[k] = l[k];
                                }

                                if (k == j + 1)
                                {
                                    l[k] = l[k] - 1;
                                }

                                if (k > j + 1)
                                {
                                    l[k] = 9;
                                }
                            }

                            l.Where(x => x > 0).ToList().ForEach(x => { Console.Write(x); });
                            Console.WriteLine();
                            return;
                        }
                    }
                }
            }
        }
    }
}