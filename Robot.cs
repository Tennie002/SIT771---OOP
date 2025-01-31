using System;
using SplashKitSDK;
using RobotDodgeGame;
namespace RobotDodgeGame
{
    public abstract class Robot
    {
        internal static int Size;

        public double X { get; protected set; }
        public double Y { get; protected set; }
        public Vector2D Velocity { get; protected set; }
        public Color MainColor { get; set; }

        public virtual int Width => 50; // Default width
        public virtual int Height => 50; // Default height
        public Circle CollisionCircle => SplashKit.CircleAt(X + Width / 2, Y + Height / 2, Width / 2);

       protected Robot(Window gameWindow, Player player)
{
    MainColor = SplashKit.RandomRGBColor(255);
    X = SplashKit.Rnd(gameWindow.Width - Width);
    Y = SplashKit.Rnd(gameWindow.Height - Height);

    // Calculate the vector manually
    Point2D startPoint = SplashKit.PointAt(X, Y);
    Point2D endPoint = SplashKit.PointAt(player.X, player.Y);
    double dx = endPoint.X - startPoint.X;
    double dy = endPoint.Y - startPoint.Y;

    // Normalize the vector (convert to unit vector)
    double magnitude = Math.Sqrt(dx * dx + dy * dy);
    if (magnitude == 0) magnitude = 1; // Prevent division by zero
    Vector2D velocity = new Vector2D()
    {
        X = dx / magnitude,
        Y = dy / magnitude
    };

    // Set velocity and scale it by a random speed
    Velocity = SplashKit.VectorMultiply(velocity, SplashKit.Rnd(1, 5));
}


        public abstract void Draw();

        public void Update()
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        public bool IsOffscreen(Window screen)
        {
            return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
        }
    }

    public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {
    }

    public override void Draw()
    {
        // Draw the square robot body
        SplashKit.FillRectangle(MainColor, X, Y, Width, Height);
        SplashKit.DrawRectangle(Color.Black, X, Y, Width, Height);

        // Draw eyes
        double eyeSize = 5;
        double eyeOffsetX = Width / 4;
        double eyeOffsetY = Height / 4;

        SplashKit.FillCircle(Color.White, X + eyeOffsetX, Y + eyeOffsetY, eyeSize);
        SplashKit.FillCircle(Color.White, X + Width - eyeOffsetX, Y + eyeOffsetY, eyeSize);

        // Draw mouth
        double mouthStartX = X + Width / 4;
        double mouthEndX = X + 3 * Width / 4;
        double mouthY = Y + 3 * Height / 4;

        SplashKit.DrawLine(Color.Black, mouthStartX, mouthY, mouthEndX, mouthY);
    }
}

    public class Roundy : Robot  // add Roundy 
    {
        public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
        {
        }

        public override void Draw()
        {
            double leftX, midX, rightX;
            double midY, eyeY, mouthY;

            midX = X + 25;
            rightX = X + 33;
            midY = Y + 25;
            eyeY = Y + 20;
            mouthY = Y + 35;
            leftX = X + 17;

            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
            SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
        }
    }
}