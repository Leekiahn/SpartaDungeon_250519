using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    private float doorSpeed = 3f;

    private Quaternion originRot;
    private Quaternion openRot;

    private void Start()
    {
        originRot = transform.rotation;
        openRot = Quaternion.Euler(-90, -90, 0);
    }

    private void Update()
    {
        Quaternion targetRotation = IsOpen ? openRot : originRot;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * doorSpeed);
    }

    public void OpenDoor()
    {
        IsOpen = true;
    }

    public void CloseDoor()
    {
        IsOpen = false;
    }

    public string GetInteractPrompt()
    {
        string str = "[E] ¹®¿­±â";
        return str;
    }
}
