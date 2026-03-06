using UnityEngine;

namespace Core
{
    public class SceneLoader : MonoBehaviour
    {
        public ApplicationScene scene;


        private void Start()
        {
            ApplicationManager.LoadScene(scene);
        }
    }
}
