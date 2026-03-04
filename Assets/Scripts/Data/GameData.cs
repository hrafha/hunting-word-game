using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    public enum Theme { Fruits, Vegetables, Colors }

    public class GameData
    {
        public int? currentlevel;
        public Theme? currentTheme;
    }
}
