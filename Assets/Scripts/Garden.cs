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

    public float rareChance = 5;    //����۹� Ȯ��
    public float growingSpeed = 1;  //�۹� ���� �ӵ�
    public float wiltTime;          //�۹��� �õ�� �ӵ�
    float spawnTime = 60;           //�۹� ���� Ÿ��

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
        // ����� ��ġ�� �۹��� �����ϱ� ���� ������� ��� ��ġ ����
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach (Node node in NodeList)
        { node.localPosition = node.GetComponent<RectTransform>().localPosition; }

        fertilizer = FertilizerData.Instance.SetListType(FertType.Basic)[(int)FertGrade.C];

        // ������ �� ��Ȯ�� �� �ִ� �۹� 2�� ����
        SpawnCropToRandomNode();
        SpawnCropToRandomNode();

        fertilizer = null;
    }

    private void SpawnCropToRandomNode()
    {
        if (fertilizer == null)
            return;

        // ��� ��带 Ž���ؼ� �۹��� ��ġ�Ǿ� ���� ���� ��� ��� ��ȯ
        List<Node> emptyNodes = NodeList.FindAll(x => x.placedCrop == null);

        if (emptyNodes.Count != 0)
        {
            int index = Random.Range(0, emptyNodes.Count);
            Vector2Int point = emptyNodes[index].Point;

            // �۹��� ��ġ�Ǿ� ���� ���� ������ ��� ��ġ�� �۹� ����
            SpawnCrop(point.x, point.y);
        }
        else
        {
            isFull = true;
            print($"�Թ��� �� á��");
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
        clone.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.35f);   // ������ �۹��� ������ �� ��ġ ����

        crop.Setup(cropsData, fertilizer, rareChance, growingSpeed, wiltTime); //�۹� ���� ����
        node.placedCrop = crop;                                                // ��� ������ �۹��� ��忡 ���
        cropList.Add(crop);                                                    // ����Ʈ�� �۹� ���� ����
    }
    void Update()
    {
        if (usefertilTime)
        {
            fertilTime += Time.deltaTime;
            if (fertilTime >= fertilizer.limitTime * 60)     //��� ��� �ð��� ��ȿ�� ���ȸ� �۹��� �ڶ���
            {
                fertilTime = 0;
                fertilizer = null;
                usefertilTime = false;
            }
            if (fertilTime <= fertilizer.limitTime * 60)     //��� ��� �ð� UI ������Ʈ
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

            ///////////////�׽�Ʈ/////////////////
            //if (time >= 0.2f) //if (time >= spawnTime)
            if (time >= 0.2f)
            {
                time = 0;
                SpawnCropToRandomNode();
            }
        }

    }
}