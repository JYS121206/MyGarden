using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public FieldCrop placedCrop;
    public Vector2 localPosition;
    public bool isGrowing = false;

    public Vector2Int Point { private set; get; }               // 현재 노드의 x, y 격자 좌표 정보 (좌상단이 0, 0)
    public Vector2Int?[] NeighborNodes { private set; get; }    // 현재 노드에 인접한 노드의 격자 좌표 (없으면 null)

    private Garden garden;

    public void Setup(Garden garden, Vector2Int?[] neighborNodes, Vector2Int point)
    {
        this.garden = garden;
        NeighborNodes = neighborNodes;
        Point = point;
    }

    public Node[] FindTarget(Node originalNode, Node[] farNode = null)
    {
        //////// 교란종 관련 로직 구상 중 ////////
        return farNode;
    }
}