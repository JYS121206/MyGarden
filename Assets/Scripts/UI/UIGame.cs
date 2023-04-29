using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private Image imgGauge;
    [SerializeField] private Text txtMyCoin;
    [SerializeField] private GameObject objMyCoin;

    [SerializeField] private GameObject objFacility;
    [SerializeField] private GameObject objDeveloper;
    [SerializeField] private GameObject objRandomBox;
    [SerializeField] private GameObject objShop;
    [SerializeField] private GameObject objInven;

    [SerializeField] private Button btnOpenLibrary;
    [SerializeField] private Button btnDevelop;
    [SerializeField] private Button btnRandomBox;
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnOpenInven;

    [SerializeField] private Button btnCloseFacility;
    [SerializeField] private Button btnCloseShop;
    [SerializeField] private Button btnCloseLibrary;
    [SerializeField] private Button btnCloseInven;

    [SerializeField] private Button btnOpenItem;
    [SerializeField] private Button btnOpenFertil;

    [SerializeField] private Button tttest;
    bool onUI = false;
    public bool OnUI => onUI; 
    bool swapBtnDevelop = false;
    bool swapBtnRandomBox = false;
    bool swapBtnShop = false;
    bool swapBtnInven = false;

    private void Start()
    {
        
        btnOpenLibrary.onClick.AddListener(OnOpenLibrary);
        btnCloseLibrary.onClick.AddListener(CloseLibrary);
        btnRandomBox.onClick.AddListener(OnRandomBox);
        btnDevelop.onClick.AddListener(OnDevelop);
        btnCloseFacility.onClick.AddListener(CloseFacility);
        btnShop.onClick.AddListener(OnShop);
        btnCloseShop.onClick.AddListener(CloseShop);

        btnOpenInven.onClick.AddListener(OnInven);
        btnCloseInven.onClick.AddListener(CloseInven);

        btnOpenItem.onClick.AddListener(OnInven);
        btnOpenFertil.onClick.AddListener(OnInven);

        CloseFacility();
        CloseShop();

        tttest.onClick.AddListener(Test);
    }

    void Test()
    { //테스트용
        GameData.Instance.Coin += 1000000;
    }

    public void SetGauge(float curr, float max)
    {
        Vector3 temp = imgGauge.gameObject.transform.localScale;
        temp.x = 1-(curr / max);
        imgGauge.gameObject.transform.localScale = temp;
    }

    public void SetTextCoin()
    {
        txtMyCoin.text = $"x {GameData.Instance.Coin}";
    }
    void OnOpenLibrary()
    {
        onUI = true;
        if (swapBtnDevelop || swapBtnRandomBox) { CloseFacility(); }
        if (swapBtnShop) { CloseShop(); }
        if (swapBtnInven) { CloseInven(); }
        objMyCoin.SetActive(false);
    }
    void OnRandomBox()
    {
        if (swapBtnDevelop)
        {
            swapBtnDevelop = false;
            objDeveloper.SetActive(swapBtnDevelop);
        }
        if (swapBtnShop) { CloseShop(); }
        if (swapBtnInven) { CloseInven(); }

        swapBtnRandomBox = !swapBtnRandomBox;
        onUI = swapBtnRandomBox;
        objRandomBox.SetActive(swapBtnRandomBox);
        objFacility.SetActive(swapBtnRandomBox);
    }
    void OnDevelop()
    {
        if (swapBtnRandomBox)
        {
            swapBtnRandomBox = false;
            objRandomBox.SetActive(swapBtnRandomBox);
        }
        if (swapBtnShop) { CloseShop(); }
        if (swapBtnInven) { CloseInven(); }

        swapBtnDevelop = !swapBtnDevelop;
        onUI = swapBtnDevelop;
        objDeveloper.SetActive(swapBtnDevelop);
        objFacility.SetActive(swapBtnDevelop);
    }
    void OnShop()
    {
        if (swapBtnRandomBox || swapBtnDevelop)
        { CloseFacility(); }
        if (swapBtnInven) { CloseInven(); }

        swapBtnShop = !swapBtnShop;
        onUI = swapBtnShop;
        objShop.SetActive(swapBtnShop);
    }
    void CloseLibrary()
    {
        onUI = false;
        objMyCoin.SetActive(true);
    }
    void CloseFacility()
    {
        onUI = false;
        swapBtnDevelop = false;
        swapBtnRandomBox = false;
        objDeveloper.SetActive(false);
        objRandomBox.SetActive(false);
        objFacility.SetActive(false);
    }
    void CloseShop()
    {
        onUI = false;
        swapBtnShop = false;
        objShop.SetActive(false);
    }

    void OnInven()
    {
        if (swapBtnDevelop || swapBtnRandomBox) { CloseFacility(); }
        if (swapBtnShop) { CloseShop(); }

        swapBtnInven = !swapBtnInven;
        onUI = swapBtnInven;
        objInven.SetActive(swapBtnInven);
    }
    void CloseInven()
    {
        onUI = false;
        swapBtnInven = false;
        objInven.SetActive(false);
    }
}