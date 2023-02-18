namespace Balthazatiy.XonixClone
{
    public class Canvas
    {
        public void Paint()
        {
            GameXonix.field.Paint();
            GameController.Instance.SetFillAmount(GameXonix.field.GetCurrentPercent());
            GameController.Instance.SetLevelNum(GameXonix.xonix.GetCurrentLevel());
            GameXonix.xonix.Paint();
            GameXonix.balls.Paint();
            GameXonix.cubes.Paint();
            GameXonix.gameOverOrPause.OnGameOver();
            GameController.Instance.Texture.Apply();
        }
    }
}