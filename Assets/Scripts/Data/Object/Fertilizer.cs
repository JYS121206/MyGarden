using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FertType
{
    Basic,
    Special,
    MaxCount
}
public enum FertGrade
{
    C,
    B,
    A,
    S,
    SS
}

[System.Serializable]
public class Fertilizer
{
    public int id;
    public string name;
    public int min;
    public int max;
    public float limitTime;
    public FertType type;
    public FertGrade grade;

    public Fertilizer(int id, string name, int min, int max, float limitTime, FertType type, FertGrade grade)
    {
        this.id = id;
        this.name = name;
        this.min = min;
        this.max = max;
        this.limitTime = limitTime;
        this.type = type;
        this.grade = grade;
    }
}