using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Guess_Numbers
{
    class Program
    {
        #region Disable Resize Windows

        private const int MF_BYCOMMAND = 0x00000000;
        //public const int SC_CLOSE = 0xF060; // CLOSE Button
        //public const int SC_MINIMIZE = 0xF020; // MINIMIZE Button
        public const int SC_MAXIMIZE = 0xF030; // MAXIMIZE Button
        public const int SC_SIZE = 0xF000; // Resize Windows

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion

        #region Main Function

        static void Main(string[] args)
        {
            Console.Clear();
            ConsoleProperties(); // Properties of console window size, background and foreground color, title, Cursor Size, Cursor Visible, Disable Resize and MAXIMIZE Window
            ShowASCII();
            LoadingBar();
            do // Play again
            {
                byte[] randNum = new byte[4]; // Create a random 4-digit number
                randNum = GetRandom(randNum);

                byte[] input;
                do
                {
                    Border();
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(35, 15);
                    Console.Write(" Please enter a 4-digit number (split by space) : ");
                    try
                    {
                        input = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToByte);
                        if (input.Length == 4)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        //
                    }
                } while (true);

                mainBody(randNum, input); // Check user input data with random 4-digit number

                Console.ResetColor();
                Console.SetCursorPosition(48, 19);
                Console.Write(" Play new game ? (y or n) ");
            } while (Console.ReadKey(true).Key == ConsoleKey.Y); // Play again);
        }

        #endregion

        #region mainBody of Guess Numbers 

        /// <summary>
        /// Main Guess Numbers function: Check numbers is Green, Yellow or Red
        /// </summary>
        /// <param name="randNum">Number taken from the computer</param>
        /// <param name="input">Number taken from the user</param>
        private static void mainBody(byte[] randNum, byte[] input)
        {
            bool youWin = true; // user Win or not
            for (int i = 0; i < 10; i++)
            {
                for (;;) // If the input was not correct (line 176), repeat it again
                {
                    Border(); // Show main information
                    youWin = true;
                    Console.SetCursorPosition(50, 15);
                    Console.Write(" You entered :"); // Show user input whit color

                    // First user numer with color
                    if (input[0] == randNum[0])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (input[0] == randNum[1] | input[0] == randNum[2] | input[0] == randNum[3])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        youWin = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        youWin = false;
                    }
                    Console.Write(" " + input[0]);

                    // Second user numer with color
                    if (input[1] == randNum[1])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (input[1] == randNum[0] | input[1] == randNum[2] | input[1] == randNum[3])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        youWin = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        youWin = false;
                    }
                    Console.Write(" " + input[1]);

                    // Third user numer with color
                    if (input[2] == randNum[2])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (input[2] == randNum[0] | input[2] == randNum[1] | input[2] == randNum[3])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        youWin = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        youWin = false;
                    }
                    Console.Write(" " + input[2]);

                    // Fourth user numer with color
                    if (input[3] == randNum[3])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (input[3] == randNum[0] | input[3] == randNum[1] | input[3] == randNum[2])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        youWin = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        youWin = false;
                    }
                    Console.Write(" " + input[3]);

                    if (youWin) // user is win
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(57, 17);
                        Console.Write(" You win! ");
                        break;
                    }

                    if (i == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(43, 17);
                        Console.Write(" Game Over!");
                        Console.ResetColor();
                        Console.Write("  The number is : " + randNum[0] + " " + randNum[1] + " " + randNum[2] + " " + randNum[3]);
                        break;
                    }

                    Console.ResetColor();
                    Console.SetCursorPosition(35, 17);
                    Console.Write(" Please enter a new 4 digit number (split by space) : ");

                    byte[] temp = input; // If the input was not correct (line 173), repeat it again
                    try
                    {
                        input = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToByte);
                        if (input.Length == 4)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        //
                    }
                    input = temp; // the input was not correct (line 176), Previous input is stored
                }
            }
        }

        #endregion

        #region Get Random Number

        /// <summary>
        /// Create Random Number by Computer
        /// </summary>
        /// <param name="randNum">Byte array</param>
        /// <returns>4 digit random number</returns>
        private static byte[] GetRandom(byte[] randNum)
        {
            Random rand = new Random();
            randNum[0] = Convert.ToByte(rand.Next(1, 10)); // First random numer is 1 to 9
            while (true)
            {
                randNum[1] = Convert.ToByte(rand.Next(10)); // Second random numer is 0 to 9
                // Checking Duplicate Numbers
                if (randNum[1] == randNum[0])
                {
                    continue; // Create random number again
                }
                break;
            }
            while (true)
            {
                randNum[2] = Convert.ToByte(rand.Next(10)); // Third random numer is 0 to 9
                // Checking Duplicate Numbers
                if (randNum[2] == randNum[0] | randNum[2] == randNum[1])
                {
                    continue; // Create random number again
                }
                break;
            }
            while (true)
            {
                randNum[3] = Convert.ToByte(rand.Next(10)); // Fourth random numer is 0 to 9
                // Checking Duplicate Numbers
                if (randNum[3] == randNum[0] | randNum[3] == randNum[1] | randNum[3] == randNum[2])
                {
                    continue; // Create random number again
                }
                break;
            }
            return randNum;
        }

        #endregion

        #region Border

        /// <summary>
        /// Border
        /// </summary>
        static void Border()
        {
            Console.ResetColor();
            Console.Clear(); // clear screen of console
            Console.WriteLine();
            Console.WriteLine("  █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█");
            Console.WriteLine("  █                                              Welcome to Guess Numbers!                                            █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                           Programmering by Hamed Movaghari                                        █"); 
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █                                                                                                                   █");
            Console.WriteLine("  █▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█");
        }

        #endregion

        #region Loading Bar

        /// <summary>
        /// Show Loading Bar
        /// </summary>
        static void LoadingBar()
        {
            Console.SetCursorPosition(43, 26);
            Console.WriteLine(" Programmering by Hamed Movaghari");
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(11,17);
            for (int i = 0; i <= 100; i++)
            {
                Console.SetCursorPosition(11+i, 17);
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(12, 17);
                Console.Write($"{i}%");
                System.Threading.Thread.Sleep(100);
            }
            Console.SetCursorPosition(47, 20);
            Console.ResetColor();
            Console.WriteLine("Press any key to play game");
            Console.ReadKey(true);
        }

        #endregion

        #region ASCII: Guest Numbers

        /// <summary>
        /// Show Guest Numbers by ascii codes
        /// </summary>
        static void ShowASCII()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t" + @"  ________                               _______               ___.                        ");
            Console.WriteLine("\t\t" + @" /  _____/ __ __   ____   ______ ______  \      \  __ __  _____\_ |__   ___________  ______");
            Console.WriteLine("\t\t" + @"/   \  ___|  |  \_/ __ \ /  ___//  ___/  /   |   \|  |  \/     \| __ \_/ __ \_  __ \/  ___/");
            Console.WriteLine("\t\t" + @"\    \_\  \  |  /\  ___/ \___ \ \___ \  /    |    \  |  /  Y Y  \ \_\ \  ___/|  | \/\___ \ ");
            Console.WriteLine("\t\t" + @" \______  /____/  \___  >____  >____  > \____|__  /____/|__|_|  /___  /\___  >__|  /____  >");
            Console.WriteLine("\t\t" + @"        \/            \/     \/     \/          \/            \/    \/     \/           \/ ");
        }

        #endregion

        #region Console Properties

        /// <summary>
        /// Console Properties: Change window size, background and foreground color, title, Cursor Size, Cursor Visible, Disable Resize and MAXIMIZE Window
        /// </summary>
        static void ConsoleProperties()
        {
            Console.WindowHeight = 30; // Change Window Height
            Console.WindowWidth = 120; // Change Window Width
            Console.BufferHeight = 30; // Change Buffer Height
            Console.BufferWidth = 120; // Change Buffer Width
            Console.BackgroundColor = ConsoleColor.Black; // Change Background Color
            Console.ForegroundColor = ConsoleColor.Gray; // Change Foreground Color
            Console.Title = "Guest Numbers"; // Change Title
            Console.CursorSize = 25; // Change Cursor Size
            Console.CursorVisible = false; // Cursor Visible
            /* Disable Resize Windows */
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND); // Disable CLOSE Button
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND); // Disable MINIMIZE Button
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND); // Disable MAXIMIZE Button
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND); // Disable Resize Windows
            }
            /* Disable Resize Windows */
        }

        #endregion
    }
}