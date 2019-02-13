using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private GameController gameController;

    [SerializeField] private Slot slot;
    [SerializeField] private Transform gridZeroPos;

    public Difficulty difficulty;

    private int columns;
    private int lines;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        CheckDifficulty();
        CreateGrid();
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
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < lines; j++)
            {
                Instantiate(slot, GetGridPos(i, j), Quaternion.identity, transform);
            }
        }
    }

    private Vector3 GetGridPos(int lines, int columns)
    {
        return new Vector3(lines, columns) + gridZeroPos.position;
    }

    public enum Difficulty { Easy, Normal, Hard };
}
