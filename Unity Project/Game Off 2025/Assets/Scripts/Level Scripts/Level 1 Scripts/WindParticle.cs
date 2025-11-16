using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    private Transform cameraPos;
    private Rigidbody2D rb;
    private void Start()
    {
        float xScale = Random.Range(1.3f,6);
        transform.localScale = new Vector3(xScale, 0.1229265f, 1);
        rb = GetComponent<Rigidbody2D>();
        cameraPos = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        rb.AddForce(Vector2.left * speed);
        if (transform.position.x < cameraPos.position.x - 25)
        {
            Destroy(gameObject);
        }
    }
}
