using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject m_ball;
    Rigidbody m_ballBody;

    public Vector3 m_startPosition;

    private void Awake()
    {
        m_ballBody = m_ball.GetComponent<Rigidbody>();
        Reset();
    }
    public void Reset()
    {
        m_ballBody.position = m_startPosition;
    }

}
