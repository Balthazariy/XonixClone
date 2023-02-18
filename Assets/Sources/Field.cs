using UnityEngine;

namespace Balthazatiy.XonixClone
{
    public class Field
    {
        private const int WATER_AREA = (GameXonix.FIELD_WIDTH - 4) * (GameXonix.FIELD_HEIGHT - 4);
        private int[,] _field = new int[GameXonix.FIELD_WIDTH, GameXonix.FIELD_HEIGHT];
        private float _currentWaterArea;
        //private int _scoreCount = 0;

        public Field()
        {
            InitField();
        }

        public void InitField()
        {
            for (int y = 0; y < GameXonix.FIELD_HEIGHT; y++)
                for (int x = 0; x < GameXonix.FIELD_WIDTH; x++)
                    _field[x, y] = (x < 2 || x > GameXonix.FIELD_WIDTH - 3 || y < 2 || y > GameXonix.FIELD_HEIGHT - 3)
                        ? GameXonix.COLOR_LAND
                        : GameXonix.COLOR_WATER;
            _currentWaterArea = WATER_AREA;
        }

        public int GetCellColor(int x, int y)
        {
            if (x < 0 || y < 0 || x > GameXonix.FIELD_WIDTH - 1 || y > GameXonix.FIELD_HEIGHT - 1) return GameXonix.COLOR_WATER;
            return _field[x, y];
        }

        public void SetCellColor(int x, int y, int color)
        {
            _field[x, y] = color;
        }

        public int GetCurrentPercent()
        {
            return (int)Mathf.Round(100f - _currentWaterArea / WATER_AREA * 100);
        }

        public void ClearTrack()
        {
            for (int y = 0; y < GameXonix.FIELD_HEIGHT; y++)
                for (int x = 0; x < GameXonix.FIELD_WIDTH; x++)
                    if (_field[x, y] == GameXonix.COLOR_TRACK) _field[x, y] = GameXonix.COLOR_WATER;
        }

        void FillTemporary(int x, int y)
        {
            var ballSqгare = 0;
            if (_field[x, y] > GameXonix.COLOR_WATER) return;
            _field[x, y] = GameXonix.COLOR_TEMP; // filling temporary color
            for (int dx = -1; dx < 2; dx++)
                for (int dy = -1; dy < 2; dy++)
                {
                    FillTemporary(x + dx, y + dy);
                }
        }

        public void TryToFill()
        {
            _currentWaterArea = 0;

            foreach (var ball in GameXonix.balls.GetBalls())
            {
                FillTemporary(ball.GetX(), ball.GetY());
            }
            for (int y = 0; y < GameXonix.FIELD_HEIGHT; y++)
                for (int x = 0; x < GameXonix.FIELD_WIDTH; x++)
                {
                    if (_field[x, y] == GameXonix.COLOR_TRACK || _field[x, y] == GameXonix.COLOR_WATER)
                    {
                        _field[x, y] = GameXonix.COLOR_LAND;
                        //_scoreCount += 10;
                    }
                    if (_field[x, y] == GameXonix.COLOR_TEMP)
                    {
                        _field[x, y] = GameXonix.COLOR_WATER;
                        _currentWaterArea++;
                    }
                }
        }

        public void Paint()
        {
            for (int y = 0; y < GameXonix.FIELD_HEIGHT; y++)
                for (int x = 0; x < GameXonix.FIELD_WIDTH; x++)
                {
                    GameController.Instance.Texture.SetPixels(x * GameXonix.POINT_SIZE, y * GameXonix.POINT_SIZE, GameXonix.POINT_SIZE, GameXonix.POINT_SIZE,
                        GameXonix.GetColors(GameXonix.POINT_SIZE * GameXonix.POINT_SIZE, GameXonix.GetColor(_field[x, y])));
                }
        }
    }
}