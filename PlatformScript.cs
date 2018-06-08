// Skrypt odpowiadający za kolizję platformy z Landerem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            // jesli rakieta porusza się za szybko lub jest zbyt przechylone, rozbija się.
            if (Mathf.Abs(collision.gameObject.transform.rotation.eulerAngles.z) > 5 ||
               collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 2)
            {
                collision.gameObject.GetComponent<RocketScript>().SetState(new LanderBoom(collision.gameObject.GetComponent<RocketScript>()));
            }
            else
                collision.gameObject.GetComponent<RocketScript>().SetState(new LanderLanded(collision.gameObject.GetComponent<RocketScript>()));

        }
    }
}
