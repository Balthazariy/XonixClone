using UnityEngine;

namespace Balthazatiy.XonixClone
{
    public class Ball
    {
        private int _x, _y, _dx, _dy;

        public Ball()
        {
            do
            {
                _x = UnityEngine.Random.Range(0, GameXonix.FIELD_WIDTH);
                _y = UnityEngine.Random.Range(0, GameXonix.FIELD_HEIGHT);
            }
            while (GameXonix.field.GetCellColor(_x, _y) > GameXonix.COLOR_WATER);

            _dx = UnityEngine.Random.Range(0, 1) == 0 ? 1 : -1;
            _dy = UnityEngine.Random.Range(0, 1) == 0 ? 1 : -1;
        }

        void UpdateDxAndDy()
        {
            if (GameXonix.field.GetCellColor(_x + _dx, _y) == GameXonix.COLOR_LAND)
                _dx = -_dx;
            if (GameXonix.field.GetCellColor(_x, _y + _dy) == GameXonix.COLOR_LAND)
                _dy = -_dy;
        }

        public void Move()
        {
            UpdateDxAndDy();
            _x += _dx;
            _y += _dy;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int GetSquare()
        {
            return _x * _y;
        }

        public bool IsHitTrackOrXonix()
        {
            UpdateDxAndDy();
            if (GameXonix.field.GetCellColor(_x + _dx, _y + _dy) == GameXonix.COLOR_TRACK)
                return true;
            if (_x + _dx == GameXonix.xonix.GetX() && _y + _dy == GameXonix.xonix.GetY())
                return true;
            return false;
        }

        public void Paint()
        {
            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE, _y * GameXonix.POINT_SIZE,
                GameXonix.POINT_SIZE, GameXonix.POINT_SIZE, GameXonix.GetColors(GameXonix.POINT_SIZE * GameXonix.POINT_SIZE, Color.white));

            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE + 2, _y * GameXonix.POINT_SIZE + 2,
                GameXonix.POINT_SIZE - 4, GameXonix.POINT_SIZE - 4, GameXonix.GetColors((GameXonix.POINT_SIZE - 4) * (GameXonix.POINT_SIZE - 4),
                GameXonix.GetColor(GameXonix.COLOR_LAND)));
        }
    }
}