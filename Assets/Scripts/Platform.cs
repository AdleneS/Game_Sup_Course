using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour

{
    [Header("Platform Speed")]
    public float m_speed;

    [Header("Platform Type")]
    public PlatformType PT;

    public float m_bounceForce = 2;

    [Header("Platform Renderer")]
    public Sprite m_defaultPlatformSprite;
    public Sprite m_movablePlatformSprite;
    public Sprite m_bouncePlatformSprite;

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
        public bool m_bounce;

    }
    private void Start()
    {
        IN = GetComponent<InstantiateNodes>();
        rb2d = GetComponent<Rigidbody2D>();
        SpriteRenderer SR = GetComponent<SpriteRenderer>();

        if (PT.m_bounce)
        {
            SR.sprite = m_bouncePlatformSprite;
        }
        else if (PT.m_movable)
        {
            SR.sprite = m_movablePlatformSprite;
        }
        else
        {
            SR.sprite = m_defaultPlatformSprite;
        }
    }
    void FixedUpdate()
    {
        PlatformBehavior();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PT.m_bounce)
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                if (collision.contacts[0].collider.gameObject.tag == "Player")
                {
                    //Only allow down/right/left collision 
                    if (Vector2.Dot(contact.normal, Vector2.down) > 0.8f ||
                        Vector2.Dot(contact.normal, Vector2.left) > 0.8f ||
                        Vector2.Dot(contact.normal, Vector2.right) > 0.8f)
                    {
                        collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, m_bounceForce), ForceMode2D.Impulse);
                    }
                }
            }
        }
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
