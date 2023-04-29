using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    Item item;

    [SerializeField] private Button btnBuy;
    [SerializeField] private Image imgItem;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtCost;
    [SerializeField] private Text txtCount;
    [SerializeField] private Text txtAbout;

    void Start()
    {
        btnBuy.onClick.AddListener(OnBuy);
    }

    private void OnEnable()
    {
        txtCount.text = $"보유량: {GameData.Instance.itemList[item.itemId].Count}";
    }

    public void Setup(Item item)
    {
        this.item = item;
        SetUI();
    }

    public void SetUI()
    {
        imgItem.sprite = Resources.Load<Sprite>($"Sprites/Item/{item.itemName}");
        txtName.text = $"{item.itemKName}";
        txtCost.text = $"x {item.itemCost}";
        txtAbout.text = $"{item.itemAbout}";
        txtCount.text = $"보유량: {GameData.Instance.itemList[item.itemId].Count}";
    }
    void OnBuy()
    {
        if (GameData.Instance.Coin >= item.itemCost)
        {
            GameData.Instance.itemList[item.itemId].Count += 1;
            GameData.Instance.Coin -= item.itemCost;
            txtCount.text = $"보유량: {GameData.Instance.itemList[item.itemId].Count}";
        }
        else
        {
            print("돈업슴");
        }
    }
}