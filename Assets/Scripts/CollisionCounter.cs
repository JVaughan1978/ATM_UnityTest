using UnityEngine;
using System.Collections;

public class CollisionCounter : MonoBehaviour 
{
    public int collisionCount = 0;

    void OnCollisionEnter (Collision collision) 
    {
        if (collision.gameObject.name != "Collider") {
            ObjectCreator.totalCollisions++;
            collisionCount++;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {	
	}
}
