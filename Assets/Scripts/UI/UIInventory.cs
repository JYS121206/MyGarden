using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private List<MyItem> items = new List<MyItem>();
    [SerializeField] private List<MyItem> fertilizers = new List<MyItem>();

    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private ItemType type = ItemType.Item;

    [SerializeField] private List<UIMyItem> itemPool = new List<UIMyItem>();

    [SerializeField] private Button btnOpenInven;
    [SerializeField] private Button btnShowItem;
    [SerializeField] private Button btnShowFertil;
    [SerializeField] private Text txtShowItem;
    [SerializeField] private Text txtShowFertil;
    [SerializeField] private Color selectColor;
    [SerializeField] private Color baseColor;
    [SerializeField] private Button btnOpenItem;
    [SerializeField] private Button btnOpenFertil;
    
    void Start()
    {
        items = GameData.Instance.myItems;
        fertilizers = GameData.Instance.myFertilizers;
        
        btnOpenInven.onClick.AddListener(OpenInven);
        btnShowItem.onClick.AddListener(ShowItem);
        btnShowFertil.onClick.AddListener(ShowFertil);

        btnOpenItem.onClick.AddListener(ShowItem);
        btnOpenFertil.onClick.AddListener(ShowFertil);

        txtShowItem.color = selectColor;
        txtShowFertil.color = baseColor;
    }
    public void OpenInven()
    {
        foreach (UIMyItem myItem in itemPool)
        { myItem.gameObject.SetActive(false); }

        if (type == ItemType.Item)
        { AddItem(items); }
        else
        { AddItem(fertilizers); }
    }

    void ShowItem()
    {
        type = ItemType.Item;
        txtShowItem.color = selectColor;
        txtShowFertil.color = baseColor;
        OpenInven();
    }
    void ShowFertil()
    {
        type = ItemType.Fertilizer;
        txtShowFertil.color = selectColor;
        txtShowItem.color = baseColor;
        OpenInven();
    }

    void AddItem(List<MyItem> list)
    {
        List<MyItem> temp = new List<MyItem>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Count > 0)
                temp.Add(list[i]);
        }

        if (itemPool.Count >= temp.Count)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                itemPool[i].gameObject.SetActive(true);
                itemPool[i].Setup(temp[i]);
            }
        }
    }
}