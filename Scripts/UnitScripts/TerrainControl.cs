using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// attach to the PlayerGod object
public class TerrainControl : NetworkBehaviour {

    private enum MouseState {
        /* click to spawn units*/ EMPTY,
        /* click to set waypoints */ ATTACK_WAYPOINT, DEFENSE_WAYPOINT, SUPPORT_WAYPOINT,
        SCOUT__WAYPOINT,
        /* click to spawn buildings */ SPAWN_ATTACK, SPAWN_DEFENSE, SPAWN_SUPPORT };

    private MouseState mouseState = MouseState.EMPTY;

    private string team;
    private bool isOnTeam = false;

    private Camera fpsCam;
    private Camera tdCam;
    private PlayerStats playerStats;

    [SerializeField]
    private GameObject redAttackBuildingPrefab;
    [SerializeField]
    private GameObject redDefenseBuildingPrefab;
    [SerializeField]
    private GameObject redSupportBuildingPrefab;

    [SerializeField]
    private GameObject blueAttackBuildingPrefab;
    [SerializeField]
    private GameObject blueDefenseBuildingPrefab;
    [SerializeField]
    private GameObject blueSupportBuildingPrefab;

    // Use this for initialization
    void Start () {
        playerStats = gameObject.GetComponent<PlayerStats>();
        tdCam = gameObject.transform.Find("PlayerTDCamera").GetComponent<Camera>();
        fpsCam = gameObject.transform.Find("FPSController").Find("FirstPersonCharacter").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnTeam) {
            isOnTeam = GameTags.isOnTeam(gameObject.tag);
            if (isOnTeam) {
                team = GameTags.dissect(gameObject.tag)[0];
            }
        } else {
            if (playerStats.inTerritoryMode) {
                if (Input.GetKeyDown(KeyCode.Alpha0)) {
                    mouseState = MouseState.EMPTY;
                    Debug.Log("set mouse to empty");
                } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    mouseState = MouseState.ATTACK_WAYPOINT;
                    Debug.Log("set mouse to attack waypoint");
                } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    mouseState = MouseState.DEFENSE_WAYPOINT;
                    Debug.Log("set mouse to defense waypoint");
                } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    mouseState = MouseState.SUPPORT_WAYPOINT;
                    Debug.Log("set mouse to support waypoint");
                } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    mouseState = MouseState.SCOUT__WAYPOINT;
                    Debug.Log("set mouse to scout waypoint");
                } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                    mouseState = MouseState.SPAWN_ATTACK;
                    Debug.Log("set mouse to attack building");
                } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
                    mouseState = MouseState.SPAWN_DEFENSE;
                    Debug.Log("set mouse to defense building");
                } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
                    mouseState = MouseState.SPAWN_SUPPORT;
                    Debug.Log("set mouse to support building");
                }

                //if (Input.GetMouseButtonDown(0)) {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    Debug.Log("clicked mouse button");
                    bool aggressive = Input.GetKey(KeyCode.LeftShift);
                    if (aggressive) {
                        Debug.Log("set aggressive");
                    }
                    switch (mouseState) {
                        case MouseState.EMPTY: {
                                // spawn units when clicking on a building
                                // if 0 == team
                                // if 1 == building
                                // if 2 == spawn or HQ
                                // call spawn
                                GameObject clickedObject;
                                if (didClickObject(out clickedObject)) {
                                    Debug.LogFormat("clicked on obj {0} with tag {1}", clickedObject.name,
                                        clickedObject.tag);
                                    string[] tagSections = GameTags.dissect(clickedObject.tag);
                                    if (GameTags.isOnTeam(clickedObject.tag) && tagSections[0] == team && tagSections[1] == GameTags.Type.BUILDING
                                        && (tagSections[2] == GameTags.BuildingClass.SPAWN || tagSections[2] == GameTags.BuildingClass.HQ)) {
                                        // TODO: check for resources here
                                        Debug.Log("Spawning units");
                                        clickedObject.GetComponent<UnitSpawner>().CmdspawnUnit();
                                    }
                                }
                                break;
                            }
                        case MouseState.ATTACK_WAYPOINT: {
                                // set waypoint for attack units
                                CmdmoveWaypoint(GameTags.UnitClass.ATTACK, aggressive);
                                Debug.Log("Set attack waypoint");
                                break;
                            }
                        case MouseState.DEFENSE_WAYPOINT: {
                                // set waypoint for defense units
                                CmdmoveWaypoint(GameTags.UnitClass.DEFENSE, aggressive);
                                Debug.Log("Set defense waypoint");
                                break;
                            }
                        case MouseState.SUPPORT_WAYPOINT: {
                                // set waypoint for support units
                                CmdmoveWaypoint(GameTags.UnitClass.SUPPORT, aggressive);
                                Debug.Log("Set support waypoint");
                                break;
                            }
                        case MouseState.SCOUT__WAYPOINT: {
                                // set waypoint for scout units
                                CmdmoveWaypoint(GameTags.UnitClass.SCOUT, aggressive);
                                Debug.Log("Set scout waypoint");
                                break;
                            }
                        case MouseState.SPAWN_ATTACK: {
                                // spawn an attack building
                                if (team == GameTags.Team.RED) {
                                    CmdspawnBuilding(redAttackBuildingPrefab);
                                    Debug.Log("Spawn red attack building");
                                } else {
                                    CmdspawnBuilding(blueAttackBuildingPrefab);
                                    Debug.Log("Spawn blue attack building");
                                }
                                break;
                            }
                        case MouseState.SPAWN_DEFENSE: {
                                // spawn an attack building
                                if (team == GameTags.Team.RED) {
                                    CmdspawnBuilding(redDefenseBuildingPrefab);
                                    Debug.Log("Spawn red defense building");
                                } else {
                                    CmdspawnBuilding(blueDefenseBuildingPrefab);
                                    Debug.Log("Spawn blue defense building");
                                }
                                break;
                            }
                        case MouseState.SPAWN_SUPPORT: {
                                // spawn an attack building
                                if (team == GameTags.Team.RED) {
                                    CmdspawnBuilding(redSupportBuildingPrefab);
                                    Debug.Log("Spawn red support building");
                                } else {
                                    CmdspawnBuilding(blueSupportBuildingPrefab);
                                    Debug.Log("Spawn blue support building");
                                }
                                break;
                            }
                    }
                }
            } else {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    switch (mouseState) {
                        case MouseState.ATTACK_WAYPOINT: {
                                // set waypoint for attack units
                                CmdmoveWaypoint(GameTags.UnitClass.ATTACK, true);
                                break;
                            }
                        case MouseState.DEFENSE_WAYPOINT: {
                                // set waypoint for defense units
                                CmdmoveWaypoint(GameTags.UnitClass.DEFENSE, true);
                                break;
                            }
                        case MouseState.SUPPORT_WAYPOINT: {
                                // set waypoint for support units
                                CmdmoveWaypoint(GameTags.UnitClass.SUPPORT, true);
                                break;
                            }
                        case MouseState.SCOUT__WAYPOINT: {
                                // set waypoint for scout units
                                CmdmoveWaypoint(GameTags.UnitClass.SCOUT, true);
                                break;
                            }
                    }
                }
            }
        }
	}
   
    private void CmdmoveWaypoint(string unitClass, bool aggressive) {
        Debug.LogFormat("Moving waypoint {0} {1}", team, unitClass);
        Vector3 newPos;
        if (didClick(out newPos)) {
            string waypointTag = GameTags.create(team, GameTags.Type.WAYPOINT, unitClass);
            GameObject waypoint = GameObject.FindGameObjectWithTag(waypointTag);
            waypoint.transform.position = newPos;
        }
        // TODO: set aggressive or not
    }

    private void CmdspawnBuilding(GameObject building) {
        GameObject clickedObject;
        if (didClickObject(out clickedObject)) {
            string[] tagSections = GameTags.dissect(clickedObject.tag);
            Debug.LogFormat("team: {0} tags: {1}", team, tagSections);
            if (GameTags.isOnTeam(clickedObject.tag) && tagSections[0] == team && tagSections[1] == GameTags.Type.PLACEHOLDER) {
                clickedObject.GetComponent<BuildingPlot>().CmdspawnBuilding(building);
                Debug.Log("spawning building");
            }
        }
    }

    
    private bool didClickObject(out GameObject clickedObject) {
        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay;
        if (playerStats.inTerritoryMode) {
            mouseRay = tdCam.ScreenPointToRay(mousePos);
        } else {
            mouseRay = fpsCam.ScreenPointToRay(mousePos);
        }
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit)) {
            clickedObject = hit.collider.gameObject;
            Debug.Log("clicked object");
            return true;
        }

        clickedObject = null;
        return false;
    }

    private bool didClick(out Vector3 clickedPos) {
        clickedPos = new Vector3();
        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay;
        if (playerStats.inTerritoryMode) {
            mouseRay = tdCam.ScreenPointToRay(mousePos);
        } else {
            mouseRay = fpsCam.ScreenPointToRay(mousePos);
        }
        RaycastHit hit;
        bool didHit = Physics.Raycast(mouseRay, out hit);
        if (didHit) {
            clickedPos = hit.point;
        }

        return didHit;
    }
}
