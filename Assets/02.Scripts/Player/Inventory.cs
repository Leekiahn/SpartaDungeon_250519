using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items;
    public ItemData selectedItem;

    public Transform EquipPos;

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddItem(Item item)
    {
        //상호작용한 오브젝트의 레이어가 Interactable이라면 아이템 데이터를 리스트에 추가하고 오브젝트 파괴
        if (CharacterManager.Instance.interact.curObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            items.Add(item.itemData);
            Debug.Log("Item added: " + item.itemData.name);

            UIManager.Instance.invenUI.CreateSlot(item.itemData);

            Destroy(item.gameObject);
        }
        else
        {
            return;
        }
    }

    public void RemoveItem(ItemData data)
    {
        if (items.Contains(data))
        {
            items.Remove(data);
            UIManager.Instance.invenUI.RefreshInventory(items);
        }

    }
}
