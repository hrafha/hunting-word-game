using UnityEngine;
using UnityEngine.UI;
using Scripts.Controllers;

namespace Scripts.HUDs
{
    public class GameHUD : MonoBehaviour
    {

        private GameController gameController;

        [SerializeField] GameObject gameOverMenu;
        [SerializeField] Text theme;
        [SerializeField] Text wordsLeft;
        [SerializeField] Text wordsFound;

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
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