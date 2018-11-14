using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class UnitPathing : NetworkBehaviour {

    private AICharacterControl controller;
    private GameObject targetWaypoint;
    private GameObject targetObjet;
    private bool arrived = false;
    private bool isOnTeam = false;
    public bool aggresssive { get; set; }
    private string[] tagSections;

	// Use this for initialization
	void Start () {
        controller = gameObject.GetComponent<AICharacterControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnTeam) {
            isOnTeam = GameTags.isOnTeam(gameObject.tag);
            if (isOnTeam) {
                tagSections = GameTags.dissect(gameObject.tag);
                targetWaypoint = GameObject.FindGameObjectWithTag(GameTags.create(tagSections[0], GameTags.Type.WAYPOINT, tagSections[2]));
                Debug.LogFormat("Unit {0} has waypoint {1}", gameObject.tag, targetWaypoint.tag);
                if (targetWaypoint != null) {
                    controller.SetTarget(targetWaypoint.transform);
                }
            }
        } else {
            if (tagSections == null) {
                tagSections = GameTags.dissect(gameObject.tag);
            }
            if (arrived && targetObjet == null) {
                controller.SetTarget(gameObject.transform);
            } else if (targetObjet != null) {
                controller.SetTarget(targetObjet.transform);
            } else {
                controller.SetTarget(targetWaypoint.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider) {
        GameObject other = otherCollider.gameObject;
        string[] otherTagSections = GameTags.dissect(other.tag);
        if (GameTags.isOnTeam(other.tag) && otherTagSections[0] == tagSections[0]
            && otherTagSections[1] == GameTags.Type.WAYPOINT 
            && otherTagSections[2] == tagSections[2]) {
            arrived = true;
        } else {
            if (arrived || aggresssive) {
                if (GameTags.isOnTeam(other.tag) && otherTagSections[0] != tagSections[0] &&
                    (otherTagSections[1] == GameTags.Type.BUILDING
                        || otherTagSections[1] == GameTags.Type.UNIT
                        || otherTagSections[1] == GameTags.Type.PLAYER)) {
                    targetObjet = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider otherCollider) {
        GameObject other = otherCollider.gameObject;
        if (targetObjet != null && other.GetInstanceID() == targetObjet.GetInstanceID()) {
            targetObjet = null;
        } else if (other.GetInstanceID() == targetWaypoint.GetInstanceID()) {
            arrived = false;
        }
    }

}
