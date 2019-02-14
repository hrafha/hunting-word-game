using UnityEngine;
using Scripts.Controllers;

namespace Scripts.Level
{
    public class LevelGenerator : MonoBehaviour
    {

        private GameController gameController;

        [SerializeField] private Slot slotPrefab;

        public Transform gridZeroPos;

        public Difficulty difficulty;

        public Slot[,] slots { get; private set; }

        public int columns { get; private set; }
        public int lines { get; private set; }

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();

            CheckDifficulty();
            CreateGrid();
            FitWords();
            FillEmptySlots();
        }

        private void CheckDifficulty()
        {
            if (difficulty == Difficulty.Easy)
            {
                lines = 9;
                columns = 10;
            }
            if (difficulty == Difficulty.Normal)
            {
                lines = 9;
                columns = 12;
            }
            if (difficulty == Difficulty.Hard)
            {
                lines = 9;
                columns = 14;
            }
        }

        private void CreateGrid()
        {
            slots = new Slot[columns, lines];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    slots[i, j] = Instantiate(slotPrefab, GetGridPos(i, j), Quaternion.identity, transform);
                }
            }
        }

        private void FitWords()
        {
            foreach (string word in gameController.gameWords)
            {
                int x = Random.Range(0, columns);
                int y = Random.Range(0, lines);
                int path = -1;
                int lettersPlaced = 0;

                while (lettersPlaced < word.Length)
                {
                    x = Random.Range(0, columns);
                    y = Random.Range(0, lines);
                    path = WordFitsIn(word, x, y);

                    if (path == 0) // Recently implemented to reduce a little the chances of word to fit the diagonal
                        if (Random.Range(0, 100) <= 50)
                            path = -1;

                    while (path == -1)
                    {
                        x = Random.Range(0, columns);
                        y = Random.Range(0, lines);
                        path = WordFitsIn(word, x, y);
                    }
                    // Tests if the path is valid to insert the word
                    lettersPlaced = GetWordPath(word, x, y, path, lettersPlaced);
                }
                InsertWord(word, x, y, path);
            }
        }

        private void FillEmptySlots()
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    if (slots[i, j].letter.value == 0)
                        slots[i, j].letter.SetRandomValue();
                }
            }
        }

        private Vector3 GetGridPos(int columns, int lines)
        {
            return new Vector3(columns, lines) + gridZeroPos.position;
        }

        private int WordFitsIn(string word, int x, int y)
        {
            if (x + word.Length - 1 < columns && y + word.Length - 1 < lines)
                return 0; // Diagonal
            else if (x + word.Length - 1 < columns)
                return 1; // Horizontal
            else if (y + word.Length - 1 < lines)
                return 2; // Vertical
            return -1;
        }

        private int GetWordPath(string word, int x, int y, int way, int lettersCount)
        {
            if (way == 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (slots[x + i, y + i].letter.value == 0)
                        lettersCount++;
                    else if (slots[x + i, y + i].letter.value == word[i])
                        lettersCount++;
                    else
                        return 0;
                }
            }
            else if (way == 1)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (slots[x + i, y].letter.value == 0)
                        lettersCount++;
                    else if (slots[x + i, y].letter.value == word[i])
                        lettersCount++;
                    else
                        return 0;
                }
            }
            else if (way == 2)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (slots[x, y + i].letter.value == 0)
                        lettersCount++;
                    else if (slots[x, y + i].letter.value == word[i])
                        lettersCount++;
                    else
                        return 0;
                }
            }
            return lettersCount;
        }

        private void InsertWord(string word, int x, int y, int way)
        {
            if (way == 0) // Diagonal
                for (int i = 0; i < word.Length; i++)
                    slots[x + i, y + i].letter.SetValue(word, i);
            else if (way == 1) // Horizontal
                for (int i = 0; i < word.Length; i++)
                    slots[x + i, y].letter.SetValue(word, i);
            else if (way == 2) // Vertical
                for (int i = 0; i < word.Length; i++)
                    slots[x, y + i].letter.SetValue(word, i);
        }

        public enum Difficulty { Easy, Normal, Hard };
    }
}