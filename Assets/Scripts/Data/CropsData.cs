using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsData : MonoBehaviour
{
    private static CropsData _instance = null;
    public static CropsData Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@CropsData");
                if (go == null)
                {
                    go = new GameObject { name = "@CropsData" };
                    go.AddComponent<CropsData>();
                }
                _instance = go.GetComponent<CropsData>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public Crop[] crops;
    TextAsset cropsTextAsset;

    private void Awake()
    {
        cropsTextAsset = Resources.Load<TextAsset>("crops");
        string[] lines = cropsTextAsset.text.Split('\n');
        crops = new Crop[lines.Length - 1];
        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split('\t');

            int id = int.Parse(row[0]);
            int revise = int.Parse(row[1]);
            int num = int.Parse(row[2]);
            string name = row[3];
            string about = row[4];
            float growingSpeed = float.Parse(row[5]);
            int coin = int.Parse(row[6]);
            int weight = int.Parse(row[7]);

            crops[i - 1] = new Crop(id, revise, num, name, about, growingSpeed, coin, weight);
            crops[i - 1].Numeric = crops[i - 1].num;
        }
        GameData.Instance.SetupCropList(SetCrops(0, 32));
    }

    public List<Crop> SetCrops(int min, int max)
    {
        List<Crop> cropList = new List<Crop>();

        if (min <= 0)
        {
            foreach (Crop data in crops)
            {
                if (data.id < max)
                    cropList.Add(data);
            }
        }
        else
        {
            cropList.Add(crops[0]);
            cropList.Add(crops[1]);
            foreach (Crop data in crops)
            {
                if (data.id >= min && data.id < max)
                    cropList.Add(data);
            }
        }
        return cropList;
    }
}