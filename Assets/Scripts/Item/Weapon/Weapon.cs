using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Weapon: Item
{
    public enum DamageType
    {
        TYPE_ENERGY,
        TYPE_KINETIC,
        TYPE_EXPLOSION
    }
    public DamageType m_Type = DamageType.TYPE_KINETIC;
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


}