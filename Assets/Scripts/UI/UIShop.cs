using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Transform Content;
    [SerializeField] private List<Item> items;
    [SerializeField] private GameObject itemPrefab;

    private void Start()
    {
        items = ItemData.Instance.SetListType(ItemSaleType.ShopItem);
        AddItem();
    }

    void AddItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject go = Instantiate(itemPrefab, Content);
            var item = go.GetComponent<UIItem>();
            item.Setup(items[i]);
        }
    }
}