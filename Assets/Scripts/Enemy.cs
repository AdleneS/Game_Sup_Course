using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Speed")]
    public float m_speed;

    [Header("Enemy Type")]
    public EnemyType ET;

    [Header("Enemy Life Settings")]
    public LivingCharacter m_livingCharacter;


    [Header("Bullet")]
    public GameObject m_bulletPrefab;
    private Bullet m_bullet;

    private int m_curWayPoint;
    private Rigidbody2D rb2d;
    private Vector3 m_target;
    private Vector3 m_moveDirection;
    private Vector3 m_velocity;
    private InstantiateNodes IN;

    [System.Serializable]
    public class EnemyType
    {
        public bool m_static;
        public bool m_patroller;
        public bool m_shooter;
    }
    private void Start()
    {
        IN = GetComponent<InstantiateNodes>();
        rb2d = GetComponent<Rigidbody2D>();
        m_bullet = m_bulletPrefab.GetComponent<Bullet>();
        if (ET.m_shooter)
        {
            StartCoroutine(Fire());
        }
    }
    void FixedUpdate()
    {
        EnemyBehavior();
        Death();
    }

    void EnemyBehavior()
    {
        if (ET.m_static)
        {
            return;
        }
        if (ET.m_patroller)
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
                    m_velocity = m_moveDirection.normalized * m_speed;
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
        rb2d.velocity = m_velocity;
    }

    void Shooter()
    {
        GameObject tempBullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().velocity =  new Vector2(m_bullet.m_bulletSpeed, 0);

        Destroy(tempBullet, m_bullet.m_bulletLife);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Shooter();
            yield return new WaitForSeconds(m_bullet.m_bulltetFrequency);
        }
    }

    void Death()
    {
        if (m_livingCharacter.m_lifePoint<=0)
        {
            if (GameObject.Find(transform.name + "_Node"))
            {
                DestroyImmediate(GameObject.Find(transform.name + "_Node"));
            }
            DestroyImmediate(gameObject);
        }
    }
}
