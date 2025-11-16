using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVisuals : MonoBehaviour
{
    [SerializeField] private GameObject windParticle;
    [SerializeField] private float distanceFromCameraCenter = 25f;
    [SerializeField] private float timeBetweenParticleCreation = 0.05f;
    private bool canCreateParticle;
    private Transform cameraPos;
    private float yPos;

    private void Start()
    {
        canCreateParticle = true;
        cameraPos = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        yPos = Random.Range(-5,30);
        if (canCreateParticle)
        {
            StartCoroutine(CreateParticle());
        }
        
    }
    private IEnumerator CreateParticle()
    {
        canCreateParticle = false;
        Instantiate(windParticle, new Vector3(cameraPos.position.x + distanceFromCameraCenter, yPos, 0), Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenParticleCreation);
        canCreateParticle = true;
    }
}
