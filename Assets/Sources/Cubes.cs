using System.Collections.Generic;

namespace Balthazatiy.XonixClone
{
    public class Cubes
    {
        private List<Cube> _cubes = new List<Cube>();

        public Cubes()
        {
            AddCube();
        }

        public void AddCube()
        {
            _cubes.Add(new Cube());
        }

        public void Move()
        {
            foreach (var cube in _cubes)
            {
                cube.Move();
            }
        }

        public List<Cube> GetCubes()
        {
            return _cubes;
        }

        public bool IsHitXonix()
        {
            foreach (var cube in _cubes)
                if (cube.IsHitXonix())
                    return true;
            return false;
        }

        public void Paint()
        {
            foreach (var cube in _cubes)
                cube.Paint();
        }
    }
}