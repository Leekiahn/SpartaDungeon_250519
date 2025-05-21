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
    public Button unequipButton;


    public void SetItem(ItemData data)
    {
        itemData = data;
        if (icon != null)
        {
            icon.sprite = itemData.icon;
        }

        useButton.onClick.AddListener(() => UseItem());
        dropButton.onClick.AddListener(() => DropItem());
        equipButton.onClick.AddListener(() => EquipItem());
        unequipButton.onClick.AddListener(() => UnequipItem());
        
        
        useButton.gameObject.SetActive(data.itemType == eItemType.consumable);
        equipButton.gameObject.SetActive(data.itemType == eItemType.equipable);
        dropButton.gameObject.SetActive(true);
    }

    public void OnClick()
    {
        CharacterManager.Instance.inven.selectedItem = itemData;
    }

    public void UseItem()
    {
        if (itemData.itemType == eItemType.consumable)
        {
            Debug.Log("Used: " + itemData.name);

            EffectManager.Instance.PlayEffect(itemData.consumables.type, itemData.consumables.value, itemData.consumables.duration); // ȿ�� ����
            Destroy(gameObject);
        }
    }

    public void DropItem()
    {
        Debug.Log("Dropped: " + itemData.name);
        CharacterManager.Instance.inven.RemoveItem(itemData); // �κ��丮���� ����
        CharacterManager.Instance.equipHandler.UnequipItem(); // ���� ����

        // ���忡 �ٽ� ������ ���� ����
        Instantiate(itemData.dropPrefab, CharacterManager.Instance.playerCtrl.dropPos.position, Quaternion.Euler(0,0,0));

        Destroy(gameObject);
    }

    public void EquipItem()
    {
        CharacterManager.Instance.equipHandler.EquipItem(itemData); //equip Ŭ������ ������ ������
        equipButton.gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
        unequipButton.gameObject.SetActive(true); // ���� ��ư ��Ȱ��ȭ
    }

    public void UnequipItem()
    {
        CharacterManager.Instance.equipHandler.UnequipItem(); //equip Ŭ������ ������ �����ϱ�
        equipButton.gameObject.SetActive(true);
        unequipButton.gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
    }


}
