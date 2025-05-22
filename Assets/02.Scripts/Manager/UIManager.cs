using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public PlayerConditionUI playerConditionUI;
    public InteractPromptUI interactPromptUI;
    public InventoryUI invenUI;
    public GameObject crosshair;

    private void Start()
    {
        playerConditionUI = GetComponentInChildren<PlayerConditionUI>();
        if (playerConditionUI == null)
        {
            Debug.LogError("PlayerConditionUI component not found on this GameObject.");
            return;
        }

        interactPromptUI = GetComponentInChildren<InteractPromptUI>();
        if (interactPromptUI == null)
        {
            Debug.LogError("InteractPromptUI component not found on this GameObject.");
            return;
        }

        if (invenUI == null)
        {
            Debug.LogError("InventoryUI component not found on this GameObject.");
            return;
        }

        crosshair = GameObject.Find("Crosshair");
        if (crosshair == null)
        {
            Debug.LogError("Crosshair GameObject not found in the scene.");
            return;
        }
    }
}
