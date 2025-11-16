using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlate : MonoBehaviour
{
    public bool isWeighedDown;
    private int numberOfCollidingObjects;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite weighedDownSprite;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isWeighedDown = numberOfCollidingObjects > 0;

        if (isWeighedDown)
        {
            sprite.sprite = weighedDownSprite;
        }
        else
        {
            sprite.sprite = defaultSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfCollidingObjects++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PushableBlock>() != null)
        {
            numberOfCollidingObjects--;
        }
    }
}
