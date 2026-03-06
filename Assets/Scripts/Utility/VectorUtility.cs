using UnityEngine;

namespace Utility
{
    public static class VectorUtility
    {
        public static int RandomRange(this Vector2Int vector)
        { return Random.Range(vector.x, vector.y); }

        public static float RandomRange(this Vector2 vector)
        { return Random.Range(vector.x, vector.y); }
    }
}
