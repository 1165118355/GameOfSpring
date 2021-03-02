using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class AutoGunnery : Weapon
{
    public AutoGunnery()
    {
        WeaponShutRange = 16;
        WeaponShutVelocity = 1.7f;
        WeaponBulletFlyVelocity = 9;
        WeaponDamage = 20f;
        WeaponTurnSpeed = 90f;
        WeaponResource = "Perfabs/Weapon/Small/LightMachineGun";
        BulletResource = "Perfabs/Bullet/LiveBullet";
        m_FireAudioResouce = "Perfabs/Audio/Dang";
        m_Level = Level.LEVEL_MIDDLE;
    }
}