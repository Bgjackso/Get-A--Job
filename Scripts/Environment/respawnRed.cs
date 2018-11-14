using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class respawnRed : NetworkBehaviour {

    public GameObject myPrefab;

	// Update is called once per frame
	void Update ()
    {
        if (!GameObject.FindGameObjectWithTag("RED_PLAYER"))
        {
            GameObject go = Instantiate(myPrefab);
        }
	}
}
