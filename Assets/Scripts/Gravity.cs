using UnityEngine;

public class Gravity : MonoBehaviour {


    public float m_gravity = -9.81f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PGravity();
    }

    void PGravity()
    {
        rb.AddForce(new Vector2(0, m_gravity * Time.deltaTime), ForceMode2D.Impulse);
    }
}
