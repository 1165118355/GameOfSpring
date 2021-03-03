using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MissileStandard01: Missile
{
    public MissileStandard01()
    {
        m_Type = DamageType.TYPE_EXPLOSION;
        WeaponShutRange = 15;
        m_TrunVelocity = 60;
        WeaponShutVelocity = 6.4f;
        WeaponBulletFlyVelocity = 4;
        WeaponDamage = 72f;
        Name = "标准-1";
        WeaponResource = "Perfabs/Weapon/Small/MissileStandard";
        BulletResource = "Perfabs/Bullet/MissileStandard01";
        m_FireAudioResouce = "Perfabs/Audio/PuHu";
    }
}