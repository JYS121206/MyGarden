using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 난방 장치: 레어 확률 올라감 </summary>
public class Thermostat : FacilityBase
{
    public override void SetFacility(int _level)
    {
        if (_level > 10)
            _level = 10;

        Level = _level;

        value = 5 * Level;
        cost = (int)Mathf.Pow(2, Level + 12);
    }
}