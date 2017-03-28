using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    protected AbstractUser m_player;
    bool m_isFreeze = false;

    const float CRITICAL_POSITION = 18.5f;
    const float FALLING_SPEED = 3;

    private void Awake()
    {
    }
    public void Init(AbstractUser player)
    {
        m_player = player;
    }

    public void SetFreeze(bool isFreeze)
    {
        m_isFreeze = isFreeze;
    }

    private void FixedUpdate()
    {
        FallBonus();
    }
    void FallBonus()
    {
        if (!m_isFreeze)
        {
            Vector3 currPos = transform.position;
            float movement = Time.deltaTime * FALLING_SPEED;
            Vector3 newPos = new Vector3(currPos.x, currPos.y, currPos.z - movement);

            transform.position = newPos;
        }
    }

    public bool IsLive()
    {
        Vector3 currPos = transform.position;

        return (currPos.z >= CRITICAL_POSITION);
    }

    private void OnTriggerEnter(Collider other)
    {
        int lay = other.gameObject.layer;

        AddEffect();
        DestroyBonus();
    }
    protected virtual void AddEffect() { }

    public void DestroyBonus()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
