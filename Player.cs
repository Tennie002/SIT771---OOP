using SplashKitSDK;

namespace RobotDodgeGame
{
    public class Player
    {
        private Bitmap _PlayerBitmap;
        private SplashKitSDK.Timer _gameTimer; // Explicit namespace to resolve ambiguity
        public float X { get; private set; }
        public float Y { get; private set; }
        private const int SPEED = 5;

        public bool Quit { get; private set; }
        public int Lives { get; set; } = 5; // Added Lives property to track player lives.
        public int Score { get; private set; } = 0; // Added Score property to track the player's score.

        public int Width => _PlayerBitmap.Width;
        public int Height => _PlayerBitmap.Height;

        public Circle CollisionCircle { get; internal set; }

        public Player(Window gameWindow)
        {
            _PlayerBitmap = new Bitmap("Player", "Resources 2/images/Player.png");
            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;
             // Initialize and start the timer
            _gameTimer = new SplashKitSDK.Timer("Game Timer");
            _gameTimer.Start();
        }

        public void HandleInput()
        {
            if (SplashKit.KeyDown(KeyCode.UpKey)) Y -= SPEED;
            if (SplashKit.KeyDown(KeyCode.DownKey)) Y += SPEED;
            if (SplashKit.KeyDown(KeyCode.LeftKey)) X -= SPEED;
            if (SplashKit.KeyDown(KeyCode.RightKey)) X += SPEED;

            if (SplashKit.KeyDown(KeyCode.EscapeKey)) Quit = true;
        }

        public void StayOnWindow(Window window)
        {
            const int GAP = 10;
            if (X < GAP) X = GAP;
            if (X > window.Width - GAP - Width) X = window.Width - GAP - Width;
            if (Y < GAP) Y = GAP;
            if (Y > window.Height - GAP - Height) Y = window.Height - GAP - Height;
        }

        public void UpdateScore()
        {
            Score = (int)(_gameTimer.Ticks / 1000);
        }

        public void Draw()
        {
            _PlayerBitmap.Draw(X, Y);
        }

        public bool CollidedWith(Robot robot)
        {
            Circle robotCircle = SplashKit.CircleAt(robot.X + GetSize() / 2, robot.Y + Robot.Size / 2, Robot.Size / 2);
            return _PlayerBitmap.CircleCollision(X, Y, robotCircle);
        }

        private static int GetSize()
        {
            return Robot.Size;
        }
    }
}