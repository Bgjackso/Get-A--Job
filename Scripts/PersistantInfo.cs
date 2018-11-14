using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PersistantInfo : NetworkBehaviour
{

    public GameObject redPlayer, bluePlayer;
    public GameObject redHQ, blueHQ;

    public bool redWin, blueWin;

    public bool playersConnected;

    private bool setBuildings = false;
    // Use this for initialization
    void Awake()
    {
        playersConnected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playersConnected)
        {
            redPlayer = GameObject.FindWithTag("RED_PLAYER");
            bluePlayer = GameObject.FindWithTag("BLUE_PLAYER");
            playersConnected = redPlayer != null && bluePlayer != null;

            //Debug.Log("trying to find all players");
        }
        if(playersConnected)
        {
            if (!setBuildings)
            {
                Debug.Log("Updating buildings");
                foreach (NetworkConnection conn in NetworkServer.connections)
                {
                    Debug.LogFormat("current connection: {0}", conn);
                    if (conn.hostId != -1)
                    {
                        Debug.Log("Not the host :3");
                        foreach (GameObject building in GameObject.FindGameObjectsWithTag(GameTags.create(GameTags.Team.BLUE, GameTags.Type.PLACEHOLDER, GameTags.Type.BUILDING)))
                        {
                            Debug.LogFormat("setting authority for building {0}", building);
                            Debug.LogFormat("current building authority: {0}", building.GetComponent<NetworkIdentity>().clientAuthorityOwner);
                            building.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
                            Debug.LogFormat("new building authority: {0}", building.GetComponent<NetworkIdentity>().clientAuthorityOwner);
                        }
                    }
                }
                setBuildings = true;
            }
            //checking if the hq and building of one player is destroyed
            if (redPlayer == null)
            {
                //making the local host winner
                blueWin = true;
                redWin = false;
                changeScene();

            }
            else if (bluePlayer == null)
            {
                //makeing the other player winner

                blueWin = false;
                redWin = true;
                changeScene();

            }
        }

       // redHQ = GameObject.FindWithTag("RED_BUILDING_HQ");
       // blueHQ = GameObject.FindWithTag("BLUE_BUILDING_HQ");

      

    }
    void changeScene()
    {
        Debug.Log("changed the scene");
        /////////////////////////////////////////////////////////

        //loading in the scene that will display if the player won or lost
        if (redWin && isLocalPlayer )
        {
             SceneManager.LoadScene("Winner");
        }
        else if (!redWin && isLocalPlayer )
        {
            SceneManager.LoadScene("Loser");
        }

        if (blueWin && isLocalPlayer )
        {
            SceneManager.LoadScene("Winner");
        }
        else if (!blueWin && isLocalPlayer )
        {
             SceneManager.LoadScene("Loser");
        }
        /////////////////////////////////////////////////////////////////////

    }
}
