using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    public FertilizerData fertilizerData;

    public Garden board;

    public Button btnBasicC;
    public Button btnBasicSS;
    public Button btnSpecialSS;

    private void Awake()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Garden>();

        fertilizerData = FertilizerData.Instance;
        board.fertilizer = fertilizerData.SetListType(FertType.Basic)[(int)FertGrade.C];
        btnBasicC.onClick.AddListener(() => SetBoardFert(FertType.Basic, FertGrade.C));
        btnBasicSS.onClick.AddListener(() => SetBoardFert(FertType.Basic, FertGrade.SS));
        btnSpecialSS.onClick.AddListener(() => SetBoardFert(FertType.Special, FertGrade.SS));

    }

    public void SetBoardFert(FertType type, FertGrade grade)
    {
        board.fertilizer = fertilizerData.SetListType(type)[(int)grade];
        board.usefertilTime = true;

        GameData.Instance.Coin += 1000000;
    }
}