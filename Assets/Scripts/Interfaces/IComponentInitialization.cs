using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IComponentInitialization
    {
        public bool Initialized()
        {
            return true;
        }
    }
}
