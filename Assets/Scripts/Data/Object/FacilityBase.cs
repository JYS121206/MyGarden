using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBase : MonoBehaviour
{
    protected float value; //������ �ɷ�ġ
    protected int level;   //����
    protected int cost;    //���� ���׷��̵� ���
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