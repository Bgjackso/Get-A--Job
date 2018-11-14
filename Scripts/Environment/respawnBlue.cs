using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class respawnBlue : NetworkBehaviour {

    public GameObject myPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("BLUE_PLAYER"))
        {
            GameObject go = Instantiate(myPrefab);
        }
    }
}
