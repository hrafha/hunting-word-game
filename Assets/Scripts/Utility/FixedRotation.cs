using UnityEngine;

namespace Utility
{
    public class FixedRotation : MonoBehaviour
    {
        public Vector3 fixedEulerAngles;


        private void LateUpdate()
        { Behaviour(); }


        private void Behaviour()
        { this.transform.eulerAngles = fixedEulerAngles; }
    }
}
