using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    private GameObject[] healthBars;
    private GameObject[] resources;
    private GameObject[] hqHP;
    private GameObject melee;
    private GameObject ranged;
    private GameObject explosive;
	// Use this for initialization
	void Start () {

        if (gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("RED_PLAYER") == null){
                gameObject.tag = "RED_PLAYER";
                healthBars = GameObject.FindGameObjectsWithTag("CurrentHealth");
                foreach (GameObject health in healthBars)
                {
                    health.tag = "RED_PLAYER_HEALTH";
                }
                resources = GameObject.FindGameObjectsWithTag("Resource");
                foreach (GameObject resource in resources)
                {
                    resource.tag = "RED_PLAYER_RESOURCE";
                }
                hqHP = GameObject.FindGameObjectsWithTag("HqHp");
                foreach (GameObject hq in hqHP)
                {
                    hq.tag = "RED_PLAYER_HQ_HEALTH";
                }
                melee = GameObject.FindGameObjectWithTag("WEAPON_MELEE") as GameObject;
                melee.tag = "RED_WEAPON_MELEE";
                ranged = GameObject.FindGameObjectWithTag("WEAPON_RANGED") as GameObject;
                ranged.tag = "RED_WEAPON_RANGED";
                explosive = GameObject.FindGameObjectWithTag("WEAPON_EXPLOSIVE") as GameObject;
                explosive.tag = "RED_WEAPON_EXPLOSIVE";
                Debug.Log("Red Player Set");
            }
            else if(GameObject.FindGameObjectWithTag("RED_PLAYER") != null){
                gameObject.tag = "BLUE_PLAYER";
                healthBars = GameObject.FindGameObjectsWithTag("CurrentHealth");
                foreach (GameObject health in healthBars)
                {
                    health.tag = "BLUE_PLAYER_HEALTH";
                }
                resources = GameObject.FindGameObjectsWithTag("Resource");
                foreach (GameObject resource in resources)
                {
                    resource.tag = "BLUE_PLAYER_RESOURCE";
                }
                hqHP = GameObject.FindGameObjectsWithTag("HqHp");
                foreach (GameObject hq in hqHP)
                {
                    hq.tag = "BLUE_PLAYER_HQ_HEALTH";
                }
                melee = GameObject.FindGameObjectWithTag("WEAPON_MELEE") as GameObject;
                melee.tag = "BLUE_WEAPON_MELEE";
                ranged = GameObject.FindGameObjectWithTag("WEAPON_RANGED") as GameObject;
                ranged.tag = "BLUE_WEAPON_RANGED";
                explosive = GameObject.FindGameObjectWithTag("WEAPON_EXPLOSIVE") as GameObject;
                explosive.tag = "BLUE_WEAPON_EXPLOSIVE";
                Debug.Log("Blue Player Set");
            }
        }

	    if (isLocalPlayer)
        {
            GetComponent<PlayerStats>().enabled = true;
            GetComponent<WeaponSwitch>().enabled = true;
            GetComponent<TerrainControl>().enabled = true;
            GetComponentInChildren<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            GetComponentInChildren<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<Camera>().enabled = true;
            GetComponent<camSwitch>().enabled = true;
            // GetComponent<tdCamMove>().enabled = true;
            GetComponent<TerrainControl>().enabled = true;
            gameObject.transform.GetChild(2).GetComponent<Camera>().enabled = true;
            gameObject.transform.GetChild(2).GetComponent<AudioListener>().enabled = true;

            GameObject[] gos = null;
            if (gameObject.tag == "RED_PLAYER") {
                gos = GameObject.FindGameObjectsWithTag("RED_PLACEHOLDER_BUILDING");
            }
            else if (gameObject.tag == "BLUE_PLAYER")
            {
                gos = GameObject.FindGameObjectsWithTag("BLUE_PLACEHOLDER_BUILDING");
            }
            foreach (GameObject obj in gos)
            {
                obj.GetComponent<BuildingPlot>().enabled = true;
            }
            //Camera.main.enabled = false;
        }
        else
        {
            //Camera.main.enabled = true;
        }
	}
	
}
