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

        //���� ���� ����
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
        //���� ���� ���׷��̵�

        int lev = facility.Level;

        if (lev == facility.MaxLevel) //�����̸� ���׷��̵� �Ұ���
        { return false; }

        if (GameData.Instance.Coin >= facility.Cost)  //������ �˻� �� ���׷��̵�
        {
            GameData.Instance.Coin -= facility.Cost;
            lev++;
            facility.SetFacility(lev);

            if (facility == sunLight) { garden.growingSpeed = facility.Value; }
            else if (facility == waterWorks) { garden.wiltTime = facility.Value; }
            else if (facility == thermostat) { garden.rareChance = facility.Value; }

            if (lev == facility.MaxLevel) { return false; }  //���� �޼��� UI ������Ʈ�� ���� false ����

            return true;
        }
        else
        {
            print("�� ����");
            return false;
        }
    }
}