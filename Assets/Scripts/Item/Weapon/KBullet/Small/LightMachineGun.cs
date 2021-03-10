using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LightMachineGun: Weapon
{
    public LightMachineGun()
    {
        ShutRange = 10;
        ShutVelocity = 0.1f;
        BulletFlyVelocity = 10;
        Damage = 3f;
        TurnSpeed = 140f;
        Name = "轻机枪";
        WeaponResource = "Perfabs/Weapon/Small/LightMachineGun";
        BulletResource = "Perfabs/Bullet/LiveBullet";
        m_FireAudioResouce = "Perfabs/Audio/Da";
    }
} 