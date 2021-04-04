using System;
using term2d;

namespace example
{
    class ExampleGame : Game
    {
        int playerX = 20;
        int playerY = 20;

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
        }

        double timer = 0.0;
        override public bool Update(UpdateInfo updateInfo)
        {
            // Update Timer
            timer += updateInfo.DeltaTime;
            // Handle Logic
            if (updateInfo.HasUnreadInput)
            {
                switch (updateInfo.LastInput)
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
                        return false;
                    default:
                        break;
                }
            }
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
            int width = canvas.GetWidth();
            int height = canvas.GetHeight();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (col == 0 || row == 0 ||
                        col == width - 1 ||
                        row == height - 1)
                    {
                        canvas.Draw(row, col, '█');
                    }
                    else if (col == playerX && row == playerY)
                    {
                        canvas.Draw(row, col, '█', ConsoleColor.Red, ConsoleColor.Blue);
                    }
                }
            }
            double actualFPS = 1.0 / updateInfo.DeltaTime;
            canvas.DrawText(0, 0, $" == term2d demo == FPS: {actualFPS:0.0} == Timer: {timer:0.00} ==", ConsoleColor.Black, ConsoleColor.White);
            return true;
        }
    }
}