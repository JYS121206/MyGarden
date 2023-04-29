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
		// ��� ũ�� ����
		gridLayoutGroup.cellSize = new Vector2(nodeSize, nodeSize);

		List<Node> nodeList = new List<Node>(nodeCount.x * nodeCount.y);

		for (int y = 0; y < nodeCount.y; ++y)
		{
			for (int x = 0; x < nodeCount.x; ++x)
			{
				GameObject clone = Instantiate(nodePrefab, nodeRect.transform);
				Vector2Int point = new Vector2Int(x, y);		  // ���� ����� x, y ���� ��ǥ ����

																  // ���� ��� ���� ���� (���� ��尡 ������ null ����)
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
				node.Setup(garden, neighborNodes, point);            // ��� ���� ����
                clone.name = $"[{node.Point.y}, {node.Point.x}]";
				nodeList.Add(node);									// ����Ʈ�� ��� ���� ����
            }
		}

		return nodeList;
	}

    /// <summary> ������ ��� ���� ��ǥ üũ </summary>
    private bool IsValid(Vector2Int point, Vector2Int nodeCount)
	{
		if (point.x == -1 || point.x == nodeCount.x || point.y == nodeCount.y || point.y == -1)
		{ return false; }

		return true;
	}
}

