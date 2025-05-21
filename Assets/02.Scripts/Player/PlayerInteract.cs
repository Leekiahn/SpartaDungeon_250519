using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject curObject;
    public Item item;
    public Door door;
    [SerializeField] private LayerMask InteractableLayerMask;
    [SerializeField] private LayerMask doorLayerMask;

    public float maxCheckDistance = 3f;

    [SerializeField] private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxCheckDistance, InteractableLayerMask))
        {
            curObject = hit.collider.gameObject;
            item = curObject.GetComponent<Item>();
            UIManager.Instance.interactPromptUI.itemText.text = item.GetInteractPrompt();
        }
        else if(Physics.Raycast(ray, out hit, maxCheckDistance, doorLayerMask))     //¹® °¨Áö
        {
            curObject = hit.collider.gameObject;
            door = curObject.GetComponent<Door>();
            CharacterManager.Instance.doorHandler.curDoor = door;
            UIManager.Instance.interactPromptUI.itemText.text = door.GetInteractPrompt();
        }
        else
        {
            curObject = null;
            item = null;
            UIManager.Instance.interactPromptUI.itemText.text = string.Empty;
        }
    }
}
