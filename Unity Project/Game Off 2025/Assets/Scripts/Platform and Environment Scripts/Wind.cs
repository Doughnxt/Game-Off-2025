using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windStrength = 300;
    public bool windActive;
    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (windActive && player.movementEnabled)
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * windStrength);
    }
}
