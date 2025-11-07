using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float rotationRadius = 2f;
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float direction = 1;
    private float posX = 0f;
    private float posY = 0f;
    private float angle = 0f;

    void Update()
    {
        posX = rotationPoint.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationPoint.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY * direction);
        angle += Time.deltaTime * swingSpeed;

        if (angle == 360)
        {
            angle = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stay on Platform"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
