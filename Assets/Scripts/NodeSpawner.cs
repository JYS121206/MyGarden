using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeSpawner : MonoBehaviour
{
	[SerializeField] private GameObject nodePrefab;	
	[SerializeField] private RectTransform nodeRect;
	[SerializeField] private GridLayoutGroup gridLayoutGroup;

	public List<Node> SpawnNodes(Garden garden, Vector2Int nodeCount, float nodeSize)
	{
		// 노드 크기 설정
		gridLayoutGroup.cellSize = new Vector2(nodeSize, nodeSize);

		List<Node> nodeList = new List<Node>(nodeCount.x * nodeCount.y);

		for (int y = 0; y < nodeCount.y; ++y)
		{
			for (int x = 0; x < nodeCount.x; ++x)
			{
				GameObject clone = Instantiate(nodePrefab, nodeRect.transform);
				Vector2Int point = new Vector2Int(x, y);		  // 현재 노드의 x, y 격자 좌표 정보

																  // 인접 노드 정보 저장 (인접 노드가 없으면 null 저장)
                Vector2Int?[] neighborNodes = new Vector2Int?[4];
				Vector2Int right = point + Vector2Int.right;
				Vector2Int down = point + Vector2Int.up;
				Vector2Int left = point + Vector2Int.left;
				Vector2Int up = point + Vector2Int.down;

				if (IsValid(right, nodeCount)) neighborNodes[0] = right;
				if (IsValid(down, nodeCount)) neighborNodes[1] = down;
				if (IsValid(left, nodeCount)) neighborNodes[2] = left;
				if (IsValid(up, nodeCount)) neighborNodes[3] = up;

                Node node = clone.GetComponent<Node>();
				node.Setup(garden, neighborNodes, point);            // 노드 정보 세팅
                clone.name = $"[{node.Point.y}, {node.Point.x}]";
				nodeList.Add(node);									// 리스트에 노드 정보 저장
            }
		}

		return nodeList;
	}

    /// <summary> 범위를 벗어난 격자 좌표 체크 </summary>
    private bool IsValid(Vector2Int point, Vector2Int nodeCount)
	{
		if (point.x == -1 || point.x == nodeCount.x || point.y == nodeCount.y || point.y == -1)
		{ return false; }

		return true;
	}
}

