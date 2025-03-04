﻿using System;
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
            //DateTime dateDuringPress = DateTime.Now;
            string buttonWasPressed = "no";
            while (true)
            {
                Console.Clear();
                //Rozdělení na metody vykreslení hranic a kolizí -Kopřiva
                CheckCollision();
                DrawBorders();


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
                    if (!has500msPassed(dateBeforePress)) { break; } //Přesunut výpočet, že 500 milisekund uběhlo do vlastní metody. -Turecký
                    if (Console.KeyAvailable && buttonWasPressed == "no")
                    {
                        movement = DeterminePressedButton(movement); //Přesunuté určení směru podle zmáčknutého tlačítka do vlastní metody. -Turecký
                        buttonWasPressed = "yes";
                    }
                }
                AddNewBodyParts(head, [xposBody, yposBody]); //Přesunuté do vlastní metody -Turecký
                DetermineMovementDirection(movement, head); //Switch statement přesunutý do vlastní metody -Turecký
                if (xposBody.Count() > score)
                {
                    RemoveOldBodyParts([xposBody, yposBody]); //Přesunuté do vlastní metody -Turecký
                }
            }

            void CheckCollision()
            {
                bool hitWall = head.xpos == screenWidth - 1 || head.xpos == 0 ||
                               head.ypos == screenHeight - 1 || head.ypos == 0;
                if (hitWall)
                {
                    isGameover = 1;
                }
            }

            void DrawBorders()
            {
                Console.ForegroundColor = ConsoleColor.Cyan ;

                for (int x = 0; x < screenWidth; x++)
                {
                    DrawBorderTile(x, 0);
                    DrawBorderTile(x, screenHeight - 1);
                }

                for (int y = 0; y < screenHeight; y++)
                {
                    DrawBorderTile(0, y);
                    DrawBorderTile(screenWidth - 1, y);
                }
            }

            void DrawBorderTile(int x, int y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("■");
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

        static void DetermineMovementDirection(string movement, Pixel head)
        {
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
        }


        static bool has500msPassed(DateTime dateBeforePress)
        {
            DateTime dateDuringPress = DateTime.Now;
            if (dateDuringPress.Subtract(dateBeforePress).TotalMilliseconds > 500) { return false; }
            return true;
        }
        static string DeterminePressedButton(string movement)
        {
            ConsoleKeyInfo pressedButton = Console.ReadKey(true);
            string new_movement = movement;
            if (pressedButton.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN")
            {
                new_movement = "UP";
            }
            if (pressedButton.Key.Equals(ConsoleKey.DownArrow) && movement != "UP")
            {
                new_movement = "DOWN";
            }
            if (pressedButton.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT")
            {
                new_movement = "LEFT";
            }
            if (pressedButton.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT")
            {
                new_movement = "RIGHT";
            }
            return new_movement;
        }

        static void AddNewBodyParts(Pixel head, List<List<int>> body)
        {
            body[0].Add(head.xpos);
            body[1].Add(head.ypos);
        }
        static void RemoveOldBodyParts(List<List<int>> body)
        {
            body[0].RemoveAt(0);
            body[1].RemoveAt(0);
        }
    }
}
//¦