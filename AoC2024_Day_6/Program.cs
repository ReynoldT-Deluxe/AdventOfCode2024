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
        //note: padded the border with a 0 to avoid out of bounds
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_6/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_6/aocData1.txt"; 
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_6/sampleData2.txt";
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_6/aocData2.txt"; 

        //int mapLimits = 12;
        int mapLimits = 132;

        string [,] map = new string[mapLimits,mapLimits];
        getMapData(dataLocation, map);

        //printMap(map, mapLimits);
        int[] startLocation = getStartLocation(map, mapLimits);

        //start the journey
        move(startLocation, map);
        //printMap(map, mapLimits);

        //count the number of X's
        int distinctPositions = countDistinctPositions(map, mapLimits);
        Console.WriteLine("Distinct positions: {0}", distinctPositions);

        try {
            using (StreamReader sr = new StreamReader(dataLocation)) {
                
                string data;
                int row = 0;
                while ((data = sr.ReadLine()) != null) {
                    //check data if it's blank 
                    if (data.Length != 0) {
                        assignDataToMap(data, row, map);
                    }
                    row++;
                }                    
            }                                

        } catch (Exception e) {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        
        //check looping by adding obstacles
        //int stepCount = 90;
        int stepCount = 15000;
        int loopCount = countLoops(dataLocation, mapLimits, stepCount);
        Console.WriteLine("Loop count: {0}", loopCount);   
      }

      static void getMapData(string dataLocation, string[,] map) {
         try {
            using (StreamReader sr = new StreamReader(dataLocation)) {
                
                string data;
                int row = 0;
                while ((data = sr.ReadLine()) != null) {
                    //check data if it's blank 
                    if (data.Length != 0) {
                        assignDataToMap(data, row, map);
                    }
                    row++;
                }                    
            }                                

        } catch (Exception e) {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
      }

      static void assignDataToMap(string data, int row, string[,] map) {
        //Console.WriteLine("data: {0}", data);
        for (int x = 0; x < data.Length; x++) {
            map[row, x] = data[x].ToString();
        }
      }

      static void printMap(string[,] map, int mapLimits) {
        for (int x = 0; x < mapLimits; x++) {
            for (int y = 0; y < mapLimits; y++) {
                Console.Write(map[x,y]);
            }
            Console.WriteLine();
        }
      }

      static int[] getStartLocation(string[,] map, int mapLimits) {
        int[] startLocation = new int[2];
        for (int i = 0; i < mapLimits; i++) {
            for (int j = 0; j < mapLimits; j++) {
                if (map[i,j] == "^") {
                   startLocation[0] = i;
                   startLocation[1] = j;
                   //Console.WriteLine("start loction row: {0}, col: {1}", i, j);
                   break;
                }
            }
        }
        return startLocation;
      }

      //used to do the journey
      //added the step count to know the limits to use for 2nd part
      //max step count would be calculated as stepCount * 2
      static void move(int[] startLocation, string[,] map) {
        string nextSpace = ".";
        string currentDirection = "up";
        int[] currentLocation = startLocation;
        int stepCount = 0;
        while (nextSpace != "0") {
            //replace current location with an 'X'
            map[currentLocation[0], currentLocation[1]] = "X";
            stepCount++;
            //set the next location as the current location
            nextSpace = getNextSpace(currentLocation, map, currentDirection);
            //check if the next space is the end of the journey or there is a need to change direction           
            if (nextSpace == "0") {
                //Console.WriteLine("End of the journey");
                break;
            }
            if (nextSpace == "#") {
                currentDirection = getNewDirection(currentDirection);
            }
            //set current location to the next location
            if (currentDirection == "up") {
                currentLocation[0] = currentLocation[0] - 1;
                currentLocation[1] = currentLocation[1];
            } else if (currentDirection == "down") {
                currentLocation[0] = currentLocation[0] + 1;
                currentLocation[1] = currentLocation[1];
            } else if (currentDirection == "left") {
                currentLocation[0] = currentLocation[0];
                currentLocation[1] = currentLocation[1] - 1;
            } else if (currentDirection == "right") {
                currentLocation[0] = currentLocation[0];
                currentLocation[1] = currentLocation[1] + 1;
            }
        }
       //Console.WriteLine("Step count: {0}", stepCount);
      }

      static string getNextSpace(int[] startLocation, string[,] map, string currentDirection) {
        string nextSpace = "";
        if (currentDirection == "up") {
            nextSpace = map[startLocation[0] - 1, startLocation[1]];
        }
        if (currentDirection == "down") {
            nextSpace = map[startLocation[0] + 1, startLocation[1]];
        }
        if (currentDirection == "left") {
            nextSpace = map[startLocation[0], startLocation[1] - 1];
        }
        if (currentDirection == "right") {
            nextSpace = map[startLocation[0], startLocation[1] + 1];
        }
        return nextSpace;
      }

      static string getNewDirection(string currentDirection) {
        string newDirection = "";
        if (currentDirection == "up") {
            newDirection = "right";
        } else if (currentDirection == "down") {
            newDirection = "left";
        } else if (currentDirection == "left") {
            newDirection = "up";
        } else if (currentDirection == "right") {
            newDirection = "down";
        }
        return newDirection;
      }

      static int countDistinctPositions(string[,] map, int mapLimits) {
        int count = 0;
        for (int i = 0; i < mapLimits; i++) {
            for (int j = 0; j < mapLimits; j++) {
                if (map[i,j] == "X") {
                   count++;
                }
            }
        }
        return count;
      }

      static int countLoops(string dataLocation, int mapLimits, int stepCount) {
        int count = 0;
        for (int i = 0; i < mapLimits; i++) {
            for (int j = 0; j < mapLimits; j++) {
                //writing so it doesn't look like it's stuck
                Console.Write(".");
                //Console.WriteLine("i: {0}, j: {1}", i, j);
                //need to reset the map
                string [,] map = new string[mapLimits,mapLimits];
                getMapData(dataLocation, map);
                if (map[i,j] != "^" && map[i,j] != "0") {
                    map[i,j] = "O";
                } else {
                    continue;
                }
                int[] startLocation = getStartLocation(map, mapLimits);
                count += moveWithStepCount(startLocation, map, stepCount);
            }
        }
        Console.WriteLine();
        return count;
      }

      static int moveWithStepCount(int[] startLocation, string[,] map, int stepCount) {
        string nextSpace = ".";
        string currentDirection = "up";
        int[] currentLocation = startLocation;
        int stepCounter = 0;
        while (nextSpace != "0") {
            if (stepCounter > stepCount) {
                //most likely a loop
                return 1;
            } else {
                stepCounter++;
                //Console.WriteLine("stepCounter: {0}", stepCounter);
            }
            //set the next location as the current location
            nextSpace = getNextSpace(currentLocation, map, currentDirection);
            //check if the next space is the end of the journey or there is a need to change direction           
            if (nextSpace == "0") {
                //Console.WriteLine("End of the journey");
                //reached the limits
                return 0;
            }
            if (nextSpace == "#" || nextSpace == "O") {
                currentDirection = getNewDirection(currentDirection);
            }
            //set current location to the next location
            if (currentDirection == "up") {
                currentLocation[0] = currentLocation[0] - 1;
                currentLocation[1] = currentLocation[1];
            } else if (currentDirection == "down") {
                currentLocation[0] = currentLocation[0] + 1;
                currentLocation[1] = currentLocation[1];
            } else if (currentDirection == "left") {
                currentLocation[0] = currentLocation[0];
                currentLocation[1] = currentLocation[1] - 1;
            } else if (currentDirection == "right") {
                currentLocation[0] = currentLocation[0];
                currentLocation[1] = currentLocation[1] + 1;
            }
        }
        //most likely a loop
        return 1;
      }
      
   }//end of class
}//end of namespace