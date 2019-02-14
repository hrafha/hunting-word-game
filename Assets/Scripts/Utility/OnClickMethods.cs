using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utility
{
    public class OnClickMethods : MonoBehaviour
    {
        
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

    }
}