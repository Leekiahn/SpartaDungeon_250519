using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject curInteractObject;
    public ItemObject curInteractItemObject;
    [SerializeField] private LayerMask InteractableLayerMask;
    public float maxCheckDistance = 3f;

    [SerializeField] private Camera cam;

    [SerializeField] private InteractPromptUI promptUI;
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
            curInteractObject = hit.collider.gameObject;
            curInteractItemObject = curInteractObject.GetComponent<ItemObject>();
            promptUI.itemText.text = curInteractItemObject.GetInteractPrompt();
        }
        else
        {
            curInteractObject = null;
            curInteractItemObject = null;
            promptUI.itemText.text = string.Empty;
        }
    }
}
