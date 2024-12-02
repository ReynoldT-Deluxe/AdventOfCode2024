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
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/aocData1.txt"; 
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/sampleData2.txt";
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_3/aocData2.txt"; 

            try {
                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data;
                    int distanceTotal = 0;
                    int similarityScore = 0;

                    List<int> list1 = new List<int>();
                    List<int> list2 = new List<int>();
                    
                    while ((data = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data.Length != 0) {
                            assignDataToList(data, list1, list2);
                        }
                    }

                    //get distance total
                    distanceTotal = getDistanceTotal(list1, list2);

                    //get similarity score
                    similarityScore = getSimilarityScore(list1, list2);
                    
                    Console.WriteLine("distanceTotal: {0}", distanceTotal);
                    Console.WriteLine("similarityScore: {0}", similarityScore);
                }                    

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      }

      static void assignDataToList(string data, List<int> list1, List<int> list2) {
        //Console.WriteLine("data: {0}", data);
        string[] dataSplit = data.Split("   ");

        list1.Add(Int32.Parse(dataSplit[0]));
        list2.Add(Int32.Parse(dataSplit[1]));    
      }

      static int getDistanceTotal(List<int> list1, List<int> list2) {
        int distanceTotal = 0;

        //sort the list
        list1.Sort();
        list2.Sort();

        for (int x = 0; x < list1.Count; x++) {
            int distance = list2[x] - list1[x];
            if (distance < 0) {
                distance = distance * -1;
            }
            distanceTotal += distance;
        }

        return distanceTotal;
      }

      static int getSimilarityScore(List<int> list1, List<int> list2) {
        int similarityScore = 0;

        //sort the list
        list1.Sort();
        list2.Sort();

        for (int x = 0; x < list1.Count; x++) {
            int tempScore = 0;
            //Console.WriteLine("list1: {0}", list1[x]);
            for (int y = 0; y < list2.Count; y++) {
                //Console.WriteLine("list2: {0}", list2[y]);
                if (list1[x] == list2[y]) {
                    tempScore += 1;
                }
            }     
            similarityScore += list1[x] * tempScore;       
        }

        return similarityScore;
      }
   }
}