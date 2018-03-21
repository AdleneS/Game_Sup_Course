using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public LivingCharacter m_livingCharacter;
    public float m_jumpHeight = 4.0f;
    public float m_speed = 2.0f;
    public int  m_damage = 1;

    public Rigidbody2D rb;
    public Vector3 m_footOffset;
    public LayerMask Mask;

    private Animator m_animator;
    private RaycastHit2D hit;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MyInput();
        OnAGround();
        CircleCast();
        m_animator.SetBool("IsGrounded", OnAGround());

    }

    void FixedUpdate()
    {
        IsAlive();
    }

    void IsAlive()
    {
        if (m_livingCharacter.m_lifePoint<=0)
        {
            DestroyImmediate(gameObject);
        }
    }
    void MyInput()
    {
        if (Input.GetButton("X_Axis"))
        {
            Movement(Input.GetAxis("X_Axis"));
            m_animator.SetFloat("VelocityX",Input.GetAxis("X_Axis"));
        }
        if (Input.GetButtonUp("X_Axis"))
        {
            Movement(0);
            m_animator.SetFloat("VelocityX", 0);
        }
        if (Input.GetButton("Jump") && OnAGround())
        {
            Jump();
        }

        if (Input.GetAxis("X_Axis") >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Movement(float axis)
    {
        rb.velocity = new Vector2((axis * m_speed), rb.velocity.y);
    }

    void CircleCast()
    {
        hit = Physics2D.CircleCast((transform.position - m_footOffset), 0.1f, new Vector2Int(0, 0), 0, Mask);
    }

    void Attack(GameObject target, int damage)
    {
        target.GetComponent<Enemy>().m_livingCharacter.m_lifePoint -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        bool kill = false;

        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5f && collision.contacts[0].collider.gameObject.tag == "Enemy")
            {
                kill = true;

                if (Input.GetButton("Jump") && kill)
                {
                    Jump();
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(rb.velocity.x, 3f), ForceMode2D.Impulse);
                }
                Attack(collision.contacts[0].collider.gameObject, m_damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((transform.position - m_footOffset), 0.1f);
    }

    bool OnAGround()
    {

        RaycastHit2D rh = hit;

        if (rh.collider != null)
        {
            if (rh.collider.tag == "Ground")
            {
                return true;
            }
        }
        return false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, m_jumpHeight), ForceMode2D.Impulse);
    }
}
