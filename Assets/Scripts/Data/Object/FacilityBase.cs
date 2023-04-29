using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBase : MonoBehaviour
{
    protected float value; //설비의 능력치
    protected int level;   //레벨
    protected int cost;    //레벨 업그레이드 비용
    protected int maxLevel = 10;

    public float Value => value;
    public int Cost => cost;
    public int MaxLevel => maxLevel;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public virtual void SetFacility(int _level) { }
}