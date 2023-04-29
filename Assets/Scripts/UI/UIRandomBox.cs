using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRandomBox : UIDeveloper
{
    [SerializeField] private Button btnDraw;
    [SerializeField] private UIPopup uIPopup;

    void Start()
    {
        base.Setup();
        btnDraw = gameObject.GetComponentsInChildren<Button>()[1];
        btnDraw.onClick.AddListener(OnDraw);
    }

    public override void UpgradeFacility() { base.UpgradeFacility(); }

    void OnDraw()
    {
        if (GameData.Instance.itemList[1].Count > 0)
        {
            var draw = facility.gameObject.GetComponent<RandomBox>().DrawFertilizer();
            uIPopup.Setup(draw);
            GameData.Instance.itemList[draw.itemId].Count += 1;
            GameData.Instance.itemList[1].Count -= 1;
        }
        else
        {
            print("»Ì±â±Ç ¾øÀ½!!");
        }
    }
}