using System;
using SplashKitSDK;

namespace RobotDodgeGame
{
    public class Program
    {
        public static void Main()
        {
            // Create a game window and initialize the RobotDodge game
            Window gameWindow = new Window("Robot Dodge", 800, 600);
            RobotDodge game = new RobotDodge(gameWindow);

            while (!game.Quit)
            {
                SplashKit.ProcessEvents();

                // Delegate input handling and logic updates to the RobotDodge game
                game.HandleInput();
                game.StayOnWindow();
                game.Update();

                // Clear screen, draw the game, and refresh
                SplashKit.ClearScreen();
                game.Draw();
                SplashKit.RefreshScreen();
                SplashKit.Delay(16);
            }

            SplashKit.CloseWindow(gameWindow);
        }
    }
}
