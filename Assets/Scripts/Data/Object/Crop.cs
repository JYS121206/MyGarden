using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crop
{
    public int id;
    public int reviseNum;
    public int num;
    public string name;
    public string about;
    public float growingSpeed;
    public int coin;
    public int weight;

    public int Numeric
    { set { num = (int)Mathf.Pow(value, reviseNum); } }  //나중에 미니게임을 추가한다면 사용...

    public Crop(int id, int reviseNum, int num, string name, string about, float growingSpeed, int coin, int weight)
    {
        this.id = id;
        this.reviseNum = id - reviseNum + 1;
        this.num = num;
        this.name = name;
        this.about = about;
        this.growingSpeed = growingSpeed;
        this.coin = coin;
        this.weight = weight;
    }
}