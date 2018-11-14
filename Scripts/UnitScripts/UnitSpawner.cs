using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UnitSpawner : NetworkBehaviour {

    private const int MAX_UNITS = 5;

    private GameObject[] spawnedUnits = new GameObject[MAX_UNITS];
    [SerializeField]
    private GameObject unitPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public void CmdspawnUnit() {
        string newTag = GameTags.create(GameTags.dissect(gameObject.tag)[0], GameTags.Type.PLAYER);
        GameObject player = GameObject.FindGameObjectWithTag(newTag);
        for (int i = 0; i < MAX_UNITS; ++i) {
            if (spawnedUnits[i] == null) {
                spawnedUnits[i] = Instantiate(unitPrefab, gameObject.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
                // NetworkServer.SpawnWithClientAuthority(spawnedUnits[i],player);
                NetworkServer.Spawn(spawnedUnits[i]);
                break;
            }
        }
        
    }

}

