using UnityEngine;
using Data.Game;
using Data.User;
using GameSystems.Controllers;
using GameSystems.HUDs;
using GameSystems.Level;
using GameSystems.Utility;
using Utility;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Data.Level;
using Core;

public class GameManager : MonoBehaviour
{
    #region Events

    public static event UnityAction<Scene, LoadSceneMode> OnSceneLoadedEvent = null;

    #endregion /Events

    #region Singleton

    private static GameManager instance = null;

    public static GameManager Instance { get => instance; private set => instance = value; }

    private void SingletonAwake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion /Singleton

    #region Unity

    private void Awake()
    {
        SingletonAwake();
        BindSystemsAndDatas();
        RefreshSystems();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        OnSceneLoadedEvent = null;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene " + scene.name + " loaded with mode: " + mode);

        BindSystemsAndDatas();
        RefreshSystems();

        OnSceneLoadedEvent?.Invoke(scene, mode);
    }

    #endregion /Unity

    /// Register static references of all project systems here.
    #region Systems

    [Header("Datas")]
    [SerializeField] private GameData gameData;
    //[SerializeField] private PlayerData playerData;
    [Header("Systems")]
    [SerializeField] private GameController gameController;
    [SerializeField] private WordSelectionController wordSelectionController;
    [SerializeField] private GameHUD gameHUD;
    [SerializeField] private LevelGenerator levelGenerator;

    public static GameData GameData { get => Instance.gameData; set => Instance.gameData = value; }
    public static LevelData CurrentLevel => GameData?.gameState.currentLevel;

    public static GameController GameController { get => Instance.gameController; set => Instance.gameController = value; }
    public static WordSelectionController WordSelectionController { get => Instance.wordSelectionController; set => Instance.wordSelectionController = value; }
    public static GameHUD GameHUD { get => Instance.gameHUD; set => Instance.gameHUD = value; }
    public static LevelGenerator LevelGenerator { get => Instance.levelGenerator; set => Instance.levelGenerator = value; }

    #endregion /Systems

    /// <summary>
    /// Update this function after with every new systems.
    /// </summary>
    #region Bind Systems and Datas

    private void BindSystemsAndDatas()
    {
        if (GameData == null)
            GameData = new GameData();

        RefreshCurrentLevel();

        GameController = FindFirstObjectByType<GameController>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        wordSelectionController = FindFirstObjectByType<WordSelectionController>();
        gameHUD = FindFirstObjectByType<GameHUD>();
    }

    #endregion /Bind Systems

    /// <summary>
    /// Update this function after with every new systems.
    /// </summary>
    #region Refresh Systems

    private void RefreshSystems()
    {
        LevelData levelData = CurrentLevel;
        RefreshGameController(levelData);
        RefreshLevelGenerator(levelData);
        RefreshWordSelectionController();
        RefreshGameHUD();
    }

    private void RefreshCurrentLevel()
    {
        var GD = gameData as GD_HuntingWords;
        if (GD == null) return;
        if (GD.levels.Count == 0)
            GD.levels = ApplicationManager.gameplayLevels.Assets;

        int maxLevel = gameData.levels.Count - 1;
        if (maxLevel <= 0)
            return; //TODO: lvData = LevelData.GetDummy();

        LevelData levelData = new LevelData()
        {
            level = 0,
            difficultySettings = gameData.levels[Random.Range(0, maxLevel)].difficultySettings,
            themeWordsSettings = gameData.levels[Random.Range(0, maxLevel)].themeWordsSettings,
        };

        var player = PlayerManager.Player;
        if (player != null)
            levelData = gameData.levels[Mathf.Min(player.gameplayLevel.Value, maxLevel)];

        gameData.gameState.currentLevel = levelData;
    }

    private void RefreshGameController(LevelData levelData)
    {
        if (gameController != null)
        {
            gameController.Setup(levelData.themeWordsSettings.themeWordsOffline, levelData.difficultySettings.amountOfWords);
            gameController.theme = levelData.themeWordsSettings.theme;
            //gameController.difficulty = levelData.difficultySettings.difficulty; //TODO: controller challanges?
            //gameController.wordsFoundHandcap = levelData.difficultySettings.wordsFoundHandcap; //TODO: wordsFoundHancap system
        }
    }

    private void RefreshLevelGenerator(LevelData levelData)
    {
        if (levelGenerator != null)
        {
            levelGenerator.gameController = gameController;
            levelGenerator.difficulty = levelData.difficultySettings.difficulty;
        }
    }

    private void RefreshWordSelectionController()
    {
        if (wordSelectionController != null)
            wordSelectionController.levelGenerator = levelGenerator;
    }

    private void RefreshGameHUD()
    {
        if (gameHUD != null)
            gameHUD.gameController = gameController;
    }

    #endregion /Refresh Systems

}
