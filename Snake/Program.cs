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
            int screenwidth = Console.WindowWidth;
            int screenheight = Console.WindowHeight;
            Random randomnummer = new Random();
            int score = 5;
            int gameover = 0;
            pixel head = new pixel();
            head.xpos = screenwidth / 2;
            head.ypos = screenheight / 2;
            head.schermkleur = ConsoleColor.Red;
            string movement = "RIGHT";
            List<int> xposBody = new List<int>();
            List<int> yposBody = new List<int>();
            int berryXPos = randomnummer.Next(0, screenwidth);
            int berryYPos = randomnummer.Next(0, screenheight);
            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            string buttonpressed = "no";
            while (true)
            {
                Console.Clear();
                if (head.xpos == screenwidth - 1 || head.xpos == 0 || head.ypos == screenheight - 1 || head.ypos == 0)
                {
                    gameover = 1;
                }
                for (int i = 0; i < screenwidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < screenwidth; i++)
                {
                    Console.SetCursorPosition(i, screenheight - 1);
                    Console.Write("■");
                }
                for (int i = 0; i < screenheight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < screenheight; i++)
                {
                    Console.SetCursorPosition(screenwidth - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryXPos == head.xpos && berryYPos == head.ypos)
                {
                    score++;
                    berryXPos = randomnummer.Next(1, screenwidth - 2);
                    berryYPos = randomnummer.Next(1, screenheight - 2);
                }
                for (int i = 0; i < xposBody.Count(); i++)
                {
                    Console.SetCursorPosition(xposBody[i], yposBody[i]);
                    Console.Write("■");
                    if (xposBody[i] == head.xpos && yposBody[i] == head.ypos)
                    {
                        gameover = 1;
                    }
                }
                if (gameover == 1)
                {
                    break;
                }
                Console.SetCursorPosition(head.xpos, head.ypos);
                Console.ForegroundColor = head.schermkleur;
                Console.Write("■");
                Console.SetCursorPosition(berryXPos, berryYPos);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
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
            Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
        }
        class pixel
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor schermkleur { get; set; }
        }
    }
}
//¦