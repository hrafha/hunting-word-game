using Data.Level;
using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(menuName = "Data/Game")]
    public class GameData : ScriptableObject
    {
        [System.Serializable]
        public struct GameState
        {
            public string Name;
            public LevelData currentLevel;
        }
    }

    public enum Difficulty { Easy, Normal, Hard };
    public enum Theme { Fruits, Vegetables, Colors }

}
