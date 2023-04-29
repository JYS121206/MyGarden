using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 비료 가챠를 돌릴 수 있음 </summary>
public class RandomBox : FacilityBase
{
    public List<Item> fertilizers;

    private void Start()
    {
        fertilizers = ItemData.Instance.RandomBoxItems;
        maxLevel = 5;
    }

    public override void SetFacility(int _level)
    {
        if (_level > 10)
            _level = 10;

        Level = _level;

        cost = (int)Mathf.Pow(2, Level + 15);
    }

    public Item DrawFertilizer()
    {
        int drawNum = Random.Range(0, Level);
        return fertilizers[drawNum];
    }
}