using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject player;
    public PlayerCtrl playerCtrl;
    public PlayerCondition condition;
    public PlayerAnimCtrl anim;
    public PlayerInteract interact;
    public Inventory inven;


    private void Awake()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
        if (playerCtrl == null)
        {
            Debug.LogError("PlayerCtrl component not found on this GameObject.");
            return;
        }

        condition = player.GetComponent<PlayerCondition>();
        if (condition == null)
        {
            Debug.LogError("PlayerCondition component not found on this GameObject.");
            return;
        }

        anim = player.GetComponent<PlayerAnimCtrl>();
        if (anim == null)
        {
            Debug.LogError("PlayerAnimCtrl component not found on this GameObject.");
            return;
        }

        interact = player.GetComponent<PlayerInteract>();
        if (interact == null)
        {
            Debug.LogError("PlayerInteract component not found on this GameObject.");
            return;
        }

        inven = player.GetComponent<Inventory>();
        if (inven == null)
        {
            Debug.LogError("Inventory component not found on this GameObject.");
            return;
        }
    }

}
