using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Savepoint : MonoBehaviour
{
    private SaveManager saveM;

    private void Start()
    {
        saveM = FindObjectOfType<SaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            saveM.lastCheckpointPos = transform.position;
        }
    }

}