using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LightMachineGun: Weapon
{
    public LightMachineGun()
    {
        WeaponShutRange = 10;
        WeaponShutVelocity = 0.1f;
        WeaponBulletFlyVelocity = 10;
        WeaponDamage = 3f;
        WeaponTurnSpeed = 140f;
        Name = "轻机枪";
        WeaponResource = "Perfabs/Weapon/Small/LightMachineGun";
        BulletResource = "Perfabs/Bullet/LiveBullet";
        m_FireAudioResouce = "Perfabs/Audio/Da";
    }
} 