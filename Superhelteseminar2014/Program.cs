using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace Superhelteseminar2014
{
    class Program
    {
        static bool Issymmectric(int[,] data)
        {
            if (data.GetLength(0) != data.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("data", "Array er ikke spejlbart");
            }

            int size = data.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    if (!data[i, j].Equals(data[j, i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static int[,] GenrateData(int size, bool symmetric, bool malformed)
        {
            Random rnd = new Random();

            int[,] data = new int[size, size];

            if (malformed)
            {
                int deviation = rnd.Next(size / 5);
                data = new int[rnd.Next(size - (size / 5), size + (size / 5)), rnd.Next(size - (size / 5), size + (size / 5))];
            }

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (malformed || !symmetric)
                    {
                        data[i, j] = rnd.Next();
                    }
                    else
                    {
                        if (i <= j)
                        {
                            data[i, j] = rnd.Next();
                        }
                        else
                        {
                            data[i, j] = data[j, i];
                        }
                    }
                }
            }

            return data;
        }

        static void Save(string path, int[,] data)
        {
            var lines = new List<string>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                var numbers = new List<int>();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    numbers.Add(data[i, j]);
                }
                lines.Add(String.Join(",", numbers));
            }
            System.IO.File.WriteAllLines(path, lines);
        }

        static int[,] Load(string path)
        {
            var lines = System.IO.File.ReadAllLines(path);
            var data = new List<int[]>();
            foreach (var line in lines)
            {
                data.Add(line.Split(',').Select(x => Convert.ToInt32(x)).ToArray());
            }

            var returndata = new int[data.Count, data[0].Length];
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    returndata[i, j] = data[i][j];
                }
            }
            return returndata;
        }

        static void Main(string[] args)
        {
            int matixSize = 200;
            //var data1 = GenrateData(matixSize, true, false);
            //Save("data1.txt", data1);
            //var data2 = GenrateData(matixSize, false, false);
            //Save("data2.txt", data2);
            var data3 = GenrateData(matixSize, true, true);
            Save("data3.txt", data3);

            var inputs = new List<string>
            {
                "data1.txt",
                "data1_LF.txt",
                "data1_CR.txt",
                "data2.txt",
                "data2_LF.txt",
                "data2_CR.txt",
                "data3.txt",
                "data3_LF.txt",
                "data3_CR.txt",
            };

            foreach (var input in inputs)
            {
                try
                {
                    Console.WriteLine(Issymmectric(Load(input)) ? "Symetrisk" : "ikke symetrisk");
                }
                catch
                {
                    Console.WriteLine("ikke spejlbar");
                }
            }
        }
    }
}
