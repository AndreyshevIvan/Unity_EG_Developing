using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : LivingBody
{
    protected AbstractUser m_player;
    public float m_criticalPosition = 18.5f;
    public float m_fallingSpeed = 3;
    bool m_isFreeze = false;

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
            Vector3 currPos = gameObject.transform.position;
            float movement = Time.deltaTime * m_fallingSpeed;
            Vector3 newPos = new Vector3(currPos.x, currPos.y, currPos.z - movement);

            gameObject.transform.position = newPos;
        }
    }

    public override bool IsLive()
    {
        Vector3 currPos = gameObject.transform.position;

        return (currPos.z >= m_criticalPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);

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
