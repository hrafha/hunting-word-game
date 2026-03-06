using UnityEngine;

namespace Utility.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicSafeArea : MonoBehaviour
    {
        [Header("Settings")]
        public Canvas primaryCanvas;

        [Header("Behaviour")]
        private RectTransform rectTransform;
        private Rect bakedRect;

        public RectTransform LocalRect
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = GetComponent<RectTransform>();

                return rectTransform;
            }
        }


        private void Start()
        {
            UpdateArea(Screen.safeArea);
        }


        private void Update()
        {
            if (!this.enabled) return;
            CheckUpdate();
        }


        private void CheckUpdate()
        {
            Rect safeArea = Screen.safeArea;
            if (safeArea != bakedRect)
                UpdateArea(safeArea);
        }


        public void UpdateArea(Rect safeArea)
        {
            RectTransform rect = LocalRect;
            if (rect != null && primaryCanvas != null)
            {
                Vector2 screenResolution = new Vector2(primaryCanvas.pixelRect.width, primaryCanvas.pixelRect.height);

                var anchorMin = safeArea.position;
                var anchorMax = safeArea.position + safeArea.size;
                anchorMin.x /= screenResolution.x;
                anchorMin.y /= screenResolution.y;
                anchorMax.x /= screenResolution.x;
                anchorMax.y /= screenResolution.y;

                rect.anchorMin = anchorMin;
                rect.anchorMax = anchorMax;

                /// Deprecated
                ////Vector2 screenResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
                //Vector2 screenResolution = new Vector2(primaryCanvas.pixelRect.width, primaryCanvas.pixelRect.height);
                ////Vector2 screenResolution = new Vector2(Screen.width, Screen.height);
                ////Vector2 screenResolution = new Vector2(primaryCanvas.renderingDisplaySize.x, primaryCanvas.renderingDisplaySize.y);
                //float canvasRatio = primaryCanvas.transform.localScale.x;
                //Vector2 virtualRatio = new Vector2()
                //{
                //    x = (float)primaryCanvas.renderingDisplaySize.x / (float)screenResolution.x,
                //    y = (float)primaryCanvas.renderingDisplaySize.y / (float)screenResolution.y
                //};

                //Vector2 halfVector = new Vector2(0.5f, 0.5f);
                //rect.anchorMin = halfVector;
                //rect.anchorMax = halfVector;

                //Vector2 offset = safeArea.position;
                //offset -= new Vector2(screenResolution.x - safeArea.width, screenResolution.y - safeArea.height) / 2f;

                //Vector2 size = safeArea.size;

                //offset = new Vector2()
                //{
                //    x = offset.x * virtualRatio.x,
                //    y = offset.y * virtualRatio.y
                //};

                //size = new Vector2()
                //{
                //    x = size.x * virtualRatio.x,
                //    y = size.y * virtualRatio.y
                //};

                //rect.anchoredPosition = offset / canvasRatio;
                //rect.sizeDelta = size / canvasRatio;

                ////Debug.Log("Safe Area: " + safeArea);
                ////Debug.Log("Total Area: " + Screen.currentResolution.width + "x" + Screen.currentResolution.height);

                bakedRect = safeArea;
            }
        }
    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(DynamicSafeArea))]
    public class DynamicSafeAreaEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Screen"))
            {
                DynamicSafeArea myTarget = (DynamicSafeArea)target;
                myTarget.UpdateArea(Screen.safeArea);
            }

            if(GUILayout.Button("Reset Screen"))
            {
                DynamicSafeArea myTarget = (DynamicSafeArea)target;
                RectTransform rect = myTarget.LocalRect;

                if(rect != null)
                {
                    rect.anchorMin = Vector2.zero;
                    rect.anchorMax = Vector2.one;

                    rect.offsetMin = Vector2.zero;
                    rect.offsetMax = Vector2.zero;
                }
            }
        }
    }
#endif
}
