using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Text.RegularExpressions;
using System.Numerics;

namespace FileApplication {
   class Program {
      static void Main(string[] args) {
        string dataLocation = "";
        
        //data to use
        //appended "end" at the end of the file as I was using the blank for calculating the token count
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_13/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_13/aocData1.txt"; 
        
        try {
            using (StreamReader sr = new StreamReader(dataLocation)) {
                
                string data;
                double[] buttonA = new double[2];
                double[] buttonB = new double[2];
                double[] prizeLoc = new double[2];
                double[] prizeLoc2 = new double[2];
                double tokencount = 0;
                double tokencount2 = 0;
                                    
                while ((data = sr.ReadLine()) != null) {
                    if (data.StartsWith("Button A")) {
                        data.Replace("Button A: ", "");
                        string[] buttonSplitA = data.Split(", ");
                        string[] buttonDataA0 = buttonSplitA[0].Split("+");
                        string[] buttonDataA1 = buttonSplitA[1].Split("+");
                        buttonA[0] = Int64.Parse(buttonDataA0[1]);
                        buttonA[1] = Int64.Parse(buttonDataA1[1]);
                    } else if (data.StartsWith("Button B")) {
                        data.Replace("Button A: ", "");
                        string[] buttonSplitB = data.Split(", ");
                        string[] buttonDataB0 = buttonSplitB[0].Split("+");
                        string[] buttonDataB1 = buttonSplitB[1].Split("+");
                        buttonB[0] = Int64.Parse(buttonDataB0[1]);
                        buttonB[1] = Int64.Parse(buttonDataB1[1]);
                    } else if (data.StartsWith("Prize")) {
                        data.Replace("Prize: ", "");
                        string[] prizeSplit = data.Split(", ");
                        string[] prizeData0 = prizeSplit[0].Split("=");
                        string[] prizeData1 = prizeSplit[1].Split("=");
                        prizeLoc[0] = Int64.Parse(prizeData0[1]);
                        prizeLoc[1] = Int64.Parse(prizeData1[1]);
                        //add 10000000000000 to the prize location to avoid conversion error
                        double P0 = 10000000000000 + Int64.Parse(prizeData0[1]);
                        double P1 = 10000000000000 + Int64.Parse(prizeData1[1]);
                        prizeLoc2[0] = P0;
                        prizeLoc2[1] = P1;
                    } else {
                        //check data if it's blank 
                        if (data.Length == 0  || data.StartsWith("end")) {
                            tokencount += getTokenCount(buttonA, buttonB, prizeLoc);
                            tokencount2 += getTokenCount(buttonA, buttonB, prizeLoc2);
                        }
                    }
                }                    

                Console.WriteLine("Total Token Count: " + tokencount);
                Console.WriteLine("Total Token Count With Conversion Error: " + tokencount2);
            }

        } catch (Exception e) {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
     }  

     static double getTokenCount(double[] buttonA, double[] buttonB, double[] prizeLoc) {
        double tokenCount = 0;

        double A0 = buttonA[0];
        double A1 = buttonA[1];
        double B0 = buttonB[0];
        double B1 = buttonB[1];
        double P0 = prizeLoc[0];
        double P1 = prizeLoc[1];
        //Console.WriteLine("A0: " + A0 + " A1: " + A1 + " B0: " + B0 + " B1: " + B1 + " P0: " + P0 + " P1: " + P1);

        //initial solution
        //calculate B token count
        double BCount = ((A1 * P0) - (A0 * P1)) / ((A1 * B0) - (A0 * B1));

        if (BCount > 0 && BCount % 1 == 0) {
            //Console.WriteLine("B Count: " + BCount);
            //calculate A token count
            double ACount = (P0 - (B0 * BCount)) / A0;
            if (ACount > 0 && ACount % 1 == 0) {
                //calculate total token count
                tokenCount = ((ACount * 3) + BCount);
                //Console.WriteLine("A: " + ACount + " B: " + BCount + " Total: " + tokenCount);
            }
        }        

        return tokenCount;
     }

   }//end class
}//end namespace