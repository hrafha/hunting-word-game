using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    using Sound;
    using Data.Level;

    public struct AssetReference<T> where T : ScriptableObject
    {
        private List<T> assets;

        public List<T> Assets
        {
            get
            {
#if UNITY_EDITOR
                if (assets == null)
                    return new List<T>(AssetsManager.FindAssetsByType<T>());
#endif
                return assets;
            }

            internal set { assets = value; }
        }


        public void SetAssets(List<T> values)
        {
            assets = new List<T>(values);
        }
    }


    public class AssetsManager : MonoBehaviour
    {
        public int levelToLoop;
        public List<LevelData> levels;
        public SoundSettings soundSettings;


        private void Awake()
        {
            Initialize();
        }


        void Initialize()
        {
            ApplicationManager.levelToLoop = levelToLoop;
            ApplicationManager.gameplayLevels.SetAssets(levels);
            SoundManager.Settings = soundSettings;
        }


#if UNITY_EDITOR
        internal static IEnumerable<T> FindAssetsByType<T>() where T : Object
        {
            var guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T)}");
            foreach (var t in guids)
            {
                var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(t);
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    yield return asset;
                }
            }
        }
#endif
    }
}
