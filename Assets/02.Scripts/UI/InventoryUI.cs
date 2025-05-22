using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent; // ItemSlot을 넣을 부모 오브젝트
    public GameObject itemSlotPrefab; // 프리팹 참조

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
            Debug.LogError("ItemSlot 컴포넌트를 찾을 수 없습니다.");
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
