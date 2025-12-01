using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTeaser : MonoBehaviour
{
    [SerializeField] private GameObject noteText;
    [SerializeField] private GameObject pressAnywhereText;
    [SerializeField] private GameObject levelTransition;
    [SerializeField] private GameObject newBackground;
    private bool canGoOn;

    private void Update()
    {
        if (canGoOn && Input.anyKeyDown)
        {
            StartCoroutine(GameStart());
        }
    }

    public void StartGame()
    {
        noteText.SetActive(true);
        Invoke(nameof(ShowInteractText), 3f);
    }

private void ShowInteractText()
    {
        pressAnywhereText.SetActive(true);
        canGoOn = true;
    }

    private IEnumerator GameStart()
    {
        canGoOn = false;
        newBackground.SetActive(true);
        noteText.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        levelTransition.SetActive(true);
    }
}
