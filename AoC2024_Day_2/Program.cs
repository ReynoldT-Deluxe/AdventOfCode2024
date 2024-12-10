using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;

namespace FileApplication {
   class Program {
      static void Main(string[] args) {
        string dataLocation = "";
        
        //data to use
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_2/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_2/aocData1.txt"; 
       
            try {
                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data;
                    int distanceTotal = 0;
                    int similarityScore = 0;

                    int safeCount = 0;
                    int problemDampenerSafeCount = 0;
                    
                    while ((data = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data.Length != 0) {
                            safeCount += determineSafeReport(data);
                            problemDampenerSafeCount += determineSafeReportWithDampener(data);
                        }
                    }
                    Console.WriteLine("safeCount: {0}", safeCount);
                    Console.WriteLine("problemDampenerSafeCount: {0}", problemDampenerSafeCount);
                }                    

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      }

      static int determineSafeReport(string data) {
        // Console.WriteLine("data: {0}", data);

        string[] dataSplit = data.Split(" ");
        List<int> level = new List<int>();

        foreach (string item in dataSplit) {
            // Console.WriteLine("item: {0}", item);
            level.Add(Int32.Parse(item));
        }
       return processLevel(level);       
      }

      static int processLevel(List<int> level) {
            bool isSafe = false;
            bool isIncreasing = false;
            if (level[0] < level[1]) {
                isIncreasing = true;
            }
            // Console.WriteLine("isIncreasing: {0}", isIncreasing);

            for (int x = 0; x < level.Count - 1; x++) {
                // Console.WriteLine("x: {0}", x);
                // Console.WriteLine("level[x]: {0}", level[x]);
                // Console.WriteLine("level[x+1]: {0}", level[x+1]);
                int checkLevel = level[x] - level[x+1];
                // Console.WriteLine("checkLevel: {0}", checkLevel);
                
                if (isIncreasing) {
                    if (checkLevel < 0) {
                        checkLevel = checkLevel * -1;
                    } else {
                        isSafe = false;
                        break;
                    }
                } else {
                    if (checkLevel < 0) {
                        isSafe = false;
                        break;
                    }
                }

                // Console.WriteLine("checkLevel2: {0}", checkLevel);
                
                if (checkLevel <= 3 && checkLevel > 0) {
                    isSafe = true;
                } else {
                    isSafe = false;
                    break;
                }
            }

            if (isSafe) {
                //Console.WriteLine(" safe");
                return 1;
            } else {
                //Console.WriteLine(" not safe");
                return 0;
            }
        }
        
        static int determineSafeReportWithDampener(string data) {
        // Console.WriteLine("data: {0}", data);

        bool isSafe = false;
        int dampenerCount = 0;
        string[] dataSplit = data.Split(" ");
        List<int> level = new List<int>();

        foreach (string item in dataSplit) {
            // Console.WriteLine("item: {0}", item);
            level.Add(Int32.Parse(item));
        }
       
        bool isIncreasing = false;
        if (level[0] < level[1]) {
            isIncreasing = true;
        }
        // Console.WriteLine("isIncreasing: {0}", isIncreasing);

        for (int x = 0; x < level.Count - 1; x++) {
            // Console.WriteLine("x: {0}", x);
            // Console.WriteLine("level[x]: {0}", level[x]);
            // Console.WriteLine("level[x+1]: {0}", level[x+1]);
            int checkLevel = level[x] - level[x+1];
            // Console.WriteLine("checkLevel: {0}", checkLevel);
            
            if (isIncreasing) {
                if (checkLevel < 0) {
                    checkLevel = checkLevel * -1;
                } else {
                    dampenerCount++;
                }
            } else {
                if (checkLevel < 0) {
                    dampenerCount++;
                }
            }

            // Console.WriteLine("checkLevel2: {0}", checkLevel);
            
            if (checkLevel <= 3 && checkLevel > 0) {
                isSafe = true;
            } else {
                dampenerCount++;
            }
        }

        // Console.WriteLine("dampenerCount: {0}", dampenerCount);
        // Console.WriteLine("xbit: {0}", xbit);

        if (isSafe && dampenerCount == 0) {
            // Console.WriteLine(" safe");
            return 1;
        } else {
            // Console.Write("level before: ");
            // foreach (int item in level) {
            //     Console.Write("{0} ", item);
            // }
            for (int x = 0; x < level.Count; x++) {
                List<int> damperTestLevel = new List<int>(level);
                damperTestLevel.RemoveAt(x);
                //  Console.Write("level after {0}:", x);
                //     foreach (int item in damperTestLevel) {
                //         Console.Write("{0} ", item);
                //     }
                //     Console.WriteLine();
                    //Console.WriteLine("damperTestLevel.Count: {0}", damperTestLevel.Count);
                int testCount = countDamperTestLevel(damperTestLevel);
                if (testCount == damperTestLevel.Count -1) {
                    //Console.WriteLine(" safe");
                    return 1;
                }
            }
            //Console.WriteLine();
            return 0;
        }
      }

      static int countDamperTestLevel (List<int> level) {
        int levelCount = 0;
        bool isIncreasing = false;
        bool isSafe = true;
        if (level[0] < level[1]) {
            isIncreasing = true;
        }

        for (int x = 0; x < level.Count - 1; x++) {
            int checkLevel = level[x] - level[x+1];
            
            if (isIncreasing) {
                if (checkLevel < 0) {
                    checkLevel = checkLevel * -1;
                } else {
                    isSafe = false;
                    break;
                }
            } else {
                if (checkLevel < 0) {
                    isSafe = false;
                    break;
                }
            }

            // Console.WriteLine("checkLevel2: {0}", checkLevel);
            
            if (checkLevel <= 3 && checkLevel > 0) {
                levelCount++;
            } else {
                isSafe = false;
                break;
            }
        }
        //Console.WriteLine("levelCount: {0}", levelCount);
        if (isSafe) {
            //Console.WriteLine(" safe");
            return levelCount;
        } else {
            //Console.WriteLine(" not safe");
            return 0;
        }
      }
   }
}