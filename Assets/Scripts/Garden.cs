using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Wait = 0, Growing, End }

public class Garden : MonoBehaviour
{
    [SerializeField] private UIGame uIGame;

    [SerializeField] private NodeSpawner nodeSpawner;
    [SerializeField] private GameObject cropPrefab;
    [SerializeField] private Transform cropRect;
    public List<Node> NodeList { private set; get; }
    public Vector2Int CropCount { private set; get; }

    private List<FieldCrop> cropList;

    private float nodeSize;

    public Fertilizer fertilizer;
    public SunLight sunLight;
    public WaterWorks waterWorks;
    public Thermostat thermostat;

    public float rareChance = 5;    //희귀작물 확률
    public float growingSpeed = 1;  //작물 성장 속도
    public float wiltTime;          //작물이 시드는 속도
    float spawnTime = 60;           //작물 스폰 타임

    CropsData cropsData;
    public float time = 0;
    public float fertilTime = 0;
    public bool usefertilTime = false;
    bool isFull;

    private void Awake()
    {
        CropCount = new Vector2Int(4, 4);

        nodeSize = (1080 - 85 - 25 * (CropCount.x - 1)) / CropCount.x;

        NodeList = nodeSpawner.SpawnNodes(this, CropCount, nodeSize);

        cropList = new List<FieldCrop>();
        cropsData = CropsData.Instance;
    }


    void Start()
    {
        // 노드의 위치에 작물을 생성하기 위해 리빌드로 노드 위치 갱신
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach (Node node in NodeList)
        { node.localPosition = node.GetComponent<RectTransform>().localPosition; }

        fertilizer = FertilizerData.Instance.SetListType(FertType.Basic)[(int)FertGrade.C];

        // 시작할 때 수확할 수 있는 작물 2개 생성
        SpawnCropToRandomNode();
        SpawnCropToRandomNode();

        fertilizer = null;
    }

    private void SpawnCropToRandomNode()
    {
        if (fertilizer == null)
            return;

        // 모든 노드를 탐색해서 작물이 배치되어 있지 않은 노드 목록 반환
        List<Node> emptyNodes = NodeList.FindAll(x => x.placedCrop == null);

        if (emptyNodes.Count != 0)
        {
            int index = Random.Range(0, emptyNodes.Count);
            Vector2Int point = emptyNodes[index].Point;

            // 작물이 배치되어 있지 않은 랜덤한 노드 위치에 작물 생성
            SpawnCrop(point.x, point.y);
        }
        else
        {
            isFull = true;
            print($"텃밭이 꽉 찼다");
        }
    }

    private void SpawnCrop(int x, int y)
    {
        if (NodeList[y * CropCount.x + x].placedCrop != null) return;

        GameObject clone = Instantiate(cropPrefab, cropRect);
        FieldCrop crop = clone.GetComponent<FieldCrop>();
        Node node = NodeList[y * CropCount.x + x];

        clone.GetComponent<RectTransform>().sizeDelta = new Vector2(nodeSize, nodeSize);
        clone.GetComponent<RectTransform>().localPosition = node.localPosition;
        clone.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.35f);   // 생성한 작물의 사이즈 및 위치 설정

        crop.Setup(cropsData, fertilizer, rareChance, growingSpeed, wiltTime); //작물 정보 세팅
        node.placedCrop = crop;                                                // 방금 생성한 작물을 노드에 등록
        cropList.Add(crop);                                                    // 리스트에 작물 정보 저장
    }
    void Update()
    {
        if (usefertilTime)
        {
            fertilTime += Time.deltaTime;
            if (fertilTime >= fertilizer.limitTime * 60)     //비료 사용 시간이 유효한 동안만 작물이 자란다
            {
                fertilTime = 0;
                fertilizer = null;
                usefertilTime = false;
            }
            if (fertilTime <= fertilizer.limitTime * 60)     //비료 사용 시간 UI 업데이트
            {
                uIGame.SetGauge(fertilTime, fertilizer.limitTime * 60);
            }

            List<Node> emptyNodes = NodeList.FindAll(x => x.placedCrop == null);
            if (emptyNodes.Count == 0)
            {
                time = 0;
                return;
            }

            time += Time.deltaTime;

            ///////////////테스트/////////////////
            //if (time >= 0.2f) //if (time >= spawnTime)
            if (time >= 0.2f)
            {
                time = 0;
                SpawnCropToRandomNode();
            }
        }

    }
}