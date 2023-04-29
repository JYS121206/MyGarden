using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public FieldCrop placedCrop;
    public Vector2 localPosition;
    public bool isGrowing = false;

    public Vector2Int Point { private set; get; }               // ���� ����� x, y ���� ��ǥ ���� (�»���� 0, 0)
    public Vector2Int?[] NeighborNodes { private set; get; }    // ���� ��忡 ������ ����� ���� ��ǥ (������ null)

    private Garden garden;

    public void Setup(Garden garden, Vector2Int?[] neighborNodes, Vector2Int point)
    {
        this.garden = garden;
        NeighborNodes = neighborNodes;
        Point = point;
    }

    public Node[] FindTarget(Node originalNode, Node[] farNode = null)
    {
        //////// ������ ���� ���� ���� �� ////////
        return farNode;
    }
}