using Data.Level;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(menuName = "Data/GD_HuntingWords")]
    public class GD_HuntingWords : GameData
    {
        public List<DifficultySettings> difficultySettings;
        public List<ThemeWordsSettings> themeWordsSettings;
    }

    #region Classes

    [System.Serializable]
    public class DifficultySettings
    {
        public Difficulty difficulty;
        public int amountOfWords;
        public int wordsFoundHandcap;
    }

    [System.Serializable]
    public class ThemeWordsSettings
    {
        public Theme theme;
        public string[] themeWordsOffline;
        //public string[] themeWordsOnline; //TODO: Method to bind words online.
    }

    #endregion /Classes

    #region Enums

    public enum Difficulty { Easy, Normal, Hard };
    public enum Theme { Fruits, Vegetables, Colors }

    #endregion /Enums
}
