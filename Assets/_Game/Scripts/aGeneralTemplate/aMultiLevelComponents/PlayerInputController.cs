using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GeneralTemplate
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("InScreenCoordinates")]
        private float minDistance = 50;

        [SerializeField]
        private float middleDistance = 200;

        [SerializeField]
        private float distFromCamera = 1;

        [SerializeField]
        private Camera renderingCamera;

        private EventSystem eventSystem;
        private List<RaycastResult> raycastResults;

        float timer = 0;

        private void Awake()
        {
            // Maybe in some universer there will be multiple eventSystems
            // and this code will break;
            eventSystem = EventSystem.current;
            raycastResults = new List<RaycastResult>();
        }

        private void Update()
        {
            // Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            // if (joystickInput.x != 0 || joystickInput.y != 0)
            // {
            //     JoystickCommand joy = new JoystickCommand(joystickInput.x, joystickInput.y);
            //     EventsContainer.InvokeJoystickInput(joy);
            // }

#if UNITY_EDITOR
            EditorPlayerInput();
            return;
#endif

#if UNITY_ANDROID || UNITY_IOS
          //  BuildPlayerInput();
#endif
        }

        private Vector3 startPointerPos;
        private DragCommand predictedDragCommand;
        private bool isValidTouchStarted;

#if UNITY_EDITOR
        private void EditorPlayerInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    isValidTouchStarted = false;
                    return;
                }
                startPointerPos = Input.mousePosition;
                isValidTouchStarted = true;
            }

            if (!isValidTouchStarted)
            {
                return;
            }

            if (Input.GetMouseButton(0))
            { 
                Vector3 endPointerPos = Input.mousePosition;
                Vector3 direction = (startPointerPos - endPointerPos).normalized;
                float magnitude   = (startPointerPos - endPointerPos).magnitude;
                if (magnitude <= minDistance)
                {
                    return;
                }

                predictedDragCommand = new DragCommand
                    (direction, magnitude / middleDistance, false);
                EventsContainer.InvokeDragCommanded(predictedDragCommand);
            }

            if (Input.GetMouseButtonUp(0))
            {
                predictedDragCommand.ConvertToCompleted();
                EventsContainer.InvokeDragCommanded(predictedDragCommand);
            }
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
     /*   private void BuildPlayerInput()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                TouchCommand touch = new TouchCommand(Input.mousePosition);
                EventsContainer.InvokeTouchInput(touch);
            }
        } */
#endif
    }
}


        

