using Data.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Level
{
    [CreateAssetMenu(menuName = "Data/Level")]
    public class LevelData : ScriptableObject
    {
        public int level = -1;
        public Difficulty difficulty;
        public ThemeWords themeWords;
    }

    [System.Serializable]
    public class ThemeWords //TODO: Fill these by searching Theme word online. Via AI.
    {
        public Theme theme;
        public List<string> levelWords = new List<string>();

        //public string[] strings = new string[] { "BANANA", "GRAPE", "ACEROLA", "MANGO", "APPLE", "PEAR", "PEACH",
        //            "ORANGE", "PITANGA", "PAPAYA", "LEMON", "GUAVA", "CHERRY", "AVOCADO" };
        //public string[] stringsx = new string[] { "POTATO", "ONION", "CORN", "CARROT", "GINGER", "BEET", "MANIOC", "EGGPLANT"
        //        , "YAM", "CHUCHU", "POD", "PEA", "JELLY", "OKRA", "PUMPKIN", "PEPPER" };
        //public string[] stringsz = new string[] { "RED", "GREEN", "BLUE", "CYAN", "MAGENTA", "YELLOW", "BLACK", "PURPLE", "PINK"
        //        , "WHITE", "GRAY", "BROWN", "ORANGE", "GREY" };
    }
}