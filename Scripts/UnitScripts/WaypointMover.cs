using UnityEngine;
using System.Collections;

public class WaypointMover : MonoBehaviour {

    int delay;

	// Use this for initialization
	void Start () {
        delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (delay == 0) {
            gameObject.transform.position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
        ++delay;
        if (delay > 100) {
            delay = 0;
        }
    }
}
