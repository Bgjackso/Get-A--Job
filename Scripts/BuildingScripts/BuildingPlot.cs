using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BuildingPlot : NetworkBehaviour {
    [SyncVar]
    private bool active = false;
    private GameObject building = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (active) {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if (building == null) {
                active = false;
            }
        } else {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
	}

  
    public void CmdspawnBuilding(GameObject building) {
        if (!active) {
            string newTag = GameTags.create(GameTags.dissect(gameObject.tag)[0], GameTags.Type.PLAYER);
            GameObject player = GameObject.FindGameObjectWithTag(newTag);

            Quaternion buildingRotation = Quaternion.identity;
            this.building = (GameObject)Instantiate(building, gameObject.transform.position, buildingRotation);
            if (GameTags.dissect(gameObject.tag)[0] == GameTags.Team.RED)
            {
                this.building.transform.Rotate(0, 180, 0);
            }
            // NetworkServer.SpawnWithClientAuthority(this.building, player);
            NetworkServer.Spawn(this.building);
            active = true;
        }
    }
}
