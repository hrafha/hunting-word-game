using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{

    private GameController gameController;

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Text wordsLeft;
    [SerializeField] Text wordsFound;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        WordsUpdate();
    }

    private void Update()
    {
        gameOverMenu.SetActive(gameController.GameOver());
    }

    public void WordsUpdate()
    {
        for (int i = 0; i < gameController.wordsFound.Length; i++)
        {
            if (gameController.wordsFound[i])
                wordsFound.text += gameController.gameWords[i] + "\n";
            else
                wordsLeft.text += gameController.gameWords[i] + "\n";
        }
    }

}
