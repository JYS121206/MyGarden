using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ΰ� �¾� ����: ���� �ڶ�� ����  </summary>
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