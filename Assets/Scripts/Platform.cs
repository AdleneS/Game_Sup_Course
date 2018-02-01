using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour

{
    [Header("Platform Speed")]
    public float m_speed;

    [Header("Platform Type")]
    public PlatformType PT;

    private int m_curWayPoint;
    private Rigidbody2D rb2d;
    private Vector3 m_target;
    private Vector3 m_moveDirection;
    private Vector3 m_velocity;
    private InstantiateNodes IN;

    [System.Serializable]
    public class PlatformType
    {
        public bool m_static;
        public bool m_movable;
    }
    private void Start()
    {
        IN = GetComponent<InstantiateNodes>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        PlatformBehavior();
    }

    void PlatformBehavior()
    {
        if (PT.m_static)
        {
            return;
        }
        if (PT.m_movable)
        {
            if (m_curWayPoint < IN.m_wayPoints.Count)
            {
                m_target = IN.m_wayPoints[m_curWayPoint].position;
                m_moveDirection = m_target - transform.position;
                m_velocity = rb2d.velocity;

                if (m_moveDirection.magnitude < 0.2f)
                {
                    m_curWayPoint++;
                }
                else
                {
                    rb2d.MovePosition(transform.position + m_moveDirection.normalized * Time.deltaTime);
                }
            }
            else
            {
                m_curWayPoint = 0;
            }
        }
        else
        {
            m_velocity = Vector2.zero;
        }
        rb2d.MovePosition(transform.position+m_moveDirection.normalized * Time.deltaTime);

    }
}
