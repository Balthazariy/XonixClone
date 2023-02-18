using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Balthazatiy.XonixClone
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public Texture2D Texture;
        public SpriteRenderer FildRenderer;

        private GameXonix _gameXonix;

        private float _levelTime;
        public bool TimeIsUp;

        #region UI_ELEMENTS
        public TextMeshProUGUI Level;
        public TextMeshProUGUI Lives;
        public TextMeshProUGUI Fill;
        public TextMeshProUGUI Timer;

        public GameObject GameOver;
        public Button PauseButton;
        public Button PlayButton;
        #endregion

        #region SWIPE_DETECTION
        private Vector3 _touchPosition;
        private float _swipeResistanceX = 50.0f;
        private float _swipeResistanceY = 100.0f;
        #endregion

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            GameStart();
        }

        void Update()
        {
            CountDownLevelTime();
            Move();
        }

        private void GameStart()
        {
            SetUI(true);
            SetTimer();
            _gameXonix = new GameXonix();
            StartCoroutine(_gameXonix.Go());
        }

        public void SetTimer()
        {
            TimeIsUp = false;
            _levelTime = 60;
        }

        public void SetUI(bool isVisible)
        {
            PauseButton.gameObject.SetActive(isVisible);
            PlayButton.gameObject.SetActive(!isVisible);
            GameOver.gameObject.SetActive(!isVisible);
        }


        private void Move()
        {
#if UNITY_EDITOR 
            if (!GameXonix.gameOverOrPause.IsPaused() && !GameXonix.gameOverOrPause.IsGameOver())
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.RIGHT);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.UP);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.LEFT);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameXonix.xonix.SetDirection(GameXonix.DOWN);
                }
            }
#endif

#if UNITY_ANDROID
            if (!GameXonix.gameOverOrPause.IsPaused() && !GameXonix.gameOverOrPause.IsGameOver())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _touchPosition = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    var deltaSwipe = _touchPosition - Input.mousePosition;
                    if (Mathf.Abs(deltaSwipe.x) > _swipeResistanceX)
                    {
                        //Swipe on the X axis
                        GameXonix.xonix.SetDirection((deltaSwipe.x < 0) ? GameXonix.RIGHT : GameXonix.LEFT);
                    }
                    else if (Mathf.Abs(deltaSwipe.y) > _swipeResistanceY)
                    {
                        //Swipe on the Y axis
                        GameXonix.xonix.SetDirection((deltaSwipe.y < 0) ? GameXonix.DOWN : GameXonix.UP);
                    }
                }
            }
#endif
        }

        private void CountDownLevelTime()
        {
            if (GameXonix.gameOverOrPause.IsPaused() || GameXonix.gameOverOrPause.IsGameOver()) return;
            if ((_levelTime > 0))
            {
                _levelTime -= Time.deltaTime;
                Timer.text = "Time left: " + Mathf.Round(_levelTime).ToString();
            }
            else
                TimeIsUp = true;
        }

        public void OnPlayButton()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public void OnPauseButton()
        {
            GameXonix.gameOverOrPause.OnPause();
            if (GameXonix.gameOverOrPause.IsPaused())
            {
                PauseButton.gameObject.SetActive(false);
                PlayButton.gameObject.SetActive(true);
            }
            else
            {
                PauseButton.gameObject.SetActive(true);
                PlayButton.gameObject.SetActive(false);
            }
        }

        public void SetLivesCount(int live)
        {
            Lives.text = "HP: " + live.ToString();
        }

        public void SetFillAmount(int fillArea)
        {
            Fill.text = "Fill: " + Mathf.Round(fillArea).ToString() + " %";
        }

        public void SetLevelNum(int level)
        {
            Level.text = "Level: " + level.ToString();
        }
    }
}