using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private AlcoType type;

    private GameObject ghostCup;

    private void Awake()
    {
        EventsContainer.GhostCupSpawned += OnGhostCupSpawned;
    }

    private void OnDisable()
    {
        EventsContainer.GhostCupSpawned -= OnGhostCupSpawned;
    }

    private void OnGhostCupSpawned(GameObject ghostCupArg, int ghostIndex)
    {
        if (ghostIndex == transform.GetSiblingIndex())
        { 
            ghostCup = ghostCupArg;
            print(ghostCupArg.name + " " + transform.name);
        }
    }

    public AlcoType ProcessPongEnterAndGetType()
    { 
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject, 1f);
        print(ghostCup.gameObject.name + " should be destroyed");
        Destroy(ghostCup.gameObject);
        return type;
    }

}
