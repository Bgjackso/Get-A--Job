using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;


public class PlayerStats : NetworkBehaviour {
    
    public static PlayerStats playerstats;

    public float max_Health = 100.0f;
    public bool destroyOnDeath;

    [SyncVar(hook = "CmdSetHealthBar")]
    public float health;
    [SyncVar]
    public float calc_Health ;
    public int ammo = 100;
    public int resources = 0;
    public bool inTerritoryMode { get; set; }

    private GameObject[] resourceText;
    private GameObject[] healthBar;
    private NetworkStartPosition[] spawnPoints;

    private void Awake()
    {
        health = max_Health;
    }

    void Start()
    {
        
        calc_Health = 1;
        inTerritoryMode = false;
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    void Update()
    {
        if (gameObject.tag == "RED_PLAYER" )
        {
            healthBar = GameObject.FindGameObjectsWithTag("RED_PLAYER_HEALTH");
            resourceText = GameObject.FindGameObjectsWithTag("RED_PLAYER_RESOURCE");
        }
        else if (gameObject.tag == "BLUE_PLAYER")
        {
            healthBar = GameObject.FindGameObjectsWithTag("BLUE_PLAYER_HEALTH");
            resourceText = GameObject.FindGameObjectsWithTag("BLUE_PLAYER_RESOURCE");
        }
        SetResourceText();
        CmdSetHealthBar(calc_Health);
    }

    [Command]
    public void CmdRemoveHealth(float amount)
    {
        if (!isServer)
        {
            return;
        }

            health -= amount;
            if (health <= 0)
            {
                if (destroyOnDeath)
                {
                    Destroy(gameObject);
                }
                else
                {
                    health = max_Health;
                    RpcRespawn();
                    Debug.Log("Dead!");
                }
            }
         calc_Health = health / max_Health;
        Debug.Log("Health Lost");
    }
    public void RemoveResources (int amount)
    {
        resources -= amount;
    }

    [Command]
    public void AddHealth(float amount)
    {
        if (!isServer)
        {
            return;
        }

        if (health < 100)
        {
            health += amount;
            if (health > 100)
            {
                health = 100;
            }
            calc_Health = health / max_Health;
            Debug.Log("Health Gained");
            //CmdSetHealthBar(calc_Health);
        }
    }
    public void AddResources(int amount)
    {
        if (resources < 1000)
        {
            resources += amount;
            if (resources > 1000)
            {
                resources = 1000;
            }
        }
    }


    public float getHealth()
    {
        return health;
    }
    public int getResources()
    {
        return resources;
    }


    
    public void CmdSetHealthBar(float myHealth)
    {
        foreach (GameObject health in healthBar)
        {
            health.transform.localScale = new Vector3(myHealth, health.transform.localScale.y, health.transform.localScale.z);
        }
    }
    public void SetResourceText()
    {
        foreach (GameObject textbox in resourceText)
        {
            textbox.GetComponent<Text>().text = "Resources: " + resources.ToString();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }


void OnCollisionEnter(Collision _collision)
    { 
        if (_collision.gameObject.tag == "HealthPack")
        {
            if (health < 100)
            {
                AddHealth(50);
                Destroy(_collision.gameObject);
                print("Thanks for the heals!");
            }
        }
        if (_collision.gameObject.tag == "Resource")
        {
            //_collision.gameObject.SetActive(false);
            AddResources(20);
            Destroy(_collision.gameObject);
        }
    }
}
