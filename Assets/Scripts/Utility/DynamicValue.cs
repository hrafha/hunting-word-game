using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public struct DynamicValue<T>
    {
        [SerializeField] private T value;
        public event Action<T> OnChange;

        public T Value
        {
            get => value;

            set
            {
                this.value = value;
                OnChange?.Invoke(this.value);
            }
        }


        public void CopyChangesRegister(DynamicValue<T> origin)
        { OnChange = (Action<T>)origin.OnChange.Clone(); }

        // ToDo: Test implicit operation
        //public static implicit operator T(DynamicValue<T> v) => v.Value;
        //public static implicit operator DynamicValue<T>(T v)
        //{
        //    DynamicValue<T> o = new DynamicValue<T>();
        //    o.Value = v;

        //    return o;
        //}
    }
}
