using UnityEngine;
using GameSystems.Level;
using UnityEngine.Events;
using Data.Game;

namespace GameSystems.Controllers
{
    public class GameController : MonoBehaviour
    {

        public Theme theme;

        public int amountOfWords;

        public string[] themeWords;
        public string[] gameWords;

        public bool[] availableThemeWords { get; private set; }
        public bool[] wordsFound { get; private set; }


        public event UnityAction<string> OnRemoveWordFromGame = null;


        private void Awake()
        {
            Setup(GetThemeWords());
        }

        private void OnEnable()
        {
            OnRemoveWordFromGame += (word) => CheckGameOver();
        }
        private void OnDisable()
        {
            OnRemoveWordFromGame -= (word) => CheckGameOver();
        }

        public void Setup(string[] words, int? amount = null)
        {
            themeWords = words;
            availableThemeWords = new bool[themeWords.Length];

            SetAmountOfWords(amount);

            gameWords = GetGameWords();
            wordsFound = new bool[gameWords.Length];
        }

        private void CheckGameOver()
        {
            if (GameOver())
            {
                Time.timeScale = 0f;

                PlayerManager.Player.gameplayLevel.Value++;
            }
        }

        private void SetAmountOfWords(int? value = null)
        {
            //LevelGenerator level = FindFirstObjectByType<LevelGenerator>();
            //if (level.difficulty == Difficulty.Easy)
            //    amountOfWords = 5;
            //if (level.difficulty == Difficulty.Normal)
            //    amountOfWords = 7;
            //if (level.difficulty == Difficulty.Hard)
            //    amountOfWords = 10;

            if (value != null)
                amountOfWords = value.Value;

            if (amountOfWords > themeWords.Length)
                amountOfWords = themeWords.Length;
        }

        private string[] GetThemeWords()
        {
            if (theme == Theme.Fruits)
                return new string[] { "BANANA", "GRAPE", "ACEROLA", "MANGO", "APPLE", "PEAR", "PEACH",
                    "ORANGE", "PITANGA", "PAPAYA", "LEMON", "GUAVA", "CHERRY", "AVOCADO" };
            if (theme == Theme.Vegetables)
                return new string[] { "POTATO", "ONION", "CORN", "CARROT", "GINGER", "BEET", "MANIOC", "EGGPLANT"
                , "YAM", "CHUCHU", "POD", "PEA", "JELLY", "OKRA", "PUMPKIN", "PEPPER" };
            if (theme == Theme.Colors)
                return new string[] { "RED", "GREEN", "BLUE", "CYAN", "MAGENTA", "YELLOW", "BLACK", "PURPLE", "PINK"
                , "WHITE", "GRAY", "BROWN", "ORANGE", "GREY" };
            return null;
        }

        private string[] GetGameWords()
        {
            string[] selectedWords = new string[amountOfWords];

            for (int i = 0; i < amountOfWords; i++)
                selectedWords[i] = RandomThemeWord();

            return selectedWords;
        }

        private string RandomThemeWord()
        {
            int r = Random.Range(0, themeWords.Length);
            while (availableThemeWords[r] == true)
                r = Random.Range(0, themeWords.Length);

            availableThemeWords[r] = true;
            return themeWords[r];
        }

        public bool GameOver()
        {
            int count = 0;
            for (int i = 0; i < wordsFound.Length; i++)
            {
                if (wordsFound[i])
                    count++;
            }
            return count == amountOfWords;
        }

        public void RemoveWordFromGame(string word)
        {
            for (int i = 0; i < gameWords.Length; i++)
            {
                if (word == gameWords.GetValue(i).ToString())
                    wordsFound[i] = true;
            }
            OnRemoveWordFromGame?.Invoke(word);
        }
    }
}