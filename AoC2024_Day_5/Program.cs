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
        string dataLocation2 = "";
        
        //data to use
        //note: separated the page rules and print list data into 2 input files for easier processing
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_5/sampleData1.txt";     
        //dataLocation2 = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_5/sampleData2.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_5/aocData1.txt"; 
        dataLocation2 = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_5/aocData2.txt"; 

            try {
                List<string[]> pageRules = new List<string[]>();
                List<string[]> printList = new List<string[]>();

                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data;
                    while ((data = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data.Length != 0) {
                            pageRules.Add(assignPageRules(data));
                        }
                    }

                    //print page rules
                    // for (int x = 0; x < pageRules.Count; x++) {
                    //     Console.Write("pageRules{0}: ", x+1);
                    //     foreach(string item in pageRules[x]) {
                    //         Console.Write("{0} ", item);
                    //     }
                    //     Console.WriteLine();
                    // }
                }       

                using (StreamReader sr = new StreamReader(dataLocation2)) {
                    
                    string data2;
                    while ((data2 = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data2.Length != 0) {
                            printList.Add(assignPrintList(data2));
                        }
                    }

                    //print print list
                    // for (int x = 0; x < printList.Count; x++) {
                    //     Console.Write("printList{0}: ", x+1);
                    //     foreach(string item in printList[x]) {
                    //         Console.Write("{0} ", item);
                    //     }
                    //     Console.WriteLine();
                    // }
                } 

                //Console.WriteLine("data: {0} {1}", pageRules.Count, printList.Count);

                //get middle total
                int middleTotal = getMiddleTotal(pageRules, printList);
                Console.WriteLine("middleTotal: {0}", middleTotal);

                //I didn't work on part 2 (T.T)

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      }

      static string[] assignPageRules(string data) {        
        string[] dataSplit = data.Split("|");
        string[] pageRules = {dataSplit[0], dataSplit[1]}; 
        //Console.WriteLine("pageRules: {0} {1}", pageRules[0], pageRules[1]);
        return pageRules;
      }

      static string[] assignPrintList(string data) {        
        string[] dataSplit = data.Split(",");
        string[] printRules = new string[dataSplit.Length];

        for (int x = 0; x < dataSplit.Length; x++) {
            printRules[x] = dataSplit[x];
        }
        return printRules;
      }

      static int getMiddleTotal(List<string[]> pageRules, List<string[]> printList) {
        int middleTotal = 0;
        bool ruleBroken = false;
        int[] ruleBreakCount = new int[printList.Count];

        //iterate thru printList
        for (int x = 0; x < pageRules.Count; x++) {
            //Console.WriteLine("pageRules {0}: {1}|{2}", x+1, pageRules[x][0], pageRules[x][1]);
            //iterate thru list of items
            for (int y = 0; y < printList.Count; y++) {
                //Console.Write("printList{0}: ", y+1);
                foreach(string item in printList[y]) {
                    //Console.Write("{0} ", item);
                }
                //Console.WriteLine();
                //iterate forward
                for (int z = 1; z < printList[y].Length; z++) {
                    if (printList[y][z] == pageRules[x][1]) {
                        //Console.WriteLine("index data:{0} ", printList[y][z]);
                        int index = Array.IndexOf(printList[y], pageRules[x][0]);
                        //Console.WriteLine("index: {0}", index);
                        if (index > z) {
                            ruleBreakCount[y]++;
                        }
                    } else {
                        continue;
                    }                    
                }   
                //iterate backward
                for (int z = printList[y].Length - 1; z > 0; z--) {
                    if (printList[y][z] == pageRules[x][0]) {
                        //Console.WriteLine("index data:{0} ", printList[y][z]);
                        int index = Array.IndexOf(printList[y], pageRules[x][1]);
                        //Console.WriteLine("index: {0}", index);
                        if (index < z && index != -1) {
                            ruleBreakCount[y]++;
                        }
                    } else {
                        continue;
                    }                    
                }       
            }            
        }

        for (int cnt = 0; cnt < ruleBreakCount.Length; cnt++) {
            //Console.WriteLine("ruleBreakCount: {0}", item);
            if (ruleBreakCount[cnt] == 0) {
                int middleInt = printList[cnt].Length / 2;
                string middleData = printList[cnt][middleInt];
                //Console.WriteLine("middle: {0}", Int32.Parse(middleData));
                middleTotal += Int32.Parse(middleData);
            }
        }

        return middleTotal;
      }

   }//end of class
}//end of namespace