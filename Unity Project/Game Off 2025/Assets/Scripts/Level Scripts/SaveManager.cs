using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;


    [Header("Saved Values")]
    public Vector2 lastCheckpointPos;
    public Vector2 lastSavepointPos;
    public bool dashObtained;
    public bool dashUpgraded;
    public bool transitioningToNextLevel;

    [Header("Denial Level Values")]
    public bool windCutsceneWatched;
    public bool stringsCutsceneWatched;

    [Header("Anger Level Values")]
    public bool dashCutsceneWatched;

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

}