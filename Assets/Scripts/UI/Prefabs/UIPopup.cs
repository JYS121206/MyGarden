using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private GameObject objPopup;
    [SerializeField] private Transform imgEffect;
    [SerializeField] private Image imgResult;
    [SerializeField] private Text txtResultName;
    [SerializeField] private Button btnGet;

    Crop crop;
    Item item;

    private void Start()
    {
        btnGet.onClick.AddListener(ClosePopup);
        ClosePopup();
    }
    private void Update()
    {
        imgEffect.Rotate(Vector3.forward * 50 * Time.deltaTime);
    }

    public void Setup(Item _item)
    {
        item = _item;

        print(item.itemName);
        objPopup.SetActive(true);

        imgResult.sprite = Resources.Load<Sprite>($"Sprites/Item/{item.itemName}");
        txtResultName.text = $"{item.itemKName}";
    }
    public void Setup(Crop _crop)
    {
        crop = _crop;
        print(crop.name);
        imgResult.sprite = Resources.Load<Sprite>($"Sprites/{crop.name}");
        txtResultName.text = $"{crop.name}";

        objPopup.SetActive(true);
    }
    void ClosePopup()
    {
        objPopup.SetActive(false);
    }
}