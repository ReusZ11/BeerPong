using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(PongAlco))]
public class Pong : MonoBehaviour
{
    [SerializeField]
    private float inputMultiplier;

    private PongAlco alcoData;
    private float forceAmount = 1;
    private Rigidbody rb;
    

    private bool isGhost;

    private void Awake()
    {
        alcoData = GetComponent<PongAlco>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

    }

    public void Shoot(Vector3 velocity, bool isGhostArg)
    {
        rb.isKinematic = false;
        isGhost = isGhostArg;
        rb.AddForce(velocity, ForceMode.Impulse);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGhost)
        {
            return;
        }

        // Should be invoked only once.
        EventsContainer.InvokeGhostPongCollided(collision.GetContact(0).point);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGhost)
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Cup"))
        {
            Cup cup = other.GetComponent<Cup>();
            AlcoType alco = cup.ProcessPongEnterAndGetType();
            int damage = alcoData.GetDamage(alco);
            EventsContainer.InvokePongLandedToTheCup(damage);
            Destroy(this.gameObject);
        }
    }
}
