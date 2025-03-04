using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            Random randomNumber = new Random();
            int score = 5;
            int isGameover = 0;
            Pixel head = new Pixel();
            head.xpos = screenWidth / 2;
            head.ypos = screenHeight / 2;
            head.color = ConsoleColor.Red;
            string movement = "RIGHT";
            List<int> xposBody = new List<int>();
            List<int> yposBody = new List<int>();
            int xposBerry = randomNumber.Next(0, screenWidth);
            int yposBerry = randomNumber.Next(0, screenHeight);
            DateTime dateBeforePress = DateTime.Now;
            DateTime dateDuringPress = DateTime.Now;
            string buttonWasPressed = "no";
            while (true)
            {
                Console.Clear();
                if (head.xpos == screenWidth - 1 || head.xpos == 0 || head.ypos == screenHeight - 1 || head.ypos == 0)
                {
                    isGameover = 1;
                }
                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, screenHeight - 1);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(screenWidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (xposBerry == head.xpos && yposBerry == head.ypos)
                {
                    score++;
                    xposBerry = randomNumber.Next(1, screenWidth - 2);
                    yposBerry = randomNumber.Next(1, screenHeight - 2);
                }
                for (int i = 0; i < xposBody.Count(); i++)
                {
                    Console.SetCursorPosition(xposBody[i], yposBody[i]);
                    Console.Write("■");
                    if (xposBody[i] == head.xpos && yposBody[i] == head.ypos)
                    {
                        isGameover = 1;
                    }
                }
                if (isGameover == 1)
                {
                    break;
                }
                Console.SetCursorPosition(head.xpos, head.ypos);
                Console.ForegroundColor = head.color;
                Console.Write("■");
                Console.SetCursorPosition(xposBerry, yposBerry);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                dateBeforePress = DateTime.Now;
                buttonWasPressed = "no";
                while (true)
                {
                    dateDuringPress = DateTime.Now;
                    if (dateDuringPress.Subtract(dateBeforePress).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pressedButton = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (pressedButton.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonWasPressed == "no")
                        {
                            movement = "UP";
                            buttonWasPressed = "yes";
                        }
                        if (pressedButton.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonWasPressed == "no")
                        {
                            movement = "DOWN";
                            buttonWasPressed = "yes";
                        }
                        if (pressedButton.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonWasPressed == "no")
                        {
                            movement = "LEFT";
                            buttonWasPressed = "yes";
                        }
                        if (pressedButton.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonWasPressed == "no")
                        {
                            movement = "RIGHT";
                            buttonWasPressed = "yes";
                        }
                    }
                }
                xposBody.Add(head.xpos);
                yposBody.Add(head.ypos);
                switch (movement)
                {
                    case "UP":
                        head.ypos--;
                        break;
                    case "DOWN":
                        head.ypos++;
                        break;
                    case "LEFT":
                        head.xpos--;
                        break;
                    case "RIGHT":
                        head.xpos++;
                        break;
                }
                if (xposBody.Count() > score)
                {
                    xposBody.RemoveAt(0);
                    yposBody.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
        class Pixel
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor color { get; set; }
        }
    }
}
//¦