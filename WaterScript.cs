// Skrypt odpowiadając za zderzenie z wodą.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            RocketScript rocket = collision.gameObject.GetComponent<RocketScript>();
            rocket.SetState(new LanderWaterboom(rocket));
        }
    }
}
