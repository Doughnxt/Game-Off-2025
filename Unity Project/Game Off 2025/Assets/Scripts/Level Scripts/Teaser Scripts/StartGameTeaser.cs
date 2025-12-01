using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTeaser : MonoBehaviour
{
    [SerializeField] private GameObject noteText;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject newBackground;
    [SerializeField] private GameObject levelTransition;
    private bool canStart;

    public void StartGame()
    {
        noteText.SetActive(true);
        Invoke(nameof(ShowText), 5f);
    }

    private void ShowText()
    {
        text.SetActive(true);
        canStart = true;
    }

    private void Update()
    {
        if (canStart && Input.anyKeyDown)
        {
            StartCoroutine(GameStart());
        }
    }
    private IEnumerator GameStart()
    {
        canStart = false;
        newBackground.SetActive(true);
        noteText.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1);
        levelTransition.SetActive(true);
    }
}
