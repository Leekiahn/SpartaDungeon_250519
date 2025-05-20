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
        //��ȣ�ۿ��� ������Ʈ�� ���̾ Interactable�̶�� ������ �����͸� ����Ʈ�� �߰��ϰ� ������Ʈ �ı�
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
