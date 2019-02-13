using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private GameController gameController;

    [SerializeField] private Slot slot;
    [SerializeField] private Transform gridZeroPos;

    public Difficulty difficulty;

    private Letter[,] gridLetter;

    private int columns;
    private int lines;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        CheckDifficulty();
        CreateGrid();
        FitWords();
    }

    private void CheckDifficulty()
    {
        if(difficulty == Difficulty.Easy)
        {
            lines = 8;
            columns = 9;
        }
        if (difficulty == Difficulty.Normal)
        {
            lines = 9;
            columns = 10;
        }
        if (difficulty == Difficulty.Hard)
        {
            lines = 10;
            columns = 12;
        }
    }

    private void CreateGrid()
    {
        gridLetter = new Letter[columns, lines];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < lines; j++)
            {
                gridLetter[i, j] = Instantiate(slot, GetGridPos(i, j), Quaternion.identity, transform).letter;
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

            while(lettersPlaced < word.Length)
            {
                x = Random.Range(0, columns);
                y = Random.Range(0, lines);
                path = WordFitsIn(word, x, y);

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
                if (gridLetter[x + i, y + i].value == 0)
                    lettersCount++;
                else if (gridLetter[x + i, y + i].value == word[i])
                    lettersCount++;
                else
                    return 0;
            }
        }
        else if (way == 1)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (gridLetter[x + i, y].value == 0)
                    lettersCount++;
                else if (gridLetter[x + i, y].value == word[i])
                    lettersCount++;
                else
                    return 0;
            }
        }
        else if (way == 2)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (gridLetter[x, y + i].value == 0)
                    lettersCount++;
                else if (gridLetter[x, y + i].value == word[i])
                    lettersCount++;
                else
                    return 0;
            }
        }
        return lettersCount;
    }

    private void InsertWord(string word, int x, int y, int way)
    {
        if(way == 0) // Diagonal
            for (int i = 0; i < word.Length; i++)
                gridLetter[x + i, y + i].SetValue(word, i);
        else if (way == 1) // Horizontal
            for (int i = 0; i < word.Length; i++)
                gridLetter[x + i, y].SetValue(word, i);
        else if (way == 2) // Vertical
            for (int i = 0; i < word.Length; i++)
                gridLetter[x, y + i].SetValue(word, i);
    }

    public enum Difficulty { Easy, Normal, Hard };
}
