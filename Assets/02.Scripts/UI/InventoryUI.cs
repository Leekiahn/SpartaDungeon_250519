using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent; // ItemSlot�� ���� �θ� ������Ʈ
    public GameObject itemSlotPrefab; // ������ ����

    //private List<ItemSlot> slotList = new List<ItemSlot>();
    private List<GameObject> slots = new List<GameObject>();

    public void CreateSlot(ItemData data)
    {
        GameObject go = Instantiate(itemSlotPrefab, slotParent);
        ItemSlot slot = go.GetComponent<ItemSlot>();
        if (slot != null)
        {
            slot.SetItem(data);
            slots.Add(go);
        }
        else
        {
            Debug.LogError("ItemSlot ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void RefreshInventory(List<ItemData> itemList)
    {
        foreach (var slot in slots)
            Destroy(slot);
        slots.Clear();

        foreach (var item in itemList)
            CreateSlot(item);
    }

}
