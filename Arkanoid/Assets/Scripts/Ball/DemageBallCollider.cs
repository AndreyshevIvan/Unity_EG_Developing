using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemageBallCollider : MonoBehaviour
{
    int m_basicDemage;
    int m_fireDemage;

    bool m_isFireMode = false;

    public void Init(int basicDemage, int fireDemage)
    {
        m_basicDemage = basicDemage;
        m_fireDemage = fireDemage;
    }

    public void SetFireMode(bool isFireModeOn)
    {
        m_isFireMode = isFireModeOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        LivingBody body = other.gameObject.GetComponent<LivingBody>();

        if (body != null)
        {
            int demage = (m_isFireMode) ? m_fireDemage : m_basicDemage;

            body.AddDemage(demage);
        }
    }
}
