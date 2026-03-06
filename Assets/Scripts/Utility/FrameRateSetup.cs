using UnityEngine;

namespace Utility
{
    public class FrameRateSetup : MonoBehaviour
    {
        public int frameRate = 60;


        private void Awake()
        { Application.targetFrameRate = frameRate; }
    }
}
