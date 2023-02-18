using System.Collections.Generic;

namespace Balthazatiy.XonixClone
{
    public class Balls
    {
        private List<Ball> _balls = new List<Ball>();

        public Balls()
        {
            AddBall();
        }

        public void AddBall()
        {
            _balls.Add(new Ball());
        }

        public void Move()
        {
            foreach (var ball in _balls)
            {
                ball.Move();
            }
        }

        public List<Ball> GetBalls()
        {
            return _balls;
        }

        public bool IsHitTrackOrXonix()
        {
            foreach (var ball in _balls)
                if (ball.IsHitTrackOrXonix())
                    return true;
            return false;
        }

        public void Paint()
        {
            foreach (var ball in _balls)
                ball.Paint();
        }
    }
}