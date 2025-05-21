using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class DoorHandler : MonoBehaviour
{
    public Door curDoor;
    public void OnInteractDoor(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (curDoor != null)
            {
                if(curDoor.IsOpen)
                {
                    curDoor.CloseDoor();
                }
                else
                {
                    curDoor.OpenDoor();
                }
            }
        }
    }
}
