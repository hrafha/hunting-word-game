using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class TransformUtility
    {
        public static void ClearChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }

        public static List<Transform> GetChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
                children.Add(transform.GetChild(i));
            return children;
        }

        public static List<Transform> GetChildren(this Transform transform, out List<Vector3> positions)
        {
            positions = new List<Vector3>();
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
                positions.Add(child.position);
            }
            return children;
        }
    }
}
