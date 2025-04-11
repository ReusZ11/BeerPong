// using System.Collections;
// using UnityEngine;
// using Cinemachine;

// namespace GeneralTemplate
// {
//     public enum CameraType
//     {
//         PickMode,
//         WolfMode
//     }

//     /// <summary>
//     /// Should be parent of all CinemachineVirtualCameras;
//     /// The index of virtual camera is equal to the sibling index,
//     /// and equal to the camera location
//     /// 
//     /// wolf in sheep
//     /// </summary>
//     public class CameraController : MonoBehaviour
//     {
//         [SerializeField]
//         private Camera renderingCamera;

//         [Header("SearcherModeCameras")]
//         [Space]
//         [SerializeField]
//         private CinemachineVirtualCamera northCamera;

//         [SerializeField]
//         private CinemachineVirtualCamera eastCamera;

//         [SerializeField]
//         private CinemachineVirtualCamera southCamera;

//         [SerializeField]
//         private CinemachineVirtualCamera westCamera;

//         [SerializeField]
//         private float blendDuration;

//         [SerializeField]
//         private float angleOfViewInit;

//         [SerializeField]
//         private float heightInit = 5;

//         [SerializeField]
//         private float forwardOffsetInit = 3.2f;

//         [Header("WolfCamera")]
//         [SerializeField]
//         private CinemachineVirtualCamera wolfCamera;

//         private CinemachineVirtualCamera currentVcam;

//         private void Awake()
//         {
//             northCamera.transform.eulerAngles = new Vector3(angleOfViewInit, 180, 0);
//             eastCamera.transform.eulerAngles = new Vector3(angleOfViewInit, -90, 0);
//             southCamera.transform.eulerAngles = new Vector3(angleOfViewInit, 0, 0);
//             westCamera.transform.eulerAngles = new Vector3(angleOfViewInit, 90, 0);

//             northCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
//                 = new Vector3(0, heightInit, forwardOffsetInit);

//             eastCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
//                 = new Vector3(forwardOffsetInit, heightInit, 0);

//             southCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
//                 = new Vector3(0, heightInit, -forwardOffsetInit); 

//             westCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset
//                 = new Vector3(-forwardOffsetInit, heightInit, 0);           


//             northCamera.Priority = 0;
//             eastCamera.Priority = 0;
//             southCamera.Priority = 0;
//             westCamera.Priority = 0;

//             currentVcam = southCamera;
//             currentVcam.Priority = 1;

//             EventsContainer.KillerSpawned += OnKillerSpawned;
//             EventsContainer.SearcherBotActivated += OnSearcherBotActivated;

//             EventsContainer.WolfToPickDark += ActivatePickModeCameraView;
//             EventsContainer.PickToWolfDark += ActivateWolfModeCameraView;

//             EventsContainer.SearcherCrossedWorldSide += OnPlayerCrossedWorldSide;

//             QueriesContainer.CurrentCameraYaw += GetCameraYaw;
//         }

//         private void OnDisable()
//         { 
//             EventsContainer.KillerSpawned -= OnKillerSpawned;
//             EventsContainer.SearcherBotActivated -= OnSearcherBotActivated;

//             EventsContainer.WolfToPickDark -= ActivatePickModeCameraView;
//             EventsContainer.PickToWolfDark -= ActivateWolfModeCameraView;

//             EventsContainer.SearcherCrossedWorldSide -= OnPlayerCrossedWorldSide;

//             QueriesContainer.CurrentCameraYaw -= GetCameraYaw;
//         }

//         private void OnKillerSpawned(Killer killer)
//         {
//             (Transform lockPoint, Transform lookPoint) = killer.GetCameraPoints();
//             wolfCamera.Follow = lockPoint;
//             wolfCamera.LookAt = lookPoint;
//         }

//         private void OnSearcherBotActivated(SearcherBot bot)
//         {
//             Debug.Log("CameraController: OnSearcherBotActivated " + bot.name);
//             northCamera.Follow = bot.transform;
//             eastCamera.Follow  = bot.transform;
//             southCamera.Follow = bot.transform;
//             westCamera.Follow  = bot.transform;
//         }

//         private void ActivatePickModeCameraView()
//         {
//             print("CameraController: Activating Searcher view");
//             currentVcam.Priority = 0;
//             WorldSide worldSide = QueriesContainer.QueryCurrentWorldSide();
//             currentVcam = ConvertToVCam(worldSide);
//             currentVcam.Priority = 1;
//         }

//         private void ActivateWolfModeCameraView()
//         {
//             print("CameraController: Activating wolf view");
//             currentVcam.Priority = 0;
//             currentVcam = wolfCamera;
//             currentVcam.Priority = 1;
//         }

//         // Rotated Camera and on completion invokes CameraAdjustedToNewWorldSide
//         private void OnPlayerCrossedWorldSide(WorldSide newWorldSide)
//         {
//             print("Crossed world side");
//             currentVcam.Priority = 0;
//             var newVcam = ConvertToVCam(newWorldSide);
//             if (currentVcam != newVcam)
//             {
//                 currentVcam = newVcam;
//                 StartCoroutine(WaitUntilBlendIsFinished());
//             }
//             currentVcam.Priority = 1;
//         }

//         private CinemachineVirtualCamera ConvertToVCam(WorldSide worldSide)
//         {
//             switch (worldSide)
//             { 
//                 case WorldSide.North:
//                     return northCamera;
//                 case WorldSide.East:
//                     return eastCamera;
//                 case WorldSide.South:
//                     return southCamera;
//                 case WorldSide.West:
//                     return westCamera;
//                 default:
//                     Debug.LogError("No such world side registerd by the coder at the time of writing." + 
//                     " Was the world changed?!");
//                     return null;
//             }
//         }

//         private IEnumerator WaitUntilBlendIsFinished()
//         {
//             yield return new WaitForSeconds(blendDuration);
//             EventsContainer.InvokeCameraAdjustedToNewWorldSide();
//         }

//         private float GetCameraYaw()
//         {
//             return renderingCamera.transform.eulerAngles.y;
//         }
//     }
// }
