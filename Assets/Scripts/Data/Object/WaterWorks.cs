using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ڵ� �޼� ��ġ: �۹��� �õ�� �ӵ��� ������ </summary>
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