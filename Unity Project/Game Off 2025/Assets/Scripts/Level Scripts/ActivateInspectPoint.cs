using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInspectPoint : MonoBehaviour
{
    private enum Level { denial, anger, bargaining, depression, acceptance }
    [SerializeField] private Level level;
    [SerializeField] private GameObject cutsceneInspectPoint;
    private SaveManager saveManager;

    private void Start()
    {
        cutsceneInspectPoint.SetActive(false);
        saveManager = FindObjectOfType<SaveManager>();
        switch (level)
        {
            case Level.denial:
                if (saveManager.stringsCutsceneWatched)
                {
                    this.gameObject.SetActive(false);
                }
                break;

            case Level.anger:
                if (saveManager.dashCutsceneWatched)
                {
                    this.gameObject.SetActive(false);
                }
                break;

            case Level.bargaining:
                break;

            case Level.depression:
                break;

            case Level.acceptance:
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cutsceneInspectPoint.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
