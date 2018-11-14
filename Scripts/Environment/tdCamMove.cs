using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class tdCamMove : NetworkBehaviour {

    private float speed = 2.5f;
    public float normal = 7f;
    public float fasterer = 14f;
    public float zmin = 15;
    public float zmax = 90;
    public float zspeed = 10;
    private Camera tdCamScript;
    private GameObject tdCam;
    private PlayerStats playerStats;

    private void Start() {
        playerStats = gameObject.GetComponent<PlayerStats>();
        tdCam = gameObject.transform.Find("PlayerTDCamera").gameObject;
        tdCamScript = gameObject.transform.Find("PlayerTDCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        if (playerStats.inTerritoryMode)
        {
            // Camera Speed
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                speed = fasterer;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                speed = normal;
            }

            //Camera Movement
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                tdCam.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                tdCam.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                tdCam.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                tdCam.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            //Camera Zoom
            float fov = tdCamScript.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * zspeed;
            fov = Mathf.Clamp(fov, zmin, zmax);
            tdCamScript.fieldOfView = fov;
            tdCam.transform.rotation = Quaternion.identity;
            tdCam.transform.Rotate(90, 0, 0);
            if (GameTags.dissect(gameObject.tag)[0] == GameTags.Team.RED) {
                tdCam.transform.Rotate(0, 0, 180);
            }
        }
    }
}
