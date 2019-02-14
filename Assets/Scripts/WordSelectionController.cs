﻿using UnityEngine;

public class WordSelectionController : MonoBehaviour
{

    private LevelGenerator level;

    public Slot firstSlot { get; private set; }
    public Slot lastSlot { get; private set; }

    private Vector3 firstPos;
    private Vector3 lastPos;

    private void Start()
    {
        level = FindObjectOfType<LevelGenerator>();
    }

    private void Update()
    {
        SelectionUpdate();
    }

    private void SelectionUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = ToGridPos(firstSlot.transform.position);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastPos = ToGridPos(lastSlot.transform.position);
        }
    }

    private bool ValidSelection()
    {
        return SelectionLenght(SelectionPath()) >= 0;
    }

    private int SelectionPath()
    {
        int aux = Mathf.Max(level.columns, level.lines);
        for (int i = (int)firstPos.x; i < level.columns; i++)
        {
            for (int k = 0; k < aux; k++)
            {
                if (new Vector3((int)firstPos.x + k, (int)firstPos.y + k) == lastPos)
                    return 0; // Diagonal
            }

            if (new Vector3(i, (int)firstPos.y) == lastPos)
                return 1; // Horizontal

            for (int j = (int)firstPos.y; j < level.lines; j++)
            {
                if (new Vector3((int)firstPos.x, j) == lastPos)
                    return 2; // Vertical
            }
        }
        return -1; // None
    }

    private int SelectionLenght(int path)
    {
        if (path == 0)
            return (int)(lastPos.x - firstPos.x) + 1;
        else if (path == 1 || path == 2)
            return (int)((lastPos.x - firstPos.x) + (lastPos.y - firstPos.y)) + 1;
        else
            return -1;
    }

    private Vector3 ToGridPos(Vector3 position)
    {
        return position - level.gridZeroPos.position;
    }

    public void SetFirstSlot(Slot slot)
    {
        firstSlot = slot;
    }

    public void SetLastSlot(Slot slot)
    {
        lastSlot = slot;
    }

}