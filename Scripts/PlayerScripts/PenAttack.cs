using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PenAttack : NetworkBehaviour {
  
    public float meleerate = 0.5F;
    public float smooth = 300;
    public GameObject weaponOnPlayer;
    public GameObject playerController;
    public Transform vectorWeaponStart;
    public bool weaponOn;

    private float nextmelee = 0;
    private bool swing = false;
    private Vector3 vectorPlayer;
    private float timer = 0.5f;
    
    void start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (weaponOn)
        {
            vectorPlayer = playerController.transform.position;
            // weaponOnPlayer.transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));

            if (Input.GetMouseButtonDown(0) && Time.time > nextmelee)
            {

                nextmelee = Time.time + meleerate;
                swing = true;
            }

            if (swing == true && timer > 0)
            {
                //turn on trigger
                GameObject.FindGameObjectWithTag(GameTags.create(GameTags.dissect(gameObject.tag)[0], GameTags.Type.WEAPON, GameTags.WeaponClass.MELEE)).GetComponent<Collider>().enabled = true;
                weaponOnPlayer.transform.RotateAround(vectorPlayer, Vector3.down, Time.deltaTime * smooth);
                timer -= Time.deltaTime;
            }

            else
            {
                swing = false;
                weaponOnPlayer.transform.position = vectorWeaponStart.transform.position;
                weaponOnPlayer.transform.rotation = gameObject.transform.rotation;
                weaponOnPlayer.transform.Rotate(90, 15, 0);
                // disable collider
                GameObject.FindGameObjectWithTag(GameTags.create(GameTags.dissect(gameObject.tag)[0], GameTags.Type.WEAPON, GameTags.WeaponClass.MELEE)).GetComponent<Collider>().enabled = false;
                timer = 0.5f;
            }
        }
    }
}
