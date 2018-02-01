using UnityEngine;

public class Camera2D : MonoBehaviour {

    public Transform target;

    public float smoothSpeed =5.0f;
    public Vector3 offset;


    void FixedUpdate()
    {
        if(target !=null)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, DesiredPosition(), smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    Vector3 DesiredPosition()
    {
        Vector3 m_desiredPosition = new Vector3(target.position.x, offset.y, offset.z);

        if (target.transform.position.y > 2.5f)
        {
            m_desiredPosition = new Vector3(target.position.x, target.position.y, offset.z);
        }

        if (target.transform.position.y < 0.5f)
        {
            m_desiredPosition = new Vector3(target.position.x, target.position.y, offset.z);
        }

        return m_desiredPosition;
    }
}
