using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMyItem : MonoBehaviour
{
    MyItem myItem;

    [SerializeField] private Button btnUse;
    [SerializeField] private Image imgItem;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtCount;
    [SerializeField] private Text txtAbout;

    void Start()
    {
        btnUse.onClick.AddListener(OnUse);
    }

    public void Setup(MyItem myItem)
    {
        this.myItem = myItem;
        SetUI();
    }

    public void SetUI()
    {
        imgItem.sprite = Resources.Load<Sprite>($"Sprites/Item/{myItem.item.itemName}");
        txtName.text = $"{myItem.item.itemKName}";
        txtCount.text = $"x {myItem.Count}";
        txtAbout.text = $"{myItem.item.itemAbout}";

        if (myItem.item.itemId == 1)
            btnUse.gameObject.SetActive(false);
        else
            btnUse.gameObject.SetActive(true);
    }

    void OnUse()
    {
        if (myItem.Count > 0)
        {
            myItem.Count -= 1;
            txtCount.text = $"x {myItem.Count}";

            if (myItem.Count == 0)
                gameObject.SetActive(false);
        }

        if (myItem.item.itemType == ItemType.Fertilizer)
        {
            GameData.Instance.SetGardenFert((int)myItem.item.saleType, (int)myItem.item.grade);
            //print($"비료 사용 시작");
        }
    }
}