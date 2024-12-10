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
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/sampleData1.txt";
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/sampleData2.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/aocData1.txt"; 
        
            try {
                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data;
                    BigInteger totals = 0;
                    BigInteger totalsWithConditions = 0;
                    List<List<string>> dataList = new List<List<string>>();
                    
                    while ((data = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data.Length != 0) {
                            totals += getMultiplierData(data);
                            dataList.Add(getMultiplierDataWithConditions(data));
                        }
                    }
                    
                    Console.WriteLine("totals: {0}", totals);

                    bool skip = false;
                    string previousStep = "do"; 
                    int i = 0;
                    BigInteger multiplierData = 0; 

                    foreach (var item in dataList) {                                               
                        foreach (var item2 in item) {      
                            i++;                      
                            if (item2 == "don't()") {
                                //Console.WriteLine("i: {1}, item2: {0}", item2, i);
                                previousStep = "don't";
                                skip = true;
                                continue;
                            } else if (item2 == "do()") {
                                //Console.WriteLine("i: {1}, item2: {0}", item2, i);
                                previousStep = "do";
                                skip = false;
                                continue;
                            } else if (skip) {
                                //Console.WriteLine("i: {1}, skip -- item2: {0}", item2, i);
                                continue;
                            }
                            var mulMatches = Regex.Matches(item2, @"\d+");
                            BigInteger mul1 = BigInteger.Parse(mulMatches[0].Value);
                            BigInteger mul2 = BigInteger.Parse(mulMatches[1].Value);
                            multiplierData = multiplierData + (mul1 * mul2);
                            //Console.WriteLine("i: {4}, mul1: {0}, mul2: {1}, mul1 * mul2: {2}, multiplierData: {3}", mul1, mul2, mul1 * mul2, multiplierData, i);
                        }
                    }

                    Console.WriteLine("totalsWithConditions: {0}", multiplierData);
                }                    

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      }

        static BigInteger getMultiplierData(string data) {
            BigInteger multiplierData = 0;
            BigInteger finalMultiplierData = 0;
            int i = 0;
            string pattern = @"mul\(\d+,\d+\)"; 
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(data);
            //Console.WriteLine("matches: {0}", matches.Count);

            foreach (Match match in matches)
            {
                i++;
                //Console.WriteLine("Found match: " + match.Value);
                var mulMatches = Regex.Matches(match.Value, @"\d+");
                BigInteger mul1 = BigInteger.Parse(mulMatches[0].Value);
                BigInteger mul2 = BigInteger.Parse(mulMatches[1].Value);
                multiplierData = multiplierData + (mul1 * mul2);
                //Console.WriteLine("i: {4}, mul1: {0}, mul2: {1}, mul1 * mul2: {2}, multiplierData: {3}", mul1, mul2, mul1 * mul2, multiplierData, i);
                if (i == matches.Count) {
                    finalMultiplierData += multiplierData;
                }
            } 

            return finalMultiplierData;     
        } 

        static List<string> getMultiplierDataWithConditions(string data) {
            //BigInteger multiplierData = 0;
            //BigInteger finalMultiplierData = 0;
            //int i = 0;
            string pattern = @"mul\(\d+,\d+\)|do\(\)|don\'t\(\)"; 
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(data);
            //Console.WriteLine("matches: {0}", matches.Count);
            List<string> dataList = new List<string>();
            
            foreach (Match match in matches)
            {
                dataList.Add(match.Value);
                // i++;
            } 

            return dataList;   
        }
   }
}