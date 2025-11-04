using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float transitionTime = .3f;
    [SerializeField] private Animator transition;
    //private SavepointManager savepointManager;
    private bool lowerVolume;

    private void Start()
    {
        //savepointManager = FindObjectOfType<SavepointManager>();
    }

    private void Update()
    {
        if (lowerVolume)
        {
            if (AudioListener.volume > 0)
            {
                AudioListener.volume -= Time.deltaTime;
            }
        }
    }

    public void LoadNextLevel()
    {
        lowerVolume = true;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void QuitToMenu()
    {
        lowerVolume = true;
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}