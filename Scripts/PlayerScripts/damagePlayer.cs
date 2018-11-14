using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class damagePlayer : NetworkBehaviour
{
    [SyncVar]
    public float damageToPlayer;
    [SyncVar]
    public float damageToEnemyAI;
    [SyncVar]
    public float damageToBuildings;
    
    private BuildingStats building;
    
    private PlayerStats player;


    private void OnTriggerEnter(Collider hit)
    {
       /* if (hit.gameObject.tag == "RED_PLAYER" && !isLocalPlayer)
        {
            player = hit.gameObject.GetComponent<PlayerStats>();
            player.CmdRemoveHealth(damageToPlayer);
            Debug.Log("Red Player Hit");
        
        }
        if (hit.gameObject.tag == "BLUE_PLAYER" && !isLocalPlayer)
        {
            player = hit.gameObject.GetComponent<PlayerStats>();
            player.CmdRemoveHealth(damageToPlayer);
            Debug.Log("Blue Player Hit");

        }*/
        if (hit.gameObject.tag == "RED_BUILDING_HQ")
        {
            
            building = hit.gameObject.GetComponent<BuildingStats>();
            building.RemoveHealth(damageToBuildings);
            Debug.Log("Take that building scum!");

        }
        if (hit.gameObject.tag == "BLUE_BUILDING_HQ")
        {

            building = hit.gameObject.GetComponent<BuildingStats>();
            building.RemoveHealth(damageToBuildings);
            Debug.Log("Take that building scum!");

        }
        if (hit.gameObject.tag == "EnemyAI")
        {
            //HEALTH = hit.GetComponent<EnemyAI>();
            //HEALTH.RemoveHealth(damageToPlayer);
            print("Enemy Ouch");

        }
    }
    void OnTriggerExit(Collider hit)
    {

    }
}
