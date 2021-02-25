using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildFunction : MonoBehaviour
{
    public Shild m_Shild;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        if (null == render)
            return;
        var color = render.color;
        color.a = m_Shild.m_Energy / m_Shild.m_EnergyMax;
        render.color = color;
    }
    public void Boom(float damage, Weapon.DamageType type)
    {
        switch (type)
        {
            case Weapon.DamageType.TYPE_EXPLOSION:
                damage /= 2;
                break;
            case Weapon.DamageType.TYPE_KINETIC:
                damage *= 2;
                break;
        }
        m_Shild.m_Energy = Mathf.Max(0, m_Shild.m_Energy - damage);

        if (m_Shild.m_Energy <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
