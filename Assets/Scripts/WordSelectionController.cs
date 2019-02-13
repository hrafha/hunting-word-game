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
        }
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
