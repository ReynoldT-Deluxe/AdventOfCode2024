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
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_4/sampleData1.txt";
        dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_4/aocData1.txt"; 
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_4/sampleData2.txt";
        //dataLocation = "/Users/t452172/Documents/Personal/Advent_of_Code/2024/AdventOfCode2024/AoC2024_Day_4/aocData2.txt"; 

            try {
                using (StreamReader sr = new StreamReader(dataLocation)) {
                    
                    string data;
                    //sample data
                    //int matrixLimit = s;
                    //aoc data
                    int matrixLimit = 142;

                    int dataRow = 0;
                    string[,] arr = new string[matrixLimit, matrixLimit];

                    while ((data = sr.ReadLine()) != null) {
                        //check data if it's blank 
                        if (data.Length != 0) {
                            for (int dataCol = 0; dataCol < data.Length; dataCol++) {
                                char temp = data[dataCol];
                                //Console.WriteLine("dataRow: {0}, dataCol: {1}, temp: {2}", dataRow, dataCol, temp);
                                arr[dataRow, dataCol] = temp.ToString();
                            }
                        }
                        dataRow++;
                    }

                    //print matrix
                    // for (int col = 0; col < matrixLimit; col++) {
                    //     for (int row = 0; row < matrixLimit; row++) {
                    //         Console.Write(arr[row, col]);
                    //     }
                    //     Console.WriteLine();
                    // }

                    //get XMAS count
                    int xmasCount = getXMASCount(arr, matrixLimit);
                    //get X-MAS count
                    int x_masCount = getX_MASCount(arr, matrixLimit);
                    
                    Console.WriteLine("xmasCount: {0}", xmasCount);
                    Console.WriteLine("x_masCount: {0}", x_masCount);
                }                    

            } catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
      } 

      static int getXMASCount(string[,] arr, int matrixLimit) {
        int xmasCount = 0;

        for (int row = 0; row < matrixLimit; row++) {
            for (int col = 0; col < matrixLimit; col++) {
                if (arr[row, col] != "X") {
                    continue;
                } else {
                    //check XMAS to the right
                    if (foundRight(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the left
                    if (foundLeft(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the top 
                    if (foundTop(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the bottom
                    if (foundBottom(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the top right
                    if (foundTopRight(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the top left    
                    if (foundTopLeft(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the bottom right
                    if (foundBottomRight(arr, row, col)) {
                        xmasCount++;
                    }
                    //check XMAS to the bottom left
                    if (foundBottomLeft(arr, row, col)) {
                        xmasCount++;
                    }
                }
            }
        }
        return xmasCount;
      }

      static bool foundRight(string[,] arr, int row, int col) {        
        if (arr[row, col + 1] == "M") {
            if (arr[row, col + 2] == ".") {
                return false;
            } else if (arr[row, col + 2] == "A") {
                if (arr[row, col + 3] == ".") {
                    return false;
                } else if (arr[row, col + 3] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row, col + 1] == ".") {
            return false;
        }
        return false;
      }

      static bool foundLeft(string[,] arr, int row, int col) {        
        if (arr[row, col - 1] == "M") {
            if (arr[row, col - 2] == ".") {
                return false;
            } else if (arr[row, col - 2] == "A") {
                if (arr[row, col - 3] == ".") {
                    return false;
                } else if (arr[row, col - 3] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row, col - 1] == ".") {
            return false;
        }
        return false;
      }

      static bool foundTop(string[,] arr, int row, int col) {        
        if (arr[row - 1, col] == "M") {
            if (arr[row - 2, col] == ".") {
                return false;
            } else if (arr[row - 2, col] == "A") {
                if (arr[row - 3, col] == ".") {
                    return false;
                } else if (arr[row - 3, col] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row - 1, col] == ".") {
            return false;
        }
        return false;
      }

      static bool foundBottom(string[,] arr, int row, int col) {        
        if (arr[row + 1, col] == "M") {
            if (arr[row + 2, col] == ".") {
                return false;
            } else if (arr[row + 2, col] == "A") {
                if (arr[row + 3, col] == ".") {
                    return false;
                } else if (arr[row + 3, col] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row + 1, col] == ".") {
            return false;
        }
        return false;
      }

      static bool foundTopRight(string[,] arr, int row, int col) {        
        if (arr[row - 1, col + 1] == "M") {
            if (arr[row - 2, col + 2] == ".") {
                return false;
            } else if (arr[row - 2, col + 2] == "A") {
                if (arr[row - 3, col + 3] == ".") {
                    return false;
                } else if (arr[row - 3, col + 3] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row - 1, col + 1] == ".") {
            return false;
        }
        return false;
      }

      static bool foundBottomRight(string[,] arr, int row, int col) {        
        if (arr[row + 1, col + 1] == "M") {
            if (arr[row + 2, col + 2] == ".") {
                return false;
            } else if (arr[row + 2, col + 2] == "A") {
                if (arr[row + 3, col + 3] == ".") {
                    return false;
                } else if (arr[row + 3, col + 3] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row + 1, col + 1] == ".") {
            return false;
        }
        return false;
      }

      static bool foundTopLeft(string[,] arr, int row, int col) {        
        if (arr[row - 1, col - 1] == "M") {
            if (arr[row - 2, col - 2] == ".") {
                return false;
            } else if (arr[row - 2, col - 2] == "A") {
                if (arr[row - 3, col - 3] == ".") {
                    return false;
                } else if (arr[row - 3, col - 3] == "S") {
                    return true;
                }
            } else {
                return false;
            }
        } else if (arr[row - 1, col - 1] == ".") {
            return false;
        }
        return false;
      }

      static bool foundBottomLeft(string[,] arr, int row, int col) {        
        if (arr[row + 1, col - 1] == "M") {
            if (arr[row + 2, col - 2] == ".") {
                return false;
            } else if (arr[row + 2, col - 2] == "A") {
                if (arr[row + 3, col - 3] == ".") {
                    return false;
                } else if (arr[row + 3, col - 3] == "S") {
                    return true;
                }
            }
        } else if (arr[row + 1, col - 1] == ".") {
            return false;
        }
        return false;
      }

      static int getX_MASCount(string[,] arr, int matrixLimit) {
        int x_masCount = 0;

        for (int row = 0; row < matrixLimit; row++) {
            for (int col = 0; col < matrixLimit; col++) {
                if (arr[row, col] != "A") {
                    continue;
                } else {
                    //check MAS left to right
                    if (foundMAS_ltr(arr, row, col)) {
                        //check MAS right to left
                        if (foundMAS_rtl(arr, row, col)) {
                            x_masCount++;
                        } else {
                            continue;
                        }
                    } else {
                        continue;
                    }
                }
            }
        }
        return x_masCount;
      }

      static bool foundMAS_ltr(string[,] arr, int row, int col) {
        if (arr[row - 1, col - 1] == "S") {
            if (arr[row + 1, col + 1] == "M") {
                return true;
            } else {
                return false;
            }
        } else if (arr[row - 1, col - 1] == "M") {
            if (arr[row + 1, col + 1] == "S") {
                return true;
            } else {
                return false;
            }
        }
        return false;
      }

      static bool foundMAS_rtl(string[,] arr, int row, int col) {
        if (arr[row - 1, col + 1] == "S") {
            if (arr[row + 1, col - 1] == "M") {
                return true;
            } else {
                return false;
            }
        } else if (arr[row - 1, col + 1] == "M") {
            if (arr[row + 1, col - 1] == "S") {
                return true;
            } else {
                return false;
            }
        }
        return false;
      }

   } //end class
} //end namespace