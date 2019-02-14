using UnityEngine;
using Scripts.HUDs;
using Scripts.Level;

namespace Scripts.Controllers
{
    public class WordSelectionController : MonoBehaviour
    {

        [SerializeField] private LineRenderer selectionLine;

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
            UpdateSelectionLine();
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
                if (ValidSelection() && StillNotFound(WordSelected()))
                {
                    FindObjectOfType<GameController>().RemoveWordFromGame(WordSelected());
                    FindObjectOfType<GameHUD>().WordsUpdate(WordSelected());
                }
            }
        }

        private bool ValidSelection()
        {
            return SelectionLenght(SelectionPath()) >= 0;
        }

        private string WordSelected()
        {
            string word = "";
            int path = SelectionPath();
            int amountLetters = SelectionLenght(path);
            int x = (int)firstPos.x;
            int y = (int)firstPos.y;

            if (path == 0) // Diagonal
                for (int i = 0; i < amountLetters; i++)
                    word += level.slots[x + i, y + i].letter.value;
            else if (path == 1) // Horizontal
                for (int i = 0; i < amountLetters; i++)
                    word += level.slots[x + i, y].letter.value;
            else if (path == 2) // Vertical
                for (int i = 0; i < amountLetters; i++)
                    word += level.slots[x, y + i].letter.value;
            return word;
        }

        private bool StillNotFound(string word)
        {
            GameController gameController = FindObjectOfType<GameController>();
            for (int i = 0; i < gameController.gameWords.Length; i++)
            {
                if (word == gameController.gameWords.GetValue(i).ToString())
                    return true;
            }
            return false;
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

        private void UpdateSelectionLine()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if (Input.GetMouseButtonDown(0))
            {
                SetLineColor(Color.cyan);
                selectionLine.SetPosition(0, firstSlot.transform.position);
            }
            if (Input.GetMouseButton(0)) // Is selecting
                selectionLine.SetPosition(1, mousePos);
            if (Input.GetMouseButtonUp(0))
            {
                if (ValidSelection() && StillNotFound(WordSelected()))
                {
                    SetLineColor(Color.green);
                    LineRenderer newLine = selectionLine;
                    Instantiate(newLine);
                    SetLineColor(Color.clear);
                }
                else if (ValidSelection())
                    SetLineColor(Color.red);
                else
                    SetLineColor(Color.clear);
            }
        }

        private void SetLineColor(Color color)
        {
            if (color != Color.clear)
                color = new Color(color.r, color.g, color.b, 0.4f);
            selectionLine.startColor = color;
            selectionLine.endColor = color;
        }

    }
}