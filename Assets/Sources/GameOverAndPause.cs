namespace Balthazatiy.XonixClone
{
    public class GameOverAndPause
    {
        private bool _gameOver;
        private bool _isPaused;

        public bool IsGameOver()
        {
            return _gameOver;
        }

        public bool IsPaused()
        {
            return _isPaused;
        }

        public void OnGameOver()
        {
            if (GameXonix.xonix.GetLivesCount() == 0)
            {
                GameController.Instance.SetUI(false);
                _gameOver = true;
            }
        }

        public void OnPause()
        {
            _isPaused = !_isPaused;
        }
    }
}