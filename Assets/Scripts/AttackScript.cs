﻿using UnityEngine;

public class AttackScript : MonoBehaviour {

    public int m_damage = 1;
	
    void Attack(GameObject target, int damage)
    {
        target.GetComponent<CharacterController2D>().m_livingCharacter.m_lifePoint -= m_damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (collision.contacts[0].collider.gameObject.tag == "Player")
            {
                //Only allow down/right/left collision 
                if (Vector2.Dot(contact.normal, Vector2.up) > 0.8f ||
                    Vector2.Dot(contact.normal, Vector2.left) > 0.8f ||
                    Vector2.Dot(contact.normal, Vector2.right) > 0.8f)
                {
                    Attack(collision.contacts[0].collider.gameObject, m_damage);
                    collision.transform.GetComponent<AudioSource>().PlayOneShot(collision.transform.GetComponent<CharacterController2D>().m_takeDammageSound);
                }
            }
        }
    }
}

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.transform.gameObject == m_player)
//        {
//            Attack(m_player, damage);
//        }
//    }
//}
