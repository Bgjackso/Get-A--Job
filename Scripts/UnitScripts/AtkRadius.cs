using UnityEngine;
using System.Collections;

public class AtkRadius : MonoBehaviour {

    //varibles
    Renderer objectRenderer;
    SphereCollider objectColliderSize;

    public float moveSpeed; // this should be in the pathing
    public bool attackMode; // if the unit is pathing or attacking

    public float attackLength; // this is mainly for if the unit is ranged
    public float colliderRadius;

	// Use this for initialization
	void Awake () {
        objectRenderer = GetComponent<Renderer>();
        objectColliderSize = transform.GetComponent<SphereCollider>();

        moveSpeed = 1.0f;
        attackMode = false;
        attackLength = 1.0f;
        colliderRadius = objectColliderSize.radius;
	}
	
	// Update is called once per frame
	void Update () {
        objectColliderSize.radius = colliderRadius;

	
	}

    // i am using a sphere colider for the radius
    void OnTriggerStay( Collider other)
    {

        if(other.tag == "Enemy") // the tags needs to be change according to the player. Dont forget to create a tag in the inspector
        {
            objectRenderer.material.color = Color.red;
            attackMode = true;

        }

    }

    //basically if the other ogject dies or happens to leave the radius.
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy") // again need to change tags to fit our needs
        {
            attackMode = false;
            objectRenderer.material.color = Color.blue;
        }
    }
}
