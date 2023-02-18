using UnityEngine;

namespace Balthazatiy.XonixClone
{
    public class Xonix
    {
        private int _x, _y, _direction, _livesCount = 3, _level = 1;
        private bool _isWater, _isSelfCross;

        public Xonix()
        {
            InitXonix();
        }

        public void InitXonix()
        {
            _y = 0;
            _x = GameXonix.FIELD_WIDTH / 2;
            _direction = 0;
            _isWater = false;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int GetLivesCount()
        {
            return _livesCount;
        }

        public void DecreaseLivesCount()
        {
            _livesCount--;
        }

        public int GetCurrentLevel()
        {
            return _level;
        }

        public void LevelUp()
        {
            _level++;
        }

        public void SetDirection(int direction)
        {
            this._direction = direction;
        }

        public void Move()
        {
            if (_direction == GameXonix.LEFT) _x--;
            if (_direction == GameXonix.RIGHT) _x++;
            if (_direction == GameXonix.UP) _y--;
            if (_direction == GameXonix.DOWN) _y++;
            if (_x < 0) _x = 0;
            if (_y < 0) _y = 0;
            if (_y > GameXonix.FIELD_HEIGHT - 1) _y = GameXonix.FIELD_HEIGHT - 1;
            if (_x > GameXonix.FIELD_WIDTH - 1) _x = GameXonix.FIELD_WIDTH - 1;
            _isSelfCross = GameXonix.field.GetCellColor(_x, _y) == GameXonix.COLOR_TRACK;
            if (GameXonix.field.GetCellColor(_x, _y) == GameXonix.COLOR_LAND && _isWater)
            {
                _direction = 0;
                _isWater = false;
                GameXonix.field.TryToFill();
            }
            if (GameXonix.field.GetCellColor(_x, _y) == GameXonix.COLOR_WATER)
            {
                _isWater = true;
                GameXonix.field.SetCellColor(_x, _y, GameXonix.COLOR_TRACK);
            }
        }

        public bool IsSelfCrosed()
        {
            return _isSelfCross;
        }

        public void Paint()
        {
            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE, _y * GameXonix.POINT_SIZE, GameXonix.POINT_SIZE,
                GameXonix.POINT_SIZE, GameXonix.GetColors(GameXonix.POINT_SIZE * GameXonix.POINT_SIZE,
                (GameXonix.field.GetCellColor(_x, _y) == GameXonix.COLOR_LAND) ? GameXonix.GetColor(GameXonix.COLOR_TRACK) : Color.white));

            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE + 3, _y * GameXonix.POINT_SIZE + 3, GameXonix.POINT_SIZE - 6,
                GameXonix.POINT_SIZE - 6, GameXonix.GetColors((GameXonix.POINT_SIZE - 6) * (GameXonix.POINT_SIZE - 6),
                (GameXonix.field.GetCellColor(_x, _y) == GameXonix.COLOR_LAND) ? Color.white : GameXonix.GetColor(GameXonix.COLOR_TRACK)));
        }
    }
}