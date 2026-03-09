using Data.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Level
{
    [CreateAssetMenu(menuName = "Data/Level")]
    public class LevelData : ScriptableObject
    {
        public int level = -1;
        public DifficultySettings difficultySettings;
        public ThemeWordsSettings themeWordsSettings;

        public static LevelData GetDummy()
        {
            return new LevelData()
            {
                level = 0,
                difficultySettings = new DifficultySettings(),
                themeWordsSettings = new ThemeWordsSettings(),
            };
        }
    }
}