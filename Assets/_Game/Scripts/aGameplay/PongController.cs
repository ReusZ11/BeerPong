using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnTransform;

    [SerializeField]
    private GameObject pongPrefab;
    [SerializeField]
    private Projection projection;
    [SerializeField]
    private float inputMultiplier;

    private Pong currentPong;

    private void Awake()
    {
        EventsContainer.ShouldSpawn += OnShouldSpawn;
        EventsContainer.DragCommanded += OnDragCommanded;
    }

    private void OnDisable()
    {
        EventsContainer.ShouldSpawn -= OnShouldSpawn;
        EventsContainer.DragCommanded -= OnDragCommanded;
    }

    private void Start()
    {
        SpawnNewPong();
    }

    private void OnDragCommanded(DragCommand dragCommand)
    {
        float angle = Vector2.SignedAngle(dragCommand.Direction, Vector2.up);
        Quaternion dragRotation = Quaternion.Euler(0, angle, 0);
        Vector3 shootDirection = dragRotation * SpawnTransform.forward;
        Debug.DrawRay(currentPong.transform.position, shootDirection * 10, Color.red, 0.1f, false);

        float forceAmount = dragCommand.Amount * inputMultiplier;

        if (!dragCommand.IsCompleted)
        { 
            projection.SimulateTrajectory(currentPong, shootDirection * forceAmount);
        }
        else 
        {
            currentPong.Shoot(shootDirection * forceAmount, false);
            
        }
    }

    private void OnShouldSpawn()
    {
        SpawnNewPong();
    }

    private void SpawnNewPong()
    { 
        GameObject gb = Instantiate(pongPrefab, SpawnTransform.position, SpawnTransform.rotation);
        currentPong = gb.GetComponent<Pong>();
    }
}
