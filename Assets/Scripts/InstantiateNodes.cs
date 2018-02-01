using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNodes : MonoBehaviour
{
    public Node m_node;

    [HideInInspector]
    public List<Transform> m_wayPoints;

    private void Start()
    {
        if(m_wayPoints != null)
        {
            foreach (Transform item in m_wayPoints)
            {
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void GenerateNodes()
    {
        m_wayPoints = new List<Transform>();       

        if (GameObject.Find(transform.name + "_Node"))
        {
            DestroyImmediate(GameObject.Find(transform.name + "_Node"));
        }
       
        if (GameObject.Find("MyNodes") != null)
        {
            if (m_node._nodeNumber > 0)
            {
                Transform nodesHolder = new GameObject(transform.name + "_Node").transform;
                nodesHolder.parent = GameObject.Find("MyNodes").transform;

                for (int i = 0; i < m_node._nodeNumber; i++)
                {
                    Transform tempNodes = Instantiate(m_node.prefab, transform.position, Quaternion.identity);
                    tempNodes.name = "Node_0" + i;
                    tempNodes.parent = nodesHolder;

                    m_wayPoints.Add(tempNodes);
                }
            }
        }
        else
        {
            Transform MyNodes = new GameObject("MyNodes").transform;
            //Debug.LogError("Il manque un objet 'MyNodes' dans la scène");
        }       
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_wayPoints.Count-1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_wayPoints[i].transform.position, m_wayPoints[i + 1].transform.position);
        }
    }
}
