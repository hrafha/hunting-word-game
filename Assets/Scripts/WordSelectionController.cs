using UnityEngine;

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
            lastPos = ToGridPos(firstSlot.transform.position);
            Debug.Log(SelectionPath());
        }
    }

    private int SelectionPath()
    {
        for (int i = (int)firstPos.x; i < level.columns; i++)
        {
            if (new Vector3((int)firstPos.x + i, (int)firstPos.y + i) == lastPos)
                return 0; // Diagonal

            if (new Vector3(i, (int)firstPos.y) == lastPos)
                return 1; // Horizontal

            for (int j = (int)firstPos.y; j < level.lines; j++)
                if (new Vector3((int)firstPos.x, j) == lastPos)
                    return 2; // Vertical
        }
        return -1; // None
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
