using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 자동 급수 장치: 작물이 시드는 속도를 늦춰줌 </summary>
public class WaterWorks : FacilityBase
{
    public override void SetFacility(int _level)
    {
        if (_level > 10)
            _level = 10;

        Level = _level;

        value = 60 * (30 + (15 * Level));
        cost = (int)Mathf.Pow(2, Level + 8);
    }
}