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

            EffectManager.Instance.PlayEffect(itemData.consumables.type, itemData.consumables.value, itemData.consumables.duration); // 효과 적용
            Destroy(gameObject);
        }
    }

    public void DropItem()
    {
        Debug.Log("Dropped: " + itemData.name);
        CharacterManager.Instance.inven.RemoveItem(itemData); // 인벤토리에서 제거
        CharacterManager.Instance.equipHandler.UnequipItem(); // 장착 해제

        // 월드에 다시 생성할 수도 있음
        Instantiate(itemData.dropPrefab, CharacterManager.Instance.playerCtrl.dropPos.position, Quaternion.Euler(0,0,0));

        Destroy(gameObject);
    }

    public void EquipItem()
    {
        CharacterManager.Instance.equipHandler.EquipItem(itemData); //equip 클래스에 아이템 보내기
        equipButton.gameObject.SetActive(false); // 장착 버튼 비활성화
        unequipButton.gameObject.SetActive(true); // 장착 버튼 비활성화
    }

    public void UnequipItem()
    {
        CharacterManager.Instance.equipHandler.UnequipItem(); //equip 클래스에 아이템 삭제하기
        equipButton.gameObject.SetActive(true);
        unequipButton.gameObject.SetActive(false); // 장착 버튼 비활성화
    }


}
