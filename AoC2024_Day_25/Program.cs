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
        //appended "end" at the end of the file as I was using the blank for converting the data into a string array
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_25/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_25/aocData1.txt"; 
        
        List<string[]> matrixList = new List<string[]>();

        try {
            using (StreamReader sr = new StreamReader(dataLocation)) {
                
                string data;
                int lineCount = 0;
                string[,] matrix = new string[7,5];                

                while ((data = sr.ReadLine()) != null) {
                    //Console.WriteLine(lineCount);
                    //Console.WriteLine(data);
                    if (lineCount == 0) {
                        //initialize the matrix
                        matrix = new string[7,5];
                    }
                    if (lineCount <= 7) {
                        if (lineCount < 7) {
                            // add the data to the matrix
                            matrix[lineCount, 0] = data[0].ToString();
                            matrix[lineCount, 1] = data[1].ToString();
                            matrix[lineCount, 2] = data[2].ToString();
                            matrix[lineCount, 3] = data[3].ToString();
                            matrix[lineCount, 4] = data[4].ToString();
                        } else if (lineCount == 7 || data == "end") {
                            //printMatrix(matrix);

                            //convert the matrix into a string array
                            string[] matrixString = new string[6];
                            matrixList.Add(convertMatrixToStringArray(matrix));
                        }
                        lineCount++;
                    }

                    if (lineCount > 7) {
                        lineCount = 0;
                    }
                }                    
            }

        } catch (Exception e) {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        List<string[]> keyList = new List<string[]>();
        List<string[]> lockList = new List<string[]>();
        
        foreach (string[] matrixString in matrixList) {
            if (matrixString[0] == "key") {
                keyList.Add(matrixString);
            } else if (matrixString[0] == "lock") {
                lockList.Add(matrixString);
            }
        }

        //get unique key-lock pairs
        int overlapPairCount = getKeyLockPairsWithoutOverlap(keyList, lockList);
        Console.WriteLine("overlapPairCount: " + overlapPairCount);
     }  

     static void printMatrix(string[,] matrix) {
        for (int i = 0; i < 7; i++) {
            for (int j = 0; j < 5; j++) {
                Console.Write(matrix[i, j]);
            }
            Console.WriteLine();
        }
     }

     static void printMatrixString(string[] matrixString) {
        for (int i = 0; i < 6; i++) {
            Console.Write(matrixString[i]);
        }
        Console.WriteLine();
     }

     static string[] convertMatrixToStringArray(string[,] matrix) {
        string[] matrixString = new string[6];
        //check the first row
        if (matrix[0,0] == "." && matrix[0,1] == "." && matrix[0,2] == "." && matrix[0,3] == "." && matrix[0,4] == ".") {
            matrixString[0] = "key";
        } else if (matrix[0,0] == "#" && matrix[0,1] == "#" && matrix[0,2] == "#" && matrix[0,3] == "#" && matrix[0,4] == "#") {
            matrixString[0] = "lock";
        }

        //get lock and key data
        for (int j = 0; j < 5; j++) {
            int matrixIndex = j + 1;
            //Console.WriteLine("j: {0} matrixIndex: {1}", j, matrixIndex);
            int data = 0;
            for (int i = 1; i < 6; i++) {
                //Console.WriteLine("i: {0} j: {1} input: {2}", i, j, matrix[i, j]);                    
                if (matrix[i, j] == "#") {                        
                    //Console.WriteLine(string.IsNullOrEmpty(matrixString[j]));
                    if (!string.IsNullOrEmpty(matrixString[matrixIndex])) {
                        data = Int32.Parse(matrixString[matrixIndex]);
                        data++;
                    } else {
                        data = 1;
                    }
                }
                matrixString[matrixIndex] = data.ToString();
            }

        }

        //printMatrixString(matrixString);
        return matrixString;
     }

     static int getKeyLockPairsWithoutOverlap(List<string[]> keyList, List<string[]> lockList) {
        int pairCount = 0;

        foreach (string[] keyData in keyList) {
            //printMatrixString(keyData);
            foreach (string[] lockData in lockList) {    
                int overlapCount = 0;      
                //printMatrixString(lockData);   
                for (int i = 1; i < 6; i++) {
                    int keyInt = Int32.Parse(keyData[i]);
                    int lockInt = Int32.Parse(lockData[i]);
                    if (keyInt + lockInt > 5) {
                        overlapCount++;
                    }
                }
                if (overlapCount == 0) {
                pairCount++;
                }
            }            
        }

        return pairCount;
     }

   }//end class
}//end namespace