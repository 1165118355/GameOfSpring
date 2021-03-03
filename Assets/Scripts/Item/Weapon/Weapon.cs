using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon: Item
{
    public enum DamageType
    {
        TYPE_ENERGY,
        TYPE_KINETIC,
        TYPE_EXPLOSION
    }
    public enum Level
    {
        LEVEL_SMALL,
        LEVEL_MIDDLE,
        LEVEL_BIG,
    }
    public DamageType m_Type = DamageType.TYPE_KINETIC;
    public string Name = "none";
    public float  WeaponShutRange = 10;
    public float  WeaponShutVelocity = 0.2f;
    public float  WeaponBulletFlyVelocity = 5f;
    public float  WeaponTurnSpeed =60f;
    public float  WeaponDamage = 1f;
    public float  m_ShotErrorValue = 0;
    public float  m_ShotErrorValueMax = 15;
    public float  m_ShotErrorValueIncrement = 0.6f;
    public string WeaponResource = "Perfabs/Weapon/Small/LightMachineGun";
    public string BulletResource = "Perfabs/Bullet/LiveBullet";
    public string m_FireAudioResouce = "Perfabs/Audio/Pa";
    public Level  m_Level = Level.LEVEL_SMALL;


    public virtual void Fire(GameObject ship, Vector3 position, Vector3 direction)
    {
        var bullet = BulletPool.get().Take(this);
        var bulletComponent = bullet.GetComponent<FlyItem>();

        if (null == bulletComponent)
            return;
        m_ShotErrorValue = Math.Min(m_ShotErrorValueMax, m_ShotErrorValue + m_ShotErrorValueIncrement);
        float errorValueHalf = m_ShotErrorValue / 2;
        float offsetValue = UnityEngine.Random.Range(-errorValueHalf, errorValueHalf);
        bulletComponent.clean();
        bulletComponent.FlyDirection = Quaternion.AngleAxis(offsetValue, Vector3.forward) * direction;
        bulletComponent.transform.position = position;
        bulletComponent.FlyVelocity = WeaponBulletFlyVelocity;
        bulletComponent.Life = WeaponShutRange;
        bulletComponent.WeaponClassName = GetType().Name;
        bulletComponent.WeaponType = m_Type;
        bulletComponent.Ship = ship;
        bulletComponent.Damage = WeaponDamage;
        bulletComponent.m_Weapon = this;
        var soundObj = SoundsPool.get().Take(m_FireAudioResouce);
        if (null == soundObj)
            return;
        soundObj.transform.position = position;
        AudioSource soundCom = soundObj.GetComponent<AudioSource>();
        if (null == soundCom)
            return;

        Ship shipCom = ship.GetComponent<Ship>();
        if (InfluenceMG.INFLUENCE_PLAYER == shipCom.m_ShipStruct.Influence)
        {
            soundCom.priority = 10;
        }
        else
        {
            soundCom.priority = 100;
        }
        soundCom.Play();
    }

    public virtual int update()
    {
        return 1;
    }

}