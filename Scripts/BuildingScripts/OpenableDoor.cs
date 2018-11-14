using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OpenableDoor : NetworkBehaviour {

    public GameObject otherDoor;
    public AudioClip a;

    private bool havePlayers = false;

    public void Start()
    {
        StartCoroutine(door());
    }

    public void Update()
    {
        if (!havePlayers)
        {
            StartCoroutine(door());
        }
    }

    IEnumerator door()
    {
        if (GameObject.FindGameObjectWithTag("RED_PLAYER") && GameObject.FindGameObjectWithTag("BLUE_PLAYER"))
        {
            havePlayers = true;
            AudioSource audio = GetComponent<AudioSource>();
            yield return new WaitForSeconds(5);
            audio.PlayOneShot(a);

            transform.rotation = Quaternion.Euler(0, 90, 0);
            otherDoor.transform.rotation = Quaternion.Euler(0, -90, 0);

        }
    }
}
