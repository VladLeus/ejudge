using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ejudge
{


    class Program
    {

        public static List<double> res = new List<double>();
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            List<double[]> arr = new List<double[]>();
            for (int i = 0; i < n; i++)
            {
                double[] temp = new double[2];
                string[] str = Console.ReadLine().Trim().Split();
                for (int j = 0; j < 2; j++)
                {
                    var d = Convert.ToDouble(str[j]);
                    temp[j] = d;
                }
                arr.Add(temp);
            }
            GetAllPossibleWays(arr, 0, n);
            Console.WriteLine(Math.Round(res.Min(), 11));
        }


        public static List<double[]> GetAllPossibleWays(List<double[]> array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var midlle = (rightIndex + leftIndex) / 2;
            int j = midlle;

            do
            {
                double temp = Math.Abs((array[i][0] - array[j][0]) * (array[j][0] - array[i][0]) + (array[i][1] - array[j][1]) * (array[j][1] - array[i][1]));
                res.Add(Math.Sqrt(temp));
                i++;
                j++;
            } while (i < midlle && j < rightIndex);
            

            if (i < midlle)
                GetAllPossibleWays(array, leftIndex, midlle);

            if (j < rightIndex)
                GetAllPossibleWays(array, midlle, rightIndex);

            return array;
        }

    }
}