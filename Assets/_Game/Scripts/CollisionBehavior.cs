using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collision")
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

   

    


    /*
    void OnTrigerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collision") {
            collision.rigidbody.velocity = Vector3.zero;
        }
    }*/
}
