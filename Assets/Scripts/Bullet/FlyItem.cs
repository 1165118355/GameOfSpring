using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlyItem: MonoBehaviour
{
    public Vector3 FlyDirection = new Vector3(0, 1, 0);
    public float Life = 1;
    public float FlyVelocity = 1;
    public float Damage = 1;
    public string WeaponClassName = "none";
    public Weapon.DamageType WeaponType;
    public Weapon m_Weapon;
    public bool Death = true;
    public GameObject Ship {
        set { 
            m_Ship = value;
            m_Influence = value.gameObject.GetComponent<Ship>().m_ShipStruct.Influence; 
                }
        get { return m_Ship; }
            }
    public GameObject m_Ship;
    protected string m_Influence;
    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Death)
            return;
        if (Life < 0)
        {
            OnRangeMax();
            BulletPool.get().Recycle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;

        while (obj)
        {
            if (Ship == obj)
                return;
            var parentTransform = obj.transform.parent;
            if (!parentTransform)
                break;
            obj = parentTransform.gameObject;
        }
        obj = collision.gameObject;
        Ship ship = obj.GetComponent<Ship>();
        ShildFunction shild = obj.GetComponent<ShildFunction>();

        if (ship)
        {
            OnBoom();
            ship.Boom(Damage, WeaponType);
            BulletPool.get().Recycle(gameObject);
        }
        if (shild)
        {
            OnBoom();
            shild.Boom(Damage, WeaponType);
            BulletPool.get().Recycle(gameObject);
        }
    }
    protected virtual void OnBoom()
    {

    }
    protected virtual void OnRangeMax()
    {

    }
    public virtual void clean()
    {

    }
}