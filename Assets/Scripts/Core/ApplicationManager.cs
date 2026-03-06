using UnityEngine.SceneManagement;

namespace Core
{
    using Data.Level;
    using System.Collections.Generic;

    public static class ApplicationManager
    {
        public static int levelToLoop;
        public static AssetReference<LevelData> gameplayLevels;


        public static LevelData GetLevelData(int level)
        {
            List<LevelData> levels = gameplayLevels.Assets;
            if (levels == null || levels.Count == 0) return null;

            levels.Sort((a, b) => a.level > b.level ? 1 : -1);

            LevelData levelData = levels[0];

            if (level > levels.Count - 1)
            {
                level = levelToLoop + (level % levels.Count);
            }

            for (int i = 0, n = levels.Count; i < n; i++)
            {
                LevelData l = levels[i];
                if (l.level <= level)
                    levelData = l;

                if (l.level >= level)
                    break;
            }

            return levelData;
        }


        public static void LoadScene(ApplicationScene scene)
        {
            string sceneName = GetSceneName(scene);

            if (!string.IsNullOrEmpty(sceneName))
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }


        private static string GetSceneName(ApplicationScene scene)
        {
            switch (scene)
            {
                case ApplicationScene.Home:
                    return "02. Home";

                case ApplicationScene.Gameplay:
                    return "03. Gameplay";
            }

            return null;
        }
    }

    [System.Serializable]
    public enum ApplicationScene
    {
        Home,
        Gameplay,
    }
}
