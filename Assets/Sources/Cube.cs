namespace Balthazatiy.XonixClone
{
    public class Cube
    {
        private int _x, _y, _dx, _dy;

        public Cube()
        {
            InitCube();
        }

        public void InitCube()
        {
            _x = _y = _dx = 1;
            _dy = -1;
        }

        void UpdateDxAndDy()
        {
            if (GameXonix.field.GetCellColor(_x + _dx, _y) == GameXonix.COLOR_WATER)
                _dx = -_dx;
            if (GameXonix.field.GetCellColor(_x, _y + _dy) == GameXonix.COLOR_WATER)
                _dy = -_dy;
        }

        public void Move()
        {
            UpdateDxAndDy();
            _x += _dx;
            _y += _dy;
        }

        public bool IsHitXonix()
        {
            UpdateDxAndDy();
            if (_x + _dx == GameXonix.xonix.GetX() && _y + _dy == GameXonix.xonix.GetY())
                return true;
            return false;
        }

        public void Paint()
        {
            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE, _y * GameXonix.POINT_SIZE,
                GameXonix.POINT_SIZE, GameXonix.POINT_SIZE, GameXonix.GetColors(GameXonix.POINT_SIZE * GameXonix.POINT_SIZE,
                GameXonix.GetColor(GameXonix.COLOR_WATER)));

            GameController.Instance.Texture.SetPixels(_x * GameXonix.POINT_SIZE + 2, _y * GameXonix.POINT_SIZE + 2,
                GameXonix.POINT_SIZE - 4, GameXonix.POINT_SIZE - 4, GameXonix.GetColors((GameXonix.POINT_SIZE - 4) * (GameXonix.POINT_SIZE - 4),
                GameXonix.GetColor(GameXonix.COLOR_LAND)));
        }
    }
}