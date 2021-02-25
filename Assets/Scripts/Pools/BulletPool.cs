using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BulletPool: Pool
{
    static BulletPool m_Instance = new BulletPool();

    GameObject m_BulletPoolObj;

    public static BulletPool get()
    {
        return m_Instance;
    }

    public void init()
    {
        m_BulletPoolObj = new GameObject();
        m_BulletPoolObj.name = "BulletPools";
    }

    public void Recycle(GameObject bulletObject)
    {
        FlyItem bullet = bulletObject.GetComponent<FlyItem>();
        if (null == bullet)
            return;


        if (!m_Pool.ContainsKey(bullet.WeaponClassName))
            m_Pool.Add(bullet.WeaponClassName, new List<GameObject>());

        m_Pool[bullet.WeaponClassName].Add(bulletObject);
        bullet.Death = true;
        bulletObject.transform.position = new Vector3(0, 100000000, 0);
    }

    public GameObject Take(Weapon weapon)
    {
        string weaponType = weapon.GetType().Name;

        if (m_Pool.ContainsKey(weaponType))
        {
            var bulletPool = m_Pool[weaponType];
            if (bulletPool.Count > 0)
            {
                var retValue = bulletPool[bulletPool.Count - 1];
                bulletPool.RemoveAt(bulletPool.Count - 1);
                retValue.GetComponent<FlyItem>().Death = false;
                return retValue;
            }
        }

        UnityEngine.Object obj = Resources.Load<GameObject>(weapon.BulletResource);
        var bullet = UnityEngine.Object.Instantiate(obj) as GameObject;
        var bulletComponent = bullet.GetComponent<FlyItem>();
        bulletComponent.Death = false;
        bullet.transform.SetParent(m_BulletPoolObj.transform);
        return bullet;
    }
}