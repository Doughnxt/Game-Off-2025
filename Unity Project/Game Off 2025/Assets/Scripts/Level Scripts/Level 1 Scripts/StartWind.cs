using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : MonoBehaviour
{
    [SerializeField] private float textShowTime = 2f;
    private PlayerMovement player;
    private Wind wind;
    private WindVisuals visuals;


    private void Start()
    {
        wind = FindObjectOfType<Wind>();
        visuals = FindObjectOfType<WindVisuals>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WindStartingCutscene());
        }
    }

    private IEnumerator WindStartingCutscene()
    {
        player.movementEnabled = false;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        // Animation trigger
        yield return new WaitForSeconds(textShowTime);
        player.movementEnabled = true;
        wind.windActive = true;
        visuals.gameObject.SetActive(true);
    }

}
