using UnityEngine;

namespace Utility
{
    public class AutoDestruction : MonoBehaviour
    {
        public float lifeTime;


        private void Start()
        {
            if (lifeTime <= 0) DestroySelf();
            else Invoke(nameof(DestroySelf), lifeTime);
        }


        private void DestroySelf()
        { Destroy(this.gameObject); }
    }
}
