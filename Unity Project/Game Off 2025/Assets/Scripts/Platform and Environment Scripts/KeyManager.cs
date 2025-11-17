using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public int keyCount;
    [SerializeField] private GameObject keyLine;

    private void Update()
    {
        if (keyCount > 0)
        {
            keyLine.SetActive(true);
        }
        else
        {
            keyLine.SetActive(false);
        }
    }

}

