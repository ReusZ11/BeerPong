using System;
using UnityEngine;

public class EventsContainer
{
    public static Action<DragCommand> DragCommanded;
    public static void InvokeDragCommanded(DragCommand dragCommand)
    {
        DragCommanded?.Invoke(dragCommand);
    }

    public static Action ShouldSpawn;
    public static void InvokeShouldSpawn()
    {
        ShouldSpawn?.Invoke();
    }

    public static Action<GameObject, int> GhostCupSpawned;
    public static void InvokeGhostCupSpawned(GameObject ghostCupTransform, int index)
    {
        GhostCupSpawned?.Invoke(ghostCupTransform, index);
    }

    public static Action<Vector3> GhostPongCollided;
    public static void InvokeGhostPongCollided(Vector3 collisionPos)
    {
        GhostPongCollided?.Invoke(collisionPos);
    }

    public static Action LivesLost;
    public static void InvokeLivesLost()
    {
        LivesLost?.Invoke();
    }

    public static Action ShouldLoseLife;
    public static void InvokeShouldLoseLife()
    {
        LivesLost?.Invoke();
    }

    public static Action<int> PongLandedToTheCup;
    public static void InvokePongLandedToTheCup(int damage)
    {
        PongLandedToTheCup?.Invoke(damage);
    }
}