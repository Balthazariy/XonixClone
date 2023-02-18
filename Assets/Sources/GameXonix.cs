using System.Collections;
using UnityEngine;

namespace Balthazatiy.XonixClone
{
    public class GameXonix
    {
        #region CONSTANTS
        public const int POINT_SIZE = 10;
        public const int FIELD_WIDTH = 640 / POINT_SIZE;
        public const int FIELD_HEIGHT = 430 / POINT_SIZE;
        public const int LEFT = 37;
        public const int UP = 38;
        public const int RIGHT = 39;
        public const int DOWN = 40;
        public const int COLOR_TEMP = 1;
        public const int COLOR_WATER = 0;
        public const int COLOR_LAND = 0x00a8a8;
        public const int COLOR_TRACK = 0x901290;
        public const int PERCENT_OF_WATER_CAPTURE = 75;

        #endregion

        public static Xonix xonix;

        public static Field field;
        public static Balls balls;
        public static Cubes cubes;
        public static GameOverAndPause gameOverOrPause;

        private Canvas canvas;

        public static Color GetColor(int color)
        {
            switch (color)
            {
                case COLOR_TEMP: return Color.white;
                case COLOR_WATER: return Color.black;
                case COLOR_LAND: return Color.cyan;
                case COLOR_TRACK: return Color.magenta;
            }
            return Color.red;
        }

        public static Color[] GetColors(int size, Color color)
        {
            var result = new Color[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = color;
            }
            return result;
        }

        public GameXonix()
        {
            xonix = new Xonix();
            field = new Field();
            balls = new Balls();
            cubes = new Cubes();
            gameOverOrPause = new GameOverAndPause();
            canvas = new Canvas();
        }

        public IEnumerator Go()
        {
            while (!gameOverOrPause.IsGameOver())
            {
                if (gameOverOrPause.IsPaused())
                    yield return new WaitForSeconds(0.5f);
                else
                {
                    xonix.Move();
                    balls.Move();
                    cubes.Move();
                    canvas.Paint();
                    yield return new WaitForSeconds(0.015f);
                    if (GameController.Instance.TimeIsUp)
                    {
                        GameController.Instance.SetTimer();
                        cubes.AddCube();
                    }
                    if (xonix.IsSelfCrosed() || balls.IsHitTrackOrXonix() || cubes.IsHitXonix())
                    {
                        GameController.Instance.SetTimer();
                        xonix.DecreaseLivesCount();
                        GameController.Instance.SetLivesCount(xonix.GetLivesCount());
                        if (xonix.GetLivesCount() > 0)
                        {
                            xonix.InitXonix();
                            field.ClearTrack();
                            canvas.Paint();
                            yield return new WaitForSeconds(2f);
                        }
                    }
                    if (field.GetCurrentPercent() >= PERCENT_OF_WATER_CAPTURE)
                    {
                        GameController.Instance.SetTimer();
                        xonix.LevelUp();
                        field.InitField();
                        xonix.InitXonix();
                        balls.AddBall();
                        canvas.Paint();
                        yield return new WaitForSeconds(1f);
                    }
                }

            }
        }
    }
}