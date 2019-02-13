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
        SetWords();
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

    private void SetWords()
    {
        foreach (string word in gameController.gameWords)
        {
            int x = Random.Range(0, columns);
            int y = Random.Range(0, lines);
            int settedLetters = 0;
            while(settedLetters != word.Length)
            {
                Debug.Log(word);
                x = Random.Range(0, columns);
                y = Random.Range(0, lines);

                while (!WordFitsInPosition(word, x, y))
                {
                    x = Random.Range(0, columns);
                    y = Random.Range(0, lines);
                }

                for (int i = 0; i < word.Length; i++)
                {
                    if (gridLetter[x + i, y].value == 0)
                        settedLetters++;
                    else if (gridLetter[x + i, y].value == word[i] && i != word.Length)
                        settedLetters++;
                }
                Debug.Log(settedLetters == word.Length);
            }
            for (int i = 0; i < word.Length; i++)
                gridLetter[x + i, y].SetValue(word, i);
        }
    }

    private Vector3 GetGridPos(int columns, int lines)
    {
        return new Vector3(columns, lines) + gridZeroPos.position;
    }

    private bool WordFitsInPosition(string word, int x, int y)
    {
        if (x + word.Length - 1 < columns) // Horizontal
            return true;
        /*if (y + word.Length - 1 <= lines) // Vertical
            return true;*/
        return false;
    }

    public enum Difficulty { Easy, Normal, Hard };
}
