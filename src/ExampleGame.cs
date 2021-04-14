using System;
using term2d;

namespace Term2DGame
{
    class ExampleGame : Game
    {
        int playerX = 1;
        int playerY = 1;

        public static void Main(string[] args)
        {
            Term2D.Start(new ExampleGame());
        }
        override public void Init(Canvas canvas)
        {
            Console.Title = "term2d Example Game";
            canvas.DefaultBackgroundColor = ConsoleColor.Blue;
            canvas.DefaultForegroundColor = ConsoleColor.White;
            canvas.Clear();
            playerX = canvas.Width / 2;
            playerY = canvas.Height / 2;
        }

        double timer = 0.0;
        bool running = true;
        override public bool Update(UpdateInfo updateInfo)
        {
            // Update Timer
            timer += updateInfo.DeltaTime;
            
            // Update View
            Canvas canvas = updateInfo.ActiveCanvas;
            
            // Alternate Between Blue & Green Every 2 Seconds
            if (timer > 5.0)
            {
                if (canvas.DefaultBackgroundColor == ConsoleColor.Blue)
                {
                    canvas.DefaultBackgroundColor = ConsoleColor.DarkBlue;
                }
                else
                {
                    canvas.DefaultBackgroundColor = ConsoleColor.Blue;
                }
                timer = 0.0;
            }

            canvas.Clear();
            int width = canvas.Width;
            int height = canvas.Height;
            
            // Draw Border Frame
            canvas.Draw(0, 0, '╔');
            canvas.Draw(0, width - 1, '╗');
            canvas.Draw(height - 1, 0, '╚');
            canvas.Draw(height - 1, width - 1, '╝');
            for (int col = 1; col < width - 1; col++)
            {
                canvas.Draw(0, col, '═');
                canvas.Draw(height - 1, col, '═');
            }
            for (int row = 1; row < height - 1; row++)
            {
                canvas.Draw(row, 0, '║');
                canvas.Draw(row, width - 1, '║');
            }

            // Draw Player
            if (playerX > 0 && playerX < canvas.Width - 1
                && playerY > 0 && playerY < canvas.Height - 1)
            {
                canvas.Draw(playerY, playerX, '█', ConsoleColor.Red, ConsoleColor.Blue);
            }

            // Draw Status
            double actualFPS = 1.0 / updateInfo.DeltaTime;
            canvas.DrawText(0, 1, $"══ term2d demo ══ FPS: {actualFPS:0.0} ══ Timer: {timer:0.00} ══");
            
            // True To Continue Game Loop, False To Stop
            return running;
        }

        override public void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    playerY--;
                    break;
                case ConsoleKey.DownArrow:
                    playerY++;
                    break;
                case ConsoleKey.LeftArrow:
                    playerX--;
                    break;
                case ConsoleKey.RightArrow:
                    playerX++;
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
                default:
                    break;
            }
        }
    }
}