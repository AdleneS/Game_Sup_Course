using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_bulletSpeed = 1.0f;
    public int m_bulletDamage = 1;
    public float m_bulletLife = 2.0f;
    public float m_bulltetFrequency = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<CharacterController2D>().m_livingCharacter.m_lifePoint -= m_bulletDamage;
            Destroy(transform.gameObject);
        }
    }
}

