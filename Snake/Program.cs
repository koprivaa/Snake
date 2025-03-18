using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;
///█ ■
////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            GameWindow gameWindow = new GameWindow(Console.WindowWidth, Console.WindowHeight); //Přidaná třída GameWindow, jejichž objekt drží informace o výšce a šířce -Turecký
            Random randomNumber = new Random();
            int score = 5;
            int isGameover = 0;
            Pixel head = new Pixel();
            head.xpos = gameWindow.windowWidth / 2;
            head.ypos = gameWindow.windowHeight / 2;
            head.color = ConsoleColor.Red;
            Direction movement = Direction.RIGHT;
            List<int> xposBody = new List<int>();
            List<int> yposBody = new List<int>();
            Pixel berry = new Pixel(); //Berry předělané z jednotlivých int proměnných na instanci třídy Pixel, stejně jako head. -Turecký
            berry.xpos = randomNumber.Next(0, gameWindow.windowWidth);
            berry.ypos = randomNumber.Next(0, gameWindow.windowHeight);
            berry.color = ConsoleColor.Cyan;
            DateTime dateBeforePress = DateTime.Now;
            //DateTime dateDuringPress = DateTime.Now;
            string buttonWasPressed = "no";
            while (true)
            {
                Console.Clear();
                //Rozdělení na metody vykreslení hranic a kolizí -Kopřiva
                isGameover = CheckCollision(head, gameWindow); //Metoda přesunutá mimo Main a upravená
                DrawBorders(gameWindow); //Metoda přesunutá mimo Main a upravená

                Console.ForegroundColor = ConsoleColor.Green;
                //Přidání kolizí borůvkám a tělu hada -Kopřiva
                CheckBerryCollision();
                CheckBodyCollision();


                if (isGameover == 1)
                {
                    break;
                }
                //Rozdělení na update pozice hlavy a borůvky -Kopřiva
                UpdateHeadPosition();
                UpdateBerryPosition();
                ResetButtonState();

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

            

            

            

            void CheckBerryCollision()
            {
                if (berry.xpos == head.xpos && berry.ypos == head.ypos)
                {
                    IncreaseScore();
                    RespawnBerry();
                }
            }
            void IncreaseScore()
            {
                score++;
            }

            void RespawnBerry()
            {
                berry.xpos = randomNumber.Next(1, gameWindow.windowWidth - 2);
                berry.ypos = randomNumber.Next(1, gameWindow.windowHeight - 2);
            }
            void CheckBodyCollision()
            {
                for (int i = 0; i < xposBody.Count(); i++)
                {
                    DrawBodySegment(xposBody[i], yposBody[i]);

                    if (IsCollisionWithBody(xposBody[i], yposBody[i]))
                    {
                        isGameover = 1;
                    }
                }
            }

            void DrawBodySegment(int x, int y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("■");
            }

            bool IsCollisionWithBody(int x, int y)
            {
                return x == head.xpos && y == head.ypos;
            }

            void UpdateHeadPosition()
            {
                Console.SetCursorPosition(head.xpos, head.ypos);
                Console.ForegroundColor = head.color;
                Console.Write("■");
            }

            void UpdateBerryPosition()
            {
                Console.SetCursorPosition(berry.xpos, berry.ypos);
                Console.ForegroundColor = berry.color;
                Console.Write("■");
            }

            void ResetButtonState()
            {
                dateBeforePress = DateTime.Now;
                buttonWasPressed = "no";
            }



            Console.SetCursorPosition(gameWindow.windowWidth / 5, gameWindow.windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(gameWindow.windowWidth / 5, gameWindow.windowHeight / 2 + 1);
        }


        class Pixel
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor color { get; set; }
        }

        class GameWindow
        {
            public int windowWidth { get; }
            public int windowHeight { get; }

            public GameWindow(int windowWidth, int windowHeight)
            {
                this.windowWidth = windowWidth;
                this.windowHeight = windowHeight;
            }

        }

        static void DetermineMovementDirection(Direction movement, Pixel head)
        {
            switch (movement)
            {
                case Direction.UP:
                    head.ypos--;
                    break;
                case Direction.DOWN:
                    head.ypos++;
                    break;
                case Direction.LEFT:
                    head.xpos--;
                    break;
                case Direction.RIGHT:
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
        static Direction DeterminePressedButton(Direction currentDirection)
        {
            if (!Console.KeyAvailable)
                return currentDirection;

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    return Direction.UP;
                case ConsoleKey.S:
                    return Direction.DOWN;
                case ConsoleKey.A:
                    return Direction.LEFT;
                case ConsoleKey.D:
                    return Direction.RIGHT;
                default:
                    return currentDirection; // Pokud byla stisknuta neplatná klávesa, směr se nezmění
            }
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
        static void DrawBorders(GameWindow gameWindow)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int x = 0; x < gameWindow.windowWidth; x++)
            {
                DrawBorderTile(x, 0);
                DrawBorderTile(x, gameWindow.windowHeight - 1);
            }

            for (int y = 0; y < gameWindow.windowHeight; y++)
            {
                DrawBorderTile(0, y);
                DrawBorderTile(gameWindow.windowWidth - 1, y);
            }
        }
        static void DrawBorderTile(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("■");
        }

        static int CheckCollision(Pixel head, GameWindow gameWindow)
        {
            bool hitWall = head.xpos == gameWindow.windowWidth - 1 || head.xpos == 0 ||
                           head.ypos == gameWindow.windowHeight - 1 || head.ypos == 0;
            if (hitWall)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
//¦