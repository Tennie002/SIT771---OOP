using SplashKitSDK;

namespace RobotDodgeGame
{
    public class Bullet
    {
        private float _x, _y;
        private float _dx, _dy;

        public Bullet(float startX, float startY, float targetX, float targetY)
        {
            _x = startX;
            _y = startY;

            Vector2D velocity = SplashKit.UnitVector(SplashKit.VectorPointToPoint(
                SplashKit.PointAt(startX, startY), SplashKit.PointAt(targetX, targetY))
            );
            _dx = (float)(velocity.X * 10); // Explicit cast to float
            _dy = (float)(velocity.Y * 10); // Explicit cast to float
        }

        public void Update()
        {
            _x += _dx;
            _y += _dy;
        }

        public void Draw()
        {
            SplashKit.FillCircle(Color.Red, _x, _y, 5); // Draw bullet
        }

        public Circle GetCircle()
        {
            return SplashKit.CircleAt(_x, _y, 5);
        }

        public bool IsOffScreen(Window gameWindow)
        {
            return _x < 0 || _x > gameWindow.Width || _y < 0 || _y > gameWindow.Height;
        }
    }
}
