using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class AutoGunnery : Weapon
{
    float m_ContinueFireTime = 0.1f;
    float m_ContinueFireTimeCount = 0;
    int m_MultiFire = 0;
    GameObject m_MultiFireShip;
    Vector3 m_MultiFirePosition;
    Vector3 m_MultiFireDirection;
    public AutoGunnery()
    {
        ShutRange = 16;
        ShutVelocity = 1.7f;
        BulletFlyVelocity = 9;
        Damage = 20f;
        TurnSpeed = 90f;
        Name = "重型自动炮";
        WeaponResource = "Perfabs/Weapon/Small/AutoGunnery";
        BulletResource = "Perfabs/Bullet/LiveBullet";
        m_FireAudioResouce = "Perfabs/Audio/Dang";
        m_Level = Level.LEVEL_MIDDLE;

    }
    public override void Fire(GameObject ship, Vector3 position, Vector3 direction)
    {
        base.Fire(ship, position, direction);
        m_MultiFireShip = ship;
        m_MultiFirePosition = position;
        m_MultiFireDirection = direction;
        m_MultiFire = 1;
    }


    public override int update()
    {
        base.update();

        if(m_MultiFire > 0)
        {
            m_ContinueFireTimeCount += Time.deltaTime;
            if(m_ContinueFireTimeCount >= m_ContinueFireTime)
            {
                m_MultiFire--;
                m_ContinueFireTimeCount = 0;
                base.Fire(m_MultiFireShip, m_MultiFirePosition, m_MultiFireDirection);
            }
        }
        return 1;
    }

}