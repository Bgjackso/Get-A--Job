using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class itemPickup : NetworkBehaviour {
    [SyncVar]
    public float health;

    private PlayerStats player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        /*if (gameObject.tag == "HealthPack") {
            Debug.Log("HP");
            if (collider.gameObject.tag == "RED_PLAYER")
            {
                player = collider.GetComponent<PlayerStats>();
                player.AddHealth(health);
                Debug.Log("Health Pickup");
                Destroy(gameObject);
            }
            if (collider.gameObject.tag == "BLUE_PLAYER") {
                player = collider.GetComponent<PlayerStats>();
                player.AddHealth(health);
                Destroy(gameObject);
            }
        }*/


        // if gameobject.tag == speedPickup

    }

}
