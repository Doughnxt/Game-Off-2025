using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool on;
    [SerializeField] private Range range;


    void Update()
    {
        if (range.inRange)
        {
            // SHOW TEXT TO INTERACT
            if (Input.GetButtonDown("Interact"))
            {
                on = !on;
            }
        }
        else
        {
            // GET RID OF TEXT TO INTERACT
        }
    }
}
