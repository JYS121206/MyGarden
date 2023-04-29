using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HarvestCrop : MonoBehaviour
{
    private GraphicRaycaster ray;
    [SerializeField] private UIGame uIGame;

    private void Awake()
    {
        ray = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && uIGame.OnUI==false)
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            ray.Raycast(ped, results);

            if (results.Count <= 0) return;

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.tag == "FieldCrop")
                    results[i].gameObject.GetComponent<FieldCrop>().Harvest();
            }
        }
    }
}