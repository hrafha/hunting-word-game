using UnityEngine;
using UnityEngine.UI;
using Scripts.Controllers;

namespace Scripts.HUDs
{
    public class GameHUD : MonoBehaviour
    {

        public GameController gameController;

        [SerializeField] GameObject gameOverMenu;
        [SerializeField] Text theme;
        [SerializeField] Text wordsLeft;
        [SerializeField] Text wordsFound;

        [Header("Debug")]
        public string logErrorFormat = "[{0}]> {1}";

        private void Start()
        {
            gameController = FindFirstObjectByType<GameController>();

            if (gameController == null)
            {
                Debug.LogError(string.Format(logErrorFormat, gameObject.name, "Cancel Initialization. GameController not found!"));
                gameObject.SetActive(false);
                return;
            }

            theme.text = gameController.theme.ToString();

            wordsFound.text = "Words Found:\n";
            WordsUpdate(null);
        }

        private void Update()
        {
            gameOverMenu.SetActive(gameController.GameOver());
        }

        public void WordsUpdate(string wordFound)
        {
            // Update words left
            wordsLeft.text = "Words Left:\n";
            for (int i = 0; i < gameController.wordsFound.Length; i++)
            {
                if (!gameController.wordsFound[i])
                    wordsLeft.text += gameController.gameWords[i] + "\n";
            }

            // Update words found if needed
            if (wordFound != null)
                wordsFound.text += wordFound + "\n";
        }

    }
}