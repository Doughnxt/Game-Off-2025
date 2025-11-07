using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedDoor : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private WeightedPlate plate;

    private void Start()
    {
        transform.position = startPos.position;
    }

    private void Update()
    {
        if (plate.isWeighedDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, moveSpeed * Time.deltaTime);
        }
    }

}
