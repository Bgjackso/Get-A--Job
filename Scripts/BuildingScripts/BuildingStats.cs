using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BuildingStats : NetworkBehaviour {
    public static BuildingStats enemystats;

    public const float max_Health = 100f;
    [SyncVar(hook = "SetHealthBar")]
    public float health = max_Health;
    public float calc_Health;
    
    private GameObject[] healthBar;

    // Use this for initialization
    void Start()
    {
        calc_Health = 1;
    }
    

    void Update()
    {
        if (gameObject.tag == "RED_BUILDING_HQ")
        {
            healthBar = GameObject.FindGameObjectsWithTag("RED_PLAYER_HQ_HEALTH");
            //Debug.Log("HQ Bar Set");
        }
        else if (gameObject.tag == "BLUE_BUILDING_HQ")
        {
            healthBar = GameObject.FindGameObjectsWithTag("BLUE_PLAYER_HQ_HEALTH");
            //Debug.Log("HQ Bar Set");
        }
        SetHealthBar(calc_Health);
    }

    
    public void RemoveHealth(float amount)
    {
        if (!isServer)
        {
            return;
        }

        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        calc_Health = health / max_Health;
        Debug.Log("HQ Bar lost");
    }
    public void SetHealthBar(float myHealth)
    {
        // Debug.LogFormat("healthBar: {0}, health: {1}", healthBar, myHealth);
        foreach (GameObject health in healthBar)
        {
            health.transform.localScale = new Vector3(myHealth, health.transform.localScale.y, health.transform.localScale.z);
            Debug.Log("HQ Bar change");
        }
    }
}
