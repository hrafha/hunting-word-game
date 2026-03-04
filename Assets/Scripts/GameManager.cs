using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Controllers;
using Scripts.Data;
using Scripts.HUDs;
using Scripts.Level;
using Scripts.Utility;

public class GameManager : MonoBehaviour
{
    
    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = Instantiate(new GameObject("GameManager").AddComponent<GameManager>());
            return instance;
        }
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance.gameObject != this.gameObject)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }

    #endregion /Singleton

    /// Register static references of all project systems here.
    #region Systems

    public static GameData gameData;
    public static GameController gameController;
    public static WordSelectionController wordSelectionController;
    public static GameHUD gameHUD;
    public static LevelGenerator levelGenerator;

    #endregion /Systems

    /// Update this functions after with every new systems.
    #region Bind Systems

    private void OnLevelWasLoaded(int level)
    {
        BindSystems();
        BindGameData();
    }

    private void BindSystems()
    {
        gameController = FindFirstObjectByType<GameController>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        wordSelectionController = FindFirstObjectByType<WordSelectionController>();
        gameHUD = FindFirstObjectByType<GameHUD>();
    }

    private void BindGameData()
    {
        if (gameData == null)
            gameData = new GameData();
    }

    #endregion /Bind Systems


}
