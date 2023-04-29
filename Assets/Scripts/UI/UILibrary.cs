using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Harvested
{
    public Crop crop;
    public bool isHarvested;

    public Harvested(Crop crop, bool isHarvested)
    {
        this.crop = crop;
        this.isHarvested = isHarvested;
    }
}

public class UILibrary : MonoBehaviour
{
    #region Library
    [Header("Library")]
    [SerializeField] private GameObject objLibrary;
    [SerializeField] private GameObject objButtons;
    [SerializeField] private Button btnOpenLibrary;
    [SerializeField] private Button btnExit;
    private Vector3 buttonsPos;
    #endregion

    #region Commons
    [Header("Commons")]
    [SerializeField] private Button btnPercentage;
    [SerializeField] private Text txtPercentage;
    public List<Harvested> cropList = new List<Harvested>();
    public List<Harvested> myCrops = new List<Harvested>();
    private int myCrop;
    bool prev = true;
    bool next = true;
    bool prevPage = true;
    bool nextPage = true;
    int curPage = 0;
    int minPage = 0;
    int maxPage;
    [SerializeField] private FertType libraryType = FertType.Basic;
    #endregion

    #region List
    [Header("List")]
    [SerializeField] private GameObject objCropsList;
    [SerializeField] private GameObject objRooms;

    [SerializeField] private Button btnPrevList;
    [SerializeField] private Button btnNextList;
    [SerializeField] private Text txtListPage;

    [SerializeField] private Button[] btnRooms = new Button[9];
    #endregion

    #region Profile
    [Header("Profile")]
    [SerializeField] private GameObject objCropProfile;

    [SerializeField] private GameObject objLockedProfile;
    [SerializeField] private GameObject objProfile;

    [SerializeField] private Button btnPrevProfile;
    [SerializeField] private Button btnNextProfile;
    [SerializeField] private Text txtProfileCropNum;

    [SerializeField] private Image imgProfileCrop;
    [SerializeField] private Text txtProfileName;
    [SerializeField] private Text txtProfileCoin;
    [SerializeField] private Text txtProfileAbout;
    [SerializeField] private Button btntoList;
    #endregion

    void Start()
    {
        #region Use UIBase
        //Bind<GameObject>(typeof(GameObjects));
        //Bind<Button>(typeof(Buttons));
        //Bind<Image>(typeof(Images));
        //Bind<Text>(typeof(Texts));

        //GetGameObject(GameObjects.objLibrary).SetActive(false);
        //GetButton(Buttons.btnOpenLibrary).onClick.AddListener(OpenLibrary);
        #endregion

        btnRooms = objRooms.GetComponentsInChildren<Button>();
        objButtons = btnOpenLibrary.transform.parent.gameObject;
        buttonsPos = objButtons.transform.position;
        cropList = GameData.Instance.cropList;

        if (libraryType == FertType.Basic)
        {
            for (int i = 0; i < 16; i++)
            {
                myCrops.Add(cropList[i]);
            }
        }
        else if (libraryType == FertType.Special)
        {
            for (int i = 16; i < 32; i++)
            {
                myCrops.Add(cropList[i]);
            }
        }

        btnOpenLibrary.onClick.AddListener(OpenLibrary);
        btnExit.onClick.AddListener(CloseLibrary);

        if (myCrops.Count < btnRooms.Length)
            maxPage = 0;
        else
            maxPage = myCrops.Count / btnRooms.Length;

        if (maxPage == minPage)
        {
            btnPrevList.GetComponent<Image>().color = Color.gray;
            btnNextList.GetComponent<Image>().color = Color.gray;

            nextPage = false;
            prevPage = false;
        }

        txtListPage.text = $"{curPage + 1}/{maxPage + 1}";

        btnPrevList.onClick.AddListener(ShowPrevList);
        btnNextList.onClick.AddListener(ShowNextList);
        btnPrevProfile.onClick.AddListener(ShowPrevProfile);
        btnNextProfile.onClick.AddListener(ShowNextProfile);
        btnPercentage.onClick.AddListener(OnSetLibraryType);
    }
    private IEnumerator OnMoveAnimation(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            objButtons.transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    void OpenLibrary()
    {
        //도감 오브젝트 활성화
        //GetGameObject(GameObjects.objLibrary).SetActive(true);

        var pos = buttonsPos;
        pos.y = -pos.y;

        StopAllCoroutines();
        StartCoroutine(OnMoveAnimation(buttonsPos, pos, 0.15f));

        objLibrary.SetActive(true);

        if (libraryType == FertType.Basic)
        {
            btnPercentage.gameObject.GetComponent<Image>().color = Color.yellow + Color.gray;
        }
        else
        {
            btnPercentage.gameObject.GetComponent<Image>().color = Color.red + Color.gray;
        }

        SetPercentage($"{libraryType} Library");
        ToList(0);
    }

    void CloseLibrary()
    {
        //도감 오브젝트 비활성화
        objLibrary.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(OnMoveAnimation(objButtons.transform.position, buttonsPos, 0.15f));
    }

    void OnSetLibraryType()
    {
        myCrops.Clear();

        //도감의 종류를 세팅
        if (libraryType == FertType.Basic)
        {
            libraryType = FertType.Special;
            btnPercentage.gameObject.GetComponent<Image>().color = Color.red + Color.gray;
            for (int i = 16; i < 32; i++)
            {
                myCrops.Add(cropList[i]);
            }
        }
        else
        {
            libraryType = FertType.Basic;
            btnPercentage.gameObject.GetComponent<Image>().color = Color.yellow + Color.gray;
            for (int i = 0; i < 16; i++)
            {
                myCrops.Add(cropList[i]);
            }
        }

        SetPercentage($"{libraryType} Library");
        ToList(0);
    }

    void SetPercentage(string type)
    {
        //도감의 수집 달성률을 세팅
        int max = myCrops.Count;
        int cur = 0;
        for (int i = 0; i < max; i++)
        {
            if (myCrops[i].isHarvested == true)
                cur++;
        }
        float val = (float)cur / (float)max;
        int percent = (int)(val * 100);
        txtPercentage.text = $"{type} {percent}%";
    }

    void ShowPrevList()
    {
        if (maxPage == minPage) { return; }

        if (!prevPage) return;

        curPage = curPage - 1;
        if (curPage == minPage)
        {
            //그 이전 장이 없으면 이미지 변경
            prevPage = false;
            btnPrevList.GetComponent<Image>().color = Color.gray;
        }
        if (!nextPage)
        {
            nextPage = true;
            btnNextList.GetComponent<Image>().color = Color.white;
        }
        //이전 장 있으면 보여주고
        SetList(curPage);
        //장수 변경
        txtListPage.text = $"{curPage + 1}/{maxPage + 1}";
    }

    void ShowNextList()
    {
        if (maxPage == minPage) {return; }

        if (!nextPage) return;

        curPage = curPage + 1;
        if (curPage == maxPage)
        {
            //그 다음 장이 없으면 이미지 변경
            nextPage = false;
            btnNextList.GetComponent<Image>().color = Color.gray;
        }
        if (!prevPage)
        {
            prevPage = true;
            btnPrevList.GetComponent<Image>().color = Color.white;
        }
        //다음 장 있으면 보여주고
        SetList(curPage);
        //장수 변경
        txtListPage.text = $"{curPage + 1}/{maxPage + 1}";
    }

    void ShowPrevProfile()
    {
        if (!prev) return;

        myCrop = myCrop - 1;

        if (myCrop == 0)
        {
            //그 이전 작물이 없으면 이미지 변경
            prev = false;
            btnPrevProfile.GetComponent<Image>().color = Color.gray;
        }
        if (!next)
        {
            next = true;
            btnNextProfile.GetComponent<Image>().color = Color.white;
        }

        //이전 작물 있으면 보여주고
        //넘버 변경
        SetProfile(myCrop);
    }

    void ShowNextProfile()
    {
        if (!next) return;

        myCrop = myCrop + 1;

        if (myCrop + 1 >= myCrops.Count)
        {
            //그 다음 작물이 없으면 이미지 변경
            next = false;
            btnNextProfile.GetComponent<Image>().color = Color.gray;
        }
        if (!prev)
        {
            prev = true;
            btnPrevProfile.GetComponent<Image>().color = Color.white;
        }

        //다음 작물 있으면 보여주고
        //넘버 변경
        SetProfile(myCrop);
    }

    void ToList(int page)
    {
        btnPercentage.onClick.RemoveAllListeners();
        btnPercentage.onClick.AddListener(OnSetLibraryType);

        curPage = page;
        //프로필 오브젝트를 비활성화하고
        objCropProfile.SetActive(false);
        //리스트 오브젝트를 활성화한다
        objCropsList.SetActive(true);
        //리스트는 page페이지로 세팅
        SetList(curPage);

        txtListPage.text = $"{curPage + 1}/{maxPage + 1}";

        if (maxPage == minPage)
        {
            prevPage = false;
            nextPage = false;
            btnPrevList.GetComponent<Image>().color = Color.gray;
            btnNextList.GetComponent<Image>().color = Color.gray;
            return;
        }

        if (curPage == minPage)
        {
            prevPage = false;
            btnPrevList.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            prevPage = true;
            btnPrevList.GetComponent<Image>().color = Color.white;
        }
        if (curPage == maxPage)
        {
            nextPage = false;
            btnNextList.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            nextPage = true;
            btnNextList.GetComponent<Image>().color = Color.white;
        }
    }

    void SetList(int page)
    {
        for (int i = 0; i < btnRooms.Length; i++)
        {
            int idx = i + (btnRooms.Length * page);

            if (idx >= myCrops.Count)
            {
                btnRooms[i].gameObject.SetActive(false);
            }
            else
            {
                btnRooms[i].gameObject.SetActive(true);

                btnRooms[i].gameObject.GetComponentInChildren<Text>().text = $"No.{myCrops[idx].crop.id+1}";

                if (myCrops[idx].isHarvested)
                {
                    btnRooms[i].gameObject.GetComponentsInChildren<Image>()[1].color = Color.white; //테스트
                    btnRooms[i].gameObject.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>($"Sprites/{myCrops[idx].crop.id}");
                    btnRooms[i].onClick.RemoveAllListeners();
                    btnRooms[i].onClick.AddListener(() => { OpenProfile(idx); });
                    btnRooms[i].onClick.AddListener(() => { SetIntMyCrop(idx); });
                }
                else
                {
                    btnRooms[i].gameObject.GetComponentsInChildren<Image>()[1].color = Color.black; //테스트
                    btnRooms[i].gameObject.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>($"Sprites/{myCrops[idx].crop.id}");
                    btnRooms[i].onClick.RemoveAllListeners();
                }
            }
        }
    }

    void SetIntMyCrop(int idx)
    {
        myCrop = idx;
        if (myCrop == 0)
        {
            //그 이전 작물이 없으면 이미지 변경
            prev = false;
            btnPrevProfile.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            prev = true;
            btnPrevProfile.GetComponent<Image>().color = Color.white;
        }
        if (myCrop + 1 >= myCrops.Count)
        {
            //그 다음 작물이 없으면 이미지 변경
            next = false;
            btnNextProfile.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            next = true;
            btnNextProfile.GetComponent<Image>().color = Color.white;
        }
    }

    void OpenProfile(int idx)
    {
        btnPercentage.onClick.RemoveAllListeners();

        objCropsList.SetActive(false);
        objCropProfile.SetActive(true);
        SetProfile(idx);
    }

    void SetProfile(int idx)
    {
        print("넘김");
        //프로필 정보 세팅
        //작물 이미지, 이름, 코인, 설명 세팅

        var crop = myCrops[idx].crop;
        txtProfileCropNum.text = $"No.{crop.id + 1}";

        int page;
        if (idx < 9) { page = 0; }
        else { page = idx / btnRooms.Length; }
        btntoList.onClick.RemoveAllListeners();
        btntoList.onClick.AddListener(() => { ToList(page); });

        if (myCrops[idx].isHarvested == false)
        {
            //잠금모드로 보여줌
            objProfile.SetActive(false);
            objLockedProfile.SetActive(true);
            return;
        }

        objLockedProfile.SetActive(false);
        objProfile.SetActive(true);

        imgProfileCrop.sprite = Resources.Load<Sprite>($"Sprites/{crop.id}");
        txtProfileName.text = crop.name;
        txtProfileCoin.text = $"x{crop.coin}";
        txtProfileAbout.text = crop.about;
    }

}