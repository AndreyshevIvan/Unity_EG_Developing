using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public AbstractPlayer m_player;
    public float m_criticalPosition = 18.5f;
    public float m_fallingSpeed = 1;

    bool m_isFreeze = false;

    private void Awake()
    {
    }

    public void Create()
    {
        CreateOptions();
    }
    protected virtual void CreateOptions() { }

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
            float offset = Time.deltaTime * m_fallingSpeed;
            Vector3 newPos = new Vector3(currPos.x, currPos.y, currPos.z - offset);

            gameObject.transform.position = newPos;
        }
    }

    public bool IsLive()
    {
        Vector3 currPos = gameObject.transform.position;

        return (currPos.z >= m_criticalPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
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
