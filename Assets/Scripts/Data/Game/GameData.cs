using Data.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(menuName = "Data/Game")]
    public class GameData : ScriptableObject
    {
        [Header("Settings")]
        public List<LevelData> levels;
        [Header("Runtime")]
        public GameState gameState;

        [System.Serializable]
        public struct GameState
        {
            public string Name;
            public LevelData currentLevel;
        }
    }
}
