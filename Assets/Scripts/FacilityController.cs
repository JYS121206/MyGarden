using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityController : MonoBehaviour
{
    [SerializeField] private SunLight sunLight;
    [SerializeField] private WaterWorks waterWorks;
    [SerializeField] private Thermostat thermostat;
    [SerializeField] private RandomBox randomBox;

    public Garden garden;

    private void Awake()
    {
        garden = GameObject.FindWithTag("Garden").GetComponent<Garden>();

        //시작 레벨 세팅
        sunLight.SetFacility(1);
        waterWorks.SetFacility(1);
        thermostat.SetFacility(1);
        randomBox.SetFacility(1);

        garden.growingSpeed = sunLight.Value;
        garden.wiltTime = waterWorks.Value;
        garden.rareChance = thermostat.Value;
    }

    public bool LevelUp(FacilityBase facility)
    {
        //설비 레벨 업그레이드

        int lev = facility.Level;

        if (lev == facility.MaxLevel) //만렙이면 업그레이드 불가능
        { return false; }

        if (GameData.Instance.Coin >= facility.Cost)  //소지금 검사 후 업그레이드
        {
            GameData.Instance.Coin -= facility.Cost;
            lev++;
            facility.SetFacility(lev);

            if (facility == sunLight) { garden.growingSpeed = facility.Value; }
            else if (facility == waterWorks) { garden.wiltTime = facility.Value; }
            else if (facility == thermostat) { garden.rareChance = facility.Value; }

            if (lev == facility.MaxLevel) { return false; }  //만렙 달성시 UI 업데이트를 위해 false 리턴

            return true;
        }
        else
        {
            print("돈 없음");
            return false;
        }
    }
}