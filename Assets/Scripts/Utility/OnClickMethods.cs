using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utility
{
    using Core;

    public class OnClickMethods : MonoBehaviour
    {
        
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void LoadScene(int appScene)
        {
            ApplicationScene scene = (ApplicationScene)appScene;
            //switch (scene)
            //{
            //    case ApplicationScene.Home:
            //        break;
            //    case ApplicationScene.Gameplay:
            //        break;
            //}
            ApplicationManager.LoadScene(scene);
        }

    }
}