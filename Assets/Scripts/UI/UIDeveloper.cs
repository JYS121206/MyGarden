using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeveloper : MonoBehaviour
{

    [SerializeField] protected Text txtLevel;
    [SerializeField] protected Text txtCost;
    [SerializeField] protected Button btnUpgrade;
    [SerializeField] protected FacilityController facilityController;

    [SerializeField] protected FacilityBase facility;

    void Start() {  Setup(); }

    public virtual void Setup()
    {
        facility = gameObject.GetComponentInChildren<FacilityBase>();
        txtLevel = gameObject.GetComponentsInChildren<Text>()[0];
        txtCost = gameObject.GetComponentsInChildren<Text>()[1];
        btnUpgrade = gameObject.GetComponentInChildren<Button>();
        btnUpgrade.onClick.AddListener(UpgradeFacility);
        txtCost.text = $"x {facility.Cost}";
    }

    public virtual void UpgradeFacility()
    {
        if (facilityController.LevelUp(facility))
        {
            txtLevel.text = $"Level . {facility.Level}";
            txtCost.text = $"x {facility.Cost}";
        }
        else
        {
            if (facility.Level == facility.MaxLevel)
            {
                txtLevel.text = $"Level . {facility.Level}";
                txtCost.text = $" --";
                btnUpgrade.gameObject.GetComponentInChildren<Text>().text = "Max Level";
            }
        }
    }
}