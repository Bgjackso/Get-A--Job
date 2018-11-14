using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponSwitch : NetworkBehaviour {

    private GameObject Melee;
    private GameObject Ranged;
    private GameObject Explosive;
    private bool foundTag = false;

    // Use this for initialization
	void Start () {
        Debug.Log("Bool " + foundTag);
        Debug.Log(gameObject.tag);

        while (!foundTag)
        {
            if (gameObject.tag == "RED_PLAYER" || gameObject.tag == "BLUE_PLAYER")
                foundTag = true;
        }
        Debug.Log(foundTag);
        Debug.Log("Here I AM!! " + gameObject.tag);

        if (gameObject.tag == "RED_PLAYER")
        {
            Melee = GameObject.FindGameObjectWithTag("RED_WEAPON_MELEE") as GameObject;
            Ranged = GameObject.FindGameObjectWithTag("RED_WEAPON_RANGED") as GameObject;
            Explosive = GameObject.FindGameObjectWithTag("RED_WEAPON_EXPLOSIVE") as GameObject;
            Debug.LogFormat("My weapon {0}", Melee.name);
        }
        else if (gameObject.tag == "BLUE_PLAYER")
        {
            Melee = GameObject.FindGameObjectWithTag("BLUE_WEAPON_MELEE") as GameObject;
            Ranged = GameObject.FindGameObjectWithTag("BLUE_WEAPON_RANGED") as GameObject;
            Explosive = GameObject.FindGameObjectWithTag("BLUE_WEAPON_EXPLOSIVE") as GameObject;
        }
        if (isLocalPlayer)
        {
            Melee.SetActive(true);
            gameObject.GetComponent<PenAttack>().weaponOn = true;
            Ranged.GetComponent<RaycastShootComplete>().weaponOn = false;
            Ranged.SetActive(false);
            Explosive.GetComponentInChildren<Launcher>().weaponOn = false;
            Explosive.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(isLocalPlayer);
            if (isLocalPlayer)
            {
                Melee.SetActive(true);
                gameObject.GetComponent<PenAttack>().weaponOn = true;
                Ranged.GetComponent<RaycastShootComplete>().weaponOn = false;
                Ranged.SetActive(false);
                Explosive.GetComponentInChildren<Launcher>().weaponOn = false;
                Explosive.SetActive(false);

            }
        }
       else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isLocalPlayer)
            {
                Melee.SetActive(false);
                gameObject.GetComponent<PenAttack>().weaponOn = false;
                Ranged.GetComponent<RaycastShootComplete>().weaponOn = true;
                Ranged.SetActive(true);
                Explosive.GetComponentInChildren<Launcher>().weaponOn = false;
                Explosive.SetActive(false);
            }
        }
       else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (isLocalPlayer)
            {
                Melee.SetActive(false);
                gameObject.GetComponent<PenAttack>().weaponOn = false;
                Ranged.GetComponent<RaycastShootComplete>().weaponOn = false;
                Ranged.SetActive(false);
                Explosive.GetComponentInChildren<Launcher>().weaponOn = true;
                Explosive.SetActive(true);
            }
        }


    }
}
