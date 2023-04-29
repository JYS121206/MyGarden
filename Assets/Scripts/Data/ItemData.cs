using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    private static ItemData _instance = null;
    public static ItemData Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@ItemData");
                if (go == null)
                {
                    go = new GameObject { name = "@ItemData" };
                    go.AddComponent<ItemData>();
                }
                _instance = go.GetComponent<ItemData>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public List<Item>[] items = new List<Item>[(int)ItemSaleType.MaxCount];
    public List<Item> ShopItems = new List<Item>();
    public List<Item> RandomBoxItems = new List<Item>();
    private TextAsset ItemTextAsset;

    private void Awake()
    {
        ItemTextAsset = Resources.Load<TextAsset>("Items");
        string[] lines = ItemTextAsset.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split('\t');

            string itemName = row[0];
            string itemKName = row[1];
            string itemAbout = row[2];
            int itemCost = int.Parse(row[3]);
            int saletypeNum = int.Parse(row[4]);
            ItemSaleType saleType = (ItemSaleType)saletypeNum;
            int itemId = int.Parse(row[5]);
            int typeNum = int.Parse(row[6]);
            ItemType itemType = (ItemType)typeNum;
            int grade = int.Parse(row[7]);

            if (saleType == ItemSaleType.ShopItem)
            { ShopItems.Add(new Item(itemName, itemKName, itemAbout, itemCost, saleType, itemId, itemType, grade)); }
            else
            { RandomBoxItems.Add(new Item(itemName, itemKName, itemAbout, itemCost, saleType, itemId, itemType, grade)); }
        }

        items[(int)ItemSaleType.ShopItem] = ShopItems;
        items[(int)ItemSaleType.RandomBoxItem] = RandomBoxItems;

        GameData.Instance.SetupItemList(SetListType(ItemSaleType.ShopItem));
        GameData.Instance.SetupItemList(SetListType(ItemSaleType.RandomBoxItem));
    }

    public List<Item> SetListType(ItemSaleType type)
    { return items[(int)type]; }
}