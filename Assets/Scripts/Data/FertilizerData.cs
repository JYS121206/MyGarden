using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerData : MonoBehaviour
{
    private static FertilizerData _instance = null;
    public static FertilizerData Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@FertilizerData");
                if (go == null)
                {
                    go = new GameObject { name = "@FertilizerData" };
                    go.AddComponent<FertilizerData>();
                }
                _instance = go.GetComponent<FertilizerData>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public List<Fertilizer>[] fertilizers = new List<Fertilizer>[(int)FertType.MaxCount];
    public List<Fertilizer> basicFertil = new List<Fertilizer>();
    public List<Fertilizer> specialFertil = new List<Fertilizer>();
    private TextAsset fertilTextAsset;

    private void Awake()
    {
        fertilTextAsset = Resources.Load<TextAsset>("Fertilizer");
        string[] lines = fertilTextAsset.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split('\t');

            int id = int.Parse(row[0]);
            string name = row[1];
            int min = int.Parse(row[2]);
            int max = int.Parse(row[3]);
            float limitTime = float.Parse(row[4]);
            int typeNum = int.Parse(row[5]);
            int gradeNum = int.Parse(row[6]);
            FertType type = (FertType)typeNum;
            FertGrade grade = (FertGrade)gradeNum;

            if (type == FertType.Basic)
            { 
                basicFertil.Add(new Fertilizer(id, name, min, max, limitTime, type, grade));
            }
            else
            { 
                specialFertil.Add(new Fertilizer(id, name, min, max, limitTime, type, grade));
            }
        }

        fertilizers[(int)FertType.Basic] = basicFertil;
        fertilizers[(int)FertType.Special] = specialFertil;
    }

    public List<Fertilizer> SetListType(FertType type)
    { return fertilizers[(int)type]; }
}