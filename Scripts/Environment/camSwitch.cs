using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

// attach to PlayerGod
public class camSwitch : MonoBehaviour {

    private Camera fpsCam;
    private AudioListener fpsListener;
    private Camera tdCam;
    private AudioListener tdListener;
    private FirstPersonController player;
    private PlayerStats playerStats;

    public bool isfps;
   
	// Use this for initialization
	void Start () {
        tdCam = gameObject.transform.Find("PlayerTDCamera").GetComponent<Camera>();
        tdListener = gameObject.transform.Find("PlayerTDCamera").GetComponent<AudioListener>();
        fpsCam = gameObject.transform.Find("FPSController").Find("FirstPersonCharacter").GetComponent<Camera>();
        fpsListener = gameObject.transform.Find("FPSController").Find("FirstPersonCharacter").GetComponent<AudioListener>();

        player = gameObject.GetComponent<FirstPersonController>();
        playerStats = gameObject.GetComponent<PlayerStats>();

        fpsCam.enabled = true;
        fpsListener.enabled = true;
        tdCam.enabled = false;
        tdListener.enabled = false;
        player.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerStats.inTerritoryMode = !playerStats.inTerritoryMode;
            fpsCam.enabled = !playerStats.inTerritoryMode;
            fpsListener.enabled = !playerStats.inTerritoryMode;
            tdCam.enabled = playerStats.inTerritoryMode;
            tdListener.enabled = playerStats.inTerritoryMode;
            player.enabled = !playerStats.inTerritoryMode;

        }
    }
}
