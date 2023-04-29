using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSaleType
{
    ShopItem,
    RandomBoxItem,
    MaxCount
}
public enum ItemType
{
    Item,
    Fertilizer
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemKName;
    public string itemAbout;
    public int itemCost;
    public ItemSaleType saleType;
    public int itemId;
    public ItemType itemType;
    public int grade;

    public Item(string itemName, string itemKName, string itemAbout, int itemCost, ItemSaleType saleType, int itemId, ItemType itemType, int grade)
    {
        this.itemName = itemName;
        this.itemKName = itemKName;
        this.itemAbout = itemAbout;
        this.itemCost = itemCost;
        this.saleType = saleType;
        this.itemId = itemId;
        this.itemType = itemType;
        this.grade = grade;
    }
}