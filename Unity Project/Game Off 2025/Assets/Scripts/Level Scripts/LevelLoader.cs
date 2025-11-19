using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float transitionTime = .7f;
    [SerializeField] private Animator transition;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Color mainFadeColor;
    [SerializeField] private Color whiteFadeColor;
    private SaveManager saveManager;
    private bool lowerVolume;

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager.transitioningToNextLevel)
        {
            fadeImage.color = whiteFadeColor;
            saveManager.transitioningToNextLevel = false;
        }
        else
        {
            fadeImage.color = mainFadeColor;
        }
        Invoke(nameof(ResetFadeColor), 2 * transitionTime);
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
        if (saveManager.transitioningToNextLevel)
        {
            fadeImage.color = whiteFadeColor;
        }
    }

    private void ResetFadeColor()
    {
        fadeImage.color = mainFadeColor;
    }

    public void LoadNextLevel()
    {
        fadeImage.color = whiteFadeColor;
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