using SplashKitSDK;
using RobotDodgeGame;

namespace RobotDodgeGame
{
    public class RobotDodge
    {
        private Player _player;
        private List<Robot> _robots;
        private List<Bullet> _bullets;
        private Window _gameWindow;

        public RobotDodge(Window gameWindow)
        {
            _gameWindow = gameWindow;
            _player = new Player(gameWindow);
            _robots = new List<Robot>();
            _bullets = new List<Bullet>();
        }

        public void HandleInput()
        {
            _player.HandleInput();
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _bullets.Add(new Bullet(_player.X + _player.Width / 2, _player.Y + _player.Height / 2, SplashKit.MouseX(), SplashKit.MouseY()));
            }
        }

        public void Update()
{
    _player.UpdateScore();

    // Update robots
    for (int i = 0; i < _robots.Count; i++)
    {
        _robots[i].Update();
        if (_robots[i].IsOffscreen(_gameWindow)) // Remove off-screen robots
        {
            _robots.RemoveAt(i--); // Safely adjust index
        }
    }

    // Update bullets
    for (int i = 0; i < _bullets.Count; i++)
    {
        _bullets[i].Update();
        if (_bullets[i].IsOffScreen(_gameWindow)) // Remove off-screen bullets
        {
            _bullets.RemoveAt(i--); // Safely adjust index
        }
    }

    // Handle collisions between player and robots
    for (int i = 0; i < _robots.Count; i++)
    {
        if (SplashKit.CirclesIntersect(_robots[i].CollisionCircle, _player.CollisionCircle))
        {
            _player.Lives--;
            _robots.RemoveAt(i--); // Safely remove robot
        }
    }

    // Handle collisions between bullets and robots
    for (int i = 0; i < _robots.Count; i++)
    {
        for (int j = 0; j < _bullets.Count; j++)
        {
            if (SplashKit.CirclesIntersect(_robots[i].CollisionCircle, _bullets[j].GetCircle()))
            {
                _robots.RemoveAt(i--); // Safely remove robot
                _bullets.RemoveAt(j--); // Safely remove bullet
                break; // Exit inner loop since the robot is removed
            }
        }
    }

    // Add new robots occasionally
    if (SplashKit.Rnd(100) < 5)
    {
        Robot newRobot = SplashKit.Rnd(2) < 1
            ? (Robot)new Boxy(_gameWindow, _player)
            : (Robot)new Roundy(_gameWindow, _player);

        _robots.Add(newRobot);
    }
}

        private Circle GetCollisionCircle()
        {
            return _player.CollisionCircle;
        }

        public void Draw()
        {
            _player.Draw();

            // Draw robots
            foreach (Robot robot in _robots)
            {
                robot.Draw();
            }

            // Draw bullets
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw();
            }

            DrawUI();
        }

        public void DrawUI()
        {
            for (int i = 0; i < _player.Lives; i++)
            {
                float x = 20 + i * 40; // Adjust position for each heart
                float y = 50; // Vertical position

                DrawHeart(x, y, 20, Color.Red);
            }

            SplashKit.DrawText($"Lives: {_player.Lives}", Color.Black, 10, 10);
            SplashKit.DrawText($"Score: {_player.Score}", Color.Black, 10, 30);
        }

        private void DrawHeart(float x, float y, float size, Color color)
        {
            float halfSize = size / 2;

            // Draw the two upper circles of the heart
            SplashKit.FillCircle(color, x - halfSize / 2, y, halfSize / 2); // Left circle
            SplashKit.FillCircle(color, x + halfSize / 2, y, halfSize / 2); // Right circle

            // Draw the bottom triangle of the heart
            SplashKit.FillTriangle(color, x - size, y, x + size, y, x, y + size); // Bottom triangle
        }

        public bool GetQuit()
        { return _player.Lives <= 0 || SplashKit.WindowCloseRequested(_gameWindow); }

        public void StayOnWindow()
        {
            _player.StayOnWindow(_gameWindow);
        }

        private Player player;

        public Player GetPlayer()
        { return _player; }



       

        // Remove this if already defined
        public bool Quit => _player.Lives <= 0 || SplashKit.WindowCloseRequested(_gameWindow);

        public Player Player { get; internal set; }

    }
}
    
    
    


