using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public CharacterController2D CC2D;
    public Transform m_spawnPoint;

    private void Update()
    {
        if (!CC2D.IsAlive())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
