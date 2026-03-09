using UnityEngine;
using UnityEngine.UI;
using GameSystems.Controllers;

namespace GameSystems.Level
{
    [RequireComponent(typeof(Image))]
    public class Slot : MonoBehaviour
    {
        private WordSelectionController wordSelection;

        public Letter letter;

        private void Start()
        {
            wordSelection = FindFirstObjectByType<WordSelectionController>();
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
                wordSelection.SetFirstSlot(this);

            if (Input.GetMouseButtonUp(0))
                wordSelection.SetLastSlot(this);
        }

    }
}