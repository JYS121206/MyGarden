using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 인공 태양 조명: 빨리 자라게 해줌  </summary>
public class SunLight : FacilityBase
{
    public override void SetFacility(int _level)
    {
        if (_level > 10)
            _level = 10;

        Level = _level;

        value = 1 - (0.05f * Level);
        cost = (int)Mathf.Pow(2, Level + 8);
    }
}