using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;


    // Saved Values
    public Vector2 lastCheckpointPos;
    public Vector2 lastSavepointPos;
    public bool dashObtained;
    public bool dashUpgraded;
    public int lastLevelIndex;
    public int currentLevelIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

}