using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Fraudulent_Activity_Notifications
{
    class Program
    {
        static int getMediana(int[] freq, int d)
        {
            var temp = new int[freq.Length];
            temp[0] = freq[0];
            
            if (d % 2 == 0)
            {
                int medianLeft = 0;
                int medianRight = 0;
                bool flag = true;

                for (int i = 1; i < 201; i++)
                {
                    temp[i] = temp[i - 1] + freq[i];
                    if ((temp[i] >= d / 2) && (flag))
                    {
                        medianLeft = i;
                        flag = false;
                    }
                    if (temp[i] >= d / 2 + 1)
                    { 
                        medianRight = i;
                        return medianLeft + medianRight;
                    }
                }
            }
            else
            {                
                for (int i = 1; i < 201; i++)
                {
                    temp[i] = temp[i - 1] + freq[i];
                    if (temp[i] >= d / 2 + 1)
                        return 2*i;
                }

            }
            return 0;
        }


        static int activityNotifications(int[] arr, int d)
        {
            int numNotifications = 0;

            int[] freq = new int[201];

            bool flag = true;

            int elementDel = 0;

            for (int index = d; index < arr.Length; index++)
            {
                if (flag)
                {
                    for (int i = index - d; i <= index - 1; i++)
                        freq[arr[i]]++;
                    flag = false;
                }
                else
                {
                    freq[elementDel]--;
                    freq[arr[index - 1]]++;
                }

                if (d % 2 == 0)
                {
                    if (arr[index] >= getMediana(freq, d))
                        numNotifications++;
                }
                else
                {
                    if (arr[index] >= 2 * getMediana(freq, d))
                        numNotifications++;
                }
                elementDel = arr[index - d];
            }
            return numNotifications;
        }
                          
        static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            string[] nd = Console.ReadLine().Split(' ');

            int n = Convert.ToInt32(nd[0]);
            int d = Convert.ToInt32(nd[1]);

            int[] expenditure = Array.ConvertAll(Console.ReadLine().Split(' '), expenditureTemp => Convert.ToInt32(expenditureTemp));
            int result = activityNotifications(expenditure, d);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
