using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyItem
{
    public Item item;
    public int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    public MyItem(Item item)
    {
        this.item = item;
        this.Count = 0;
    }
}

public class GameData : MonoBehaviour
{
    private static GameData _instance = null;

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@GameData");
                if (go == null)
                {
                    go = new GameObject { name = "@GameData" };
                    go.AddComponent<GameData>();
                }
                _instance = go.GetComponent<GameData>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public UIGame uIGame;
    public Garden garden;

    public List<MyItem> itemList = new List<MyItem>();
    public List<MyItem> myItems = new List<MyItem>();
    public List<MyItem> myFertilizers = new List<MyItem>();
    public List<Harvested> cropList = new List<Harvested>();

    private int coin;
    public int Coin
    {
        get { return coin; }
        set
        {
            coin = value;
            uIGame.SetTextCoin();
        }
    }

    private void Awake()
    {
        uIGame = GameObject.Find("UIGame").GetComponent<UIGame>();
        garden = GameObject.Find("Garden").GetComponent<Garden>();
    }

    public void SetupCropList(List<Crop> crops)
    {
        var cropsData = crops;

        for (int i = 0; i < cropsData.Count; i++)
        {
            Harvested crop = new Harvested(cropsData[i], false);
            cropList.Add(crop);
        }
    }
    public void SetupItemList(List<Item> items)
    {
        var itemsData = items;

        for (int i = 0; i < itemsData.Count; i++)
        {
            MyItem myItem = new MyItem(itemsData[i]);
            itemList.Add(myItem);

            if (myItem.item.itemType == ItemType.Item)
                myItems.Add(myItem);
            else
                myFertilizers.Add(myItem);
        }
    }

    public void SetGardenFert(int _type, int _grade)
    {
        FertType type = (FertType)_type;
        FertGrade grade = (FertGrade)_grade;

        garden.fertilizer = FertilizerData.Instance.SetListType(type)[(int)grade];
        garden.usefertilTime = true;
        garden.fertilTime = 0;
    }
}