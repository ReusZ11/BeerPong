using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour 
{
    [SerializeField] 
    private LineRenderer line;

    [SerializeField]
    private GameObject firstGhostCollisionMark;

    [SerializeField]
    private float minDistToSetLinePos;

    [SerializeField] 
    private float _maxPhysicsFrameIterations = 100;

    [SerializeField] 
    private Transform toSimulatesParent;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Awake()
    {
        EventsContainer.GhostPongCollided += OnGhostPongCollided;
    }

    private void OnDisable()
    {
        EventsContainer.GhostPongCollided -= OnGhostPongCollided;
    }

    private void Start()
    {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        for (int i = 0; i < toSimulatesParent.childCount; i++)
        {
            Transform obj = toSimulatesParent.GetChild(i);
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic)
            {
                _spawnedObjects.Add(obj, ghostObj.transform);
            }

            if (ghostObj.transform.childCount > 0)
            { 
                GameObject redundant = ghostObj.transform.GetChild(0).gameObject;
                Destroy(redundant);
            }

            // Destroy cup script from ghost cup so it will act purely as a physical object.
            Destroy(ghostObj.GetComponent<Cup>());

            // If special logic needed, it is recommended to create a separate class GhostCup.cs

            EventsContainer.InvokeGhostCupSpawned(ghostObj, i);
        }
    }

    // private void Update() {
    //     foreach (var item in _spawnedObjects) {
    //         item.Value.position = item.Key.position;
    //         item.Value.rotation = item.Key.rotation;
    //     }
    // }

    private bool isGhostPongCollided;
    private Vector3 firstGhostCollisionPos;

    private GameObject ghostCollisionMark;

    private void OnGhostPongCollided(Vector3 firstGhostCollisionPosArg)
    {
        isGhostPongCollided = true;
        firstGhostCollisionPos = firstGhostCollisionPosArg;
    }

    public void SimulateTrajectory(Pong pong, Vector3 velocity) 
    {
        var ghostPong = Instantiate(pong, pong.transform.position, Quaternion.identity);

        SceneManager.MoveGameObjectToScene(ghostPong.gameObject, _simulationScene);

        ghostPong.Shoot(velocity, true);

        line.positionCount = 0;
        isGhostPongCollided = false;

        Vector3 lastPosition = ghostPong.transform.position;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);

            if (isGhostPongCollided)
            {
                if (ghostCollisionMark == null)
                { 
                    ghostCollisionMark = Instantiate(firstGhostCollisionMark, firstGhostCollisionPos, Quaternion.identity);
                }
                else
                {
                    ghostCollisionMark.transform.position = firstGhostCollisionPos;
                }
                break;
            }

            Vector3 ghostPos = ghostPong.transform.position;
            float currentDist = (ghostPos - lastPosition).magnitude;
            if (currentDist >= minDistToSetLinePos)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, ghostPos);
            }
        }

        Destroy(ghostPong.gameObject);

    }
}