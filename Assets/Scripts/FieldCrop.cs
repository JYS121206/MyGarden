using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FieldCrop : MonoBehaviour
{
    [SerializeField] private List<Crop> crops;
    [SerializeField] private Image imageCrop;
    private Crop myCrop;
    int wiltCrop = 1;
    public bool isGrowing = false;
    public bool isHarvestable = false;
    [SerializeField] private FertType myFertType;
    
    public float time = 0;
    public float wiltTimer = 0;
    float grownTime = 120;
    float growingSpeed;
    float wiltTime;

    public void Setup(CropsData cropsData, Fertilizer fertilizer, float rareChance, float growingSpeed, float wiltTime)
    {
        //작물 정보 세팅
        imageCrop.color = Color.white; 
        SpringUpAnimation();

        this.growingSpeed = growingSpeed;
        this.wiltTime = wiltTime;
        myFertType = fertilizer.type;
        crops = cropsData.SetCrops(fertilizer.min, fertilizer.max);

        int weight = 0;
        int selectNum = 0;
        int total = 0;

        //레어 확률 조정
        for (int i = 1; i < crops.Count; i++) { total += crops[i].weight; }
        float ratio = total / rareChance;
        int weight_0 = (int)((100 - rareChance) * ratio);
        crops[0].weight = weight_0;
        total = 0;

        //가중치 랜덤 사용해 자라날 작물 선택
        for (int i = 0; i < crops.Count; i++) { total += crops[i].weight; }
        selectNum = Mathf.FloorToInt(total * Random.Range(0.0f, 1.0f));
        for (int i = 0; i < crops.Count; i++)
        {
            weight += crops[i].weight;
            if (selectNum <= weight)
            {
                if (i == wiltCrop)
                {
                    SetMyCrop(0);
                    return;
                }
                SetMyCrop(i);
                return;
            }
        }
    }

    void SetMyCrop(int idx)
    {
        myCrop = crops[idx];
        imageCrop.sprite = Resources.Load<Sprite>($"Sprites/springUp");
    }

    void Update()
    {
        if (myCrop == null)
            return;

        if (isGrowing)
        {
            float growingScale = 1 + 0.2f * Mathf.Sin(Time.time * 2);
            transform.localScale = new Vector3(1, growingScale, growingScale);
        }

        time += Time.deltaTime;

        ///////////////테스트/////////////////
        //if (time >= myCrop.growingSpeed && !isHarvestable)
        //if (time >= grownTime*myCrop.growingSpeed*growingSpeed && !isHarvestable)
        //if (time >= 3 && !isHarvestable)
        if (time >= 3 && !isHarvestable)    //설정한 시간이 지난 후 성장 완료(수확 가능해짐)
        {
            isGrowing = false;
            isHarvestable = true;
            StartCoroutine(OnFadeAnimation(0.15f));
        }

        if (!isHarvestable)
            return;

        wiltTimer += Time.deltaTime;
        //if (wiltTimer >= wiltTime)
        //if (wiltTimer >= 5)
        if (wiltTimer >= 20)                //설정한 시간이 지난 후 작물이 시든다
        {
            myCrop = crops[wiltCrop];
            imageCrop.sprite = Resources.Load<Sprite>($"Sprites/{wiltCrop}");
        }

    }

    public void Harvest()
    {
        if (!isHarvestable) { return; }

        if (GameData.Instance.cropList[myCrop.id].isHarvested == false) //도감 정보의 수확 여부 업데이트를 위한 조건문
        { GameData.Instance.cropList[myCrop.id].isHarvested = true; }

        GameData.Instance.Coin += myCrop.coin;
        Destroy(gameObject);
    }

    public void SpringUpAnimation()
    {
        StartCoroutine(OnScaleAnimation(Vector3.one * 0.5f, Vector3.one, 0.15f));
        isGrowing = true;
    }

    private IEnumerator OnScaleAnimation(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }

    private IEnumerator OnFadeAnimation(float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            Color color = imageCrop.color;
            color.a = Mathf.Lerp(1, 0, percent*0.7f);
            imageCrop.color = color;

            yield return null;
        }

        transform.localScale = Vector3.one;
        imageCrop.sprite = Resources.Load<Sprite>($"Sprites/{myCrop.id}"); //다 자라면 작물 이미지 공개

        current = 0;
        percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            Color color = imageCrop.color;
            color.a = Mathf.Lerp(0, 1, percent);
            imageCrop.color = color;

            yield return null;
        }
    }
}