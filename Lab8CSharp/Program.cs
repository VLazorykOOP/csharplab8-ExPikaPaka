using System;
using System.Drawing;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Lab8CSharp {
    internal class Program {
        static void Main(string[] args) {
            int number = 1;

            while (number != 0) {
                Console.Write("Input task number [1-5], [0] to exit: ");

                try {
                    string? input = Console.ReadLine();

                    if (input != null) {
                        number = int.Parse(input);

                        switch (number) {
                            case 0:
                                return;

                            case 1:
                                task1(); // Testing task 1
                                break;

                            case 2:
                                task2(); // Testing task
                                break;

                            case 3:
                                task3(); // Testing task 3
                                break;

                            case 4:
                                task4(); // Testing task 4
                                break;

                            case 5:
                                task5(); // Testing task 5
                                break;

                            default:
                                break;
                        }
                    } else {
                        Console.WriteLine("Nothing provided. Exiting...");
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }

                Console.WriteLine();
            }
        }



        static void task1() {
            Console.WriteLine("|===~        Testing task 1.1        ~===|");

            try {
                string? input = "input.txt";
                string? output = "output.txt";

                if (input != null && output != null) {
                    // Using stream for big files
                    StreamReader reader = new StreamReader(input);
                    StreamWriter writer = new StreamWriter(output);

                    while (!reader.EndOfStream) {
                        string? text = reader.ReadLine();

                        if (text != null) {
                            System.Console.Write(text);

                            // Min - 01.01.1900
                            // Max - 31.12.2099
                            string pattern = @"((0[1-9])|([1-2][0-9])|(3(0|1)))\.((0[1-9])|([1][0-2]))\.((19|20)[0-9]{2})";

                            if (Regex.IsMatch(text, pattern)) {
                                writer.WriteLine(text);

                                Console.WriteLine(" - [J] Pass");
                            } else {
                                Console.WriteLine(" - [X] X Fail X");
                            }
                        }
                    }

                    reader.Close();
                    writer.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            Console.WriteLine();
        }

        static void task2() {
            Console.WriteLine("|===~        Testing task 2.1        ~===|");

            try {
                Console.Write("Input text filename: ");
                string? input = Console.ReadLine();

                Console.Write("Input a 'word' to find it's location in the text: ");
                string? checkWord = Console.ReadLine();

                if (input != null && checkWord != null) {
                    // Using stream for big files
                    StreamReader reader = new StreamReader(input);

                    bool wordMatch = false;
                    int charPosition = 1;
                    int linePosition = 1;

                    while (!reader.EndOfStream) {
                        string? text = reader.ReadLine();

                        if(text != null) {
                            string pattern = checkWord;
                            Match match = Regex.Match(text, pattern);

                            if (match.Success) {
                                wordMatch = true;
                                charPosition = match.Index + 1;
                                break;
                            } 
                            linePosition++;
                        }
                    }
                    reader.Close();
                   
                    if(wordMatch) {
                        Console.WriteLine($"Found word '{checkWord}' at ({linePosition},{charPosition})");
                    } else { 
                        Console.WriteLine("No match found."); 
                    }
                }
            } catch (FileNotFoundException ex) {
                Console.WriteLine("File not found!");
            } catch (Exception ex) { 
                Console.WriteLine(ex);
            }

            Console.WriteLine();
        }

        static void task3() {
            Console.WriteLine("|===~        Testing task 3.1        ~===|");

            try {
                Console.Write("Input text filename: ");
                string? input = Console.ReadLine();

                if (File.Exists(input)) {
                    StreamReader reader = new StreamReader(input);

                    // Output words with duplicated letters
                    Console.WriteLine("\n  Words with duplicated letters:");
                    while (!reader.EndOfStream) {
                        string text = reader.ReadLine();
                        string[] words = text.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' });

                        foreach (string word in words) {
                            if (HasDuplicatedLetters(word)) {
                                Console.Write(word + " ");
                            }
                        }
                    }
                    reader.Close();

                    Console.WriteLine("\n"); 

                    // Reopen the reader tor print text without duplicated letters
                    reader = new StreamReader(input);
                    Console.WriteLine("\n  Text without duplicated letters:");

                    while (!reader.EndOfStream) {
                        string text = reader.ReadLine();
                        string[] words = text.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?' });

                        foreach (string word in words) {
                            if (!HasDuplicatedLetters(word)) {
                                Console.Write(word + " ");
                            }
                        }
                    }
                    reader.Close();
                } else {
                    Console.WriteLine("File not found!");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            Console.WriteLine();
        }

        static bool HasDuplicatedLetters(string word) {
            foreach (char c in word) {
                if (word.IndexOf(c) != word.LastIndexOf(c)) {
                    return true;
                }
            }
            return false;
        }

        static void task4() {
            Console.WriteLine("|===~        Testing task 4.1        ~===|");

            FileStream fileStream = null;
            BinaryWriter writer = null;
            BinaryReader reader = null;

            try {
                string fileName = "powers_of_3.txt";

                // Writing to file in binary mode
                fileStream = new FileStream(fileName, FileMode.Create);
                writer = new BinaryWriter(fileStream);

                for (int i = 0; i < 20; i++) {
                    int value = (int)Math.Pow(3, i);
                    writer.Write(value);
                }

                writer.Close();
                fileStream.Close();

                // Reading from file in binary mode
                fileStream = new FileStream(fileName, FileMode.Open);
                reader = new BinaryReader(fileStream);

                int lineNumber = 1;

                Console.WriteLine("Line number   |   Value   |   Info");
                Console.WriteLine("-----------------------------------");

                while (reader.BaseStream.Position < reader.BaseStream.Length) {
                    int value = reader.ReadInt32();
                    string line = value.ToString();

                    if (lineNumber % 2 == 0) {
                        string lineNumberStr = lineNumber.ToString().PadLeft(14);
                        string lineValueStr = line.PadLeft(11);
                        string infoStr = $"(3^{lineNumber - 1})".PadLeft(8);
                        Console.WriteLine($"{lineNumberStr}|{lineValueStr}|{infoStr}");
                    }

                    lineNumber++;
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            } 

            // Closing resources
            if (writer != null)
                writer.Close();

            if (reader != null)
                reader.Close();

            if (fileStream != null)
                fileStream.Close();
 
        }

        static void task5() {
            Console.WriteLine("|===~        Testing task 5.1        ~===|");

            try {

                string studentSurname = "Shevchenko";

                // Step 1: Create directories
                string tempPath = @".\temp";
                string studentDir1 = $"{tempPath}\\{studentSurname}1";
                string studentDir2 = $"{tempPath}\\{studentSurname}2";
                Directory.Delete(tempPath, true);
                Directory.CreateDirectory(tempPath);
                Directory.CreateDirectory(studentDir1);
                Directory.CreateDirectory(studentDir2);

                // Step 2: Create files and write text
                string t1Text = "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми";
                string t2Text = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ";
                File.WriteAllText($"{studentDir1}\\t1.txt", t1Text);
                File.WriteAllText($"{studentDir1}\\t2.txt", t2Text);

                // Step 3: Read and write files in studentDir2
                string t1Content = File.ReadAllText($"{studentDir1}\\t1.txt");
                string t2Content = File.ReadAllText($"{studentDir1}\\t2.txt");
                File.WriteAllText($"{studentDir2}\\t3.txt", t1Content + "\n" + t2Content);

                // Step 4: Display information about created files
                Console.WriteLine("Files created:");
                Console.WriteLine($"t1.txt: {t1Text}");
                Console.WriteLine($"t2.txt: {t2Text}");
                Console.WriteLine($"t3.txt: {t1Content}\n{t2Content}");

                // Step 5: Move t2.txt to studentDir2
                File.Move($"{studentDir1}\\t2.txt", $"{studentDir2}\\t2.txt");

                // Step 6: Copy t1.txt to studentDir2
                File.Copy($"{studentDir1}\\t1.txt", $"{studentDir2}\\t1.txt");

                // Step 7: Rename directories
                Directory.Move(studentDir2, $"{tempPath}\\ALL");
                Directory.Delete(studentDir1, true);

                // Step 8: Display information about files in ALL directory
                Console.WriteLine("\nFiles in ALL directory:");
                string[] filesInAll = Directory.GetFiles($"{tempPath}\\ALL");
                foreach (string file in filesInAll) {
                    string fileName = Path.GetFileName(file);
                    string fileContent = File.ReadAllText(file);
                    Console.WriteLine($"{fileName}: {fileContent}");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}