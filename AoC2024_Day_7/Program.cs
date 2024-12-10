using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Text;

namespace FileApplication {
   class Program {
      static void Main(string[] args) {
        string dataLocation = "";
        
        //data to use
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_7/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_7/aocData1.txt"; 
        
            try {
                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data = "";
                    long sumOfBinaryResults = 0;
                    long sumOfTernaryResults = 0;
                    
                    while ((data = sr.ReadLine()) != null) {
                        //to show the progress
                        Console.Write(".");
                        //check data if it's blank 
                        if (data.Length != 0) {
                            sumOfBinaryResults += getBinaryCalibrationValidation(data);
                            sumOfTernaryResults += getTernaryCalibrationValidation(data);
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Sum of the binary results: " + sumOfBinaryResults);
                    Console.WriteLine("Sum of the ternary results: " + sumOfTernaryResults);
                }                    

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      }

      static long getBinaryCalibrationValidation(string data) {
        string[] splitData = data.Split(": ");
        long checkData = Int64.Parse(splitData[0]);
        
        //get the check list
        List<string> checkList = splitData[1].Split(" ").ToList();
        //print checkList
        // foreach (string item in checkList) {    
        //     Console.Write("{0} ", item);
        // }    

        int bitCount = checkList.Count - 1;

        //get the binary list
        List<string> binaryList = getBinaryList(bitCount);

        bool isPassed = false;

        foreach (string binary in binaryList) {
            //Console.WriteLine(binary);
            long checkSum = Int64.Parse(checkList[0]);
            for (int i = 0; i < binary.Length; i++) {
                //Console.WriteLine("checkSumB: ", checkSum);
                //Console.WriteLine("binaryData: ", binary);
                if (binary[i].ToString() == "0") {
                    checkSum += Int64.Parse(checkList[i+1]);
                } else {
                    checkSum = checkSum * Int64.Parse(checkList[i+1]);
                }
            }
            if (checkSum == checkData) {
                isPassed = true;
                break;
            }
        }

        if (isPassed) {
            return checkData;
        } else {
            return 0;
        }
      }

      static long getTernaryCalibrationValidation(string data) {
        string[] splitData = data.Split(": ");
        long checkData = Int64.Parse(splitData[0]);
        
        //get the check list
        List<string> checkList = splitData[1].Split(" ").ToList();
        //print checkList
        // foreach (string item in checkList) {    
        //     Console.Write("{0} ", item);
        // }    

        int bitCount = checkList.Count - 1;

        //get the ternary list
        List<string> ternaryList = getTernaryList(bitCount);

        //write down the formula
        foreach (string ternary in ternaryList) {
            StringBuilder formula = new StringBuilder(checkList[0]);  
            for (int i = 0; i < ternary.Length; i++) {
                if (ternary[i].ToString() == "0") {
                    formula.Append(" + " + checkList[i+1]);
                } else if (ternary[i].ToString() == "1") {
                    formula.Append(" * " + checkList[i+1]);
                } else {
                    formula.Append(" || " + checkList[i+1]);
                }
            }
            //Console.WriteLine(formula);
            if (isPassingFormula(formula.ToString(), checkData)) {
                return checkData;
            } else {
                continue;
            }
        }
        return 0;
      }

      static List<string> getBinaryList(int operatorCount) {
        List<string> list = new List<string>();
		int n = operatorCount;
		for (int i = 0; i < (1 << n); i++) {
			string binary = Convert.ToString(i, 2).PadLeft(n, '0');
			list.Add(binary); 
            //Console.WriteLine(binary);
		}

        //print the list
        // foreach (string item in list) {
        //     Console.WriteLine(item);
        // }
        return list;
      }

      static List<string> getTernaryList(int n)
        {
            List<string> list = new List<string>();
            int max = (int)Math.Pow(3, n);
            for (int i = 0; i < max; i++)
            {
                list.Add(getTernaryNumber(i, n));
            }
            return list;
        }

      static string getTernaryNumber(int num, int digits)
        {
            char[] ternary = new char[digits];
            for (int i = 0; i < digits; i++)
            {
                ternary[digits - i - 1] = (char)('0' + num % 3);
                num /= 3;
            }
            string result = new string(ternary);
            return result;
        }
    
      static bool isPassingFormula(string formula, long checkData) {        
        bool isPassed = false;
        //single value
        if (!formula.Contains("+") && !formula.Contains("*") && !formula.Contains("||")) {
            if (Int64.Parse(formula) == checkData) {
                isPassed = true;
            }
        } else {
            string[] splitFormula = formula.Split(" ");
            long totals = Int64.Parse(splitFormula[0]);
            //Console.WriteLine("splitFormula[0]: " + totals);
            for (int i = 1; i < splitFormula.Length; i++) {
                //Console.WriteLine("splitFormula[{0}]:  {1}", i, splitFormula[i]);
                if (splitFormula[i] == "+") {
                    //Console.WriteLine("splitFormula[{0}]:  {1}", i+1, splitFormula[i+1]);
                    totals += Int64.Parse(splitFormula[i+1]);
                } else if (splitFormula[i] == "*") {
                    //Console.WriteLine("splitFormula[{0}]:  {1}", i+1, splitFormula[i+1]);
                    totals = totals * Int64.Parse(splitFormula[i+1]);
                } else if (splitFormula[i] == "||") {
                    //Console.WriteLine("splitFormula[{0}]:  {1}", i+1, splitFormula[i+1]);
                    StringBuilder sb = new StringBuilder(totals.ToString());
                    sb.Append(Int64.Parse(splitFormula[i+1]).ToString());
                    totals = Int64.Parse(sb.ToString());
                }
                i++;
            }
            //Console.WriteLine("totals: {0} checkData: {1}", totals, checkData);
            if (totals == checkData) {
                return true;
            }
        }
        return isPassed;        
      }
      
   }//end of class
}//end of namespace
