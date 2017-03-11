using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemageBallCollider : MonoBehaviour
{
    public int m_basicDemage;
    public int m_fireDemage;

    bool m_isFireMode = false;

    int m_collideNum = 0;

    public void SetFireMode(bool isFireModeOn)
    {
        m_isFireMode = isFireModeOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_collideNum++;

        LivingBody body = other.gameObject.GetComponent<LivingBody>();

        if (body != null)
        {
            int demage = (m_isFireMode) ? m_fireDemage : m_basicDemage;
            body.AddDemage(demage);
        }
    }

    public int GetFireDemage()
    {
        return m_fireDemage;
    }
}
