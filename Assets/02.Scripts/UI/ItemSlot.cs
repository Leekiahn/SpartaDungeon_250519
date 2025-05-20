using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Button itemSlot;
    public Image icon;
    private ItemData itemData;

    public Button useButton;
    public Button dropButton;
    public Button equipButton;


    public void SetItem(ItemData data)
    {
        itemData = data;
        if (icon != null)
        {
            icon.sprite = itemData.icon;
        }

        //useButton.onClick.AddListener(() => UseItem());
        dropButton.onClick.AddListener(() => DropItem());
        //equipButton.onClick.AddListener(() => EquipItem());

        useButton.gameObject.SetActive(data.itemType == eItemType.consumable);
        equipButton.gameObject.SetActive(data.itemType == eItemType.equipable);
    }

    public void OnClick()
    {
        CharacterManager.Instance.inven.selectedItem = itemData;
    }

    //public void UseItem()
    //{
    //    if (itemData.itemType == eItemType.consumable)
    //    {
    //        Debug.Log("Used: " + itemData.itemName);
    //        PlayerManager.Instance.Heal(itemData.healAmount); // ����
    //        InventoryManager.Instance.RemoveItem(itemData);
    //        Destroy(gameObject);
    //    }
    //}

    public void DropItem()
    {
        Debug.Log("Dropped: " + itemData.name);
        CharacterManager.Instance.inven.RemoveItem(itemData); // �κ��丮���� ����

        // ���忡 �ٽ� ������ ���� ����
        Instantiate(itemData.dropPrefab, CharacterManager.Instance.playerCtrl.dropPos.position, Quaternion.Euler(0,0,0));

        Destroy(gameObject);
    }

    //public void EquipItem()
    //{
    //    Debug.Log("Equipped: " + itemData.itemName);
    //    EquipmentManager.Instance.Equip(itemData);
    //    // �κ��丮���� �������� �ʾƵ� �� (���� UI������ ���� ����)
    //}
}
