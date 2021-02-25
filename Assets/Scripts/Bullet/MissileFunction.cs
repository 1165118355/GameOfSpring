using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFunction : FlyItem
{
    bool m_Trace = true;
    GameObject m_Target;
    void Start()
    {
        
    }

    public override void Update()
    {
        base.Update();
        if (Death)
            return;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, FlyDirection);
        var moveOffset = FlyDirection * (FlyVelocity * Time.deltaTime);
        Life -= moveOffset.magnitude;
        transform.position += moveOffset;

        if(m_Target)
        {
            Missile missile = m_Weapon as Missile;
            var targetDefference = m_Target.transform.position - transform.position;
            var d = Vector3.Cross(targetDefference, FlyDirection).z;
            if(d >= 0)
            {
                FlyDirection = Quaternion.AngleAxis(missile.m_TrunVelocity * Time.deltaTime, new Vector3(0, 0, -1)) * FlyDirection;
            }
            else
            {
                FlyDirection = Quaternion.AngleAxis(missile.m_TrunVelocity * Time.deltaTime, new Vector3(0, 0, 1)) * FlyDirection;
            }
        }
    }

    public void FixedUpdate()
    {
        if (m_Trace && !m_Target && null != m_Influence)
        {
            float distance = 1000000000000;
            for (int i = 0; i < ShipManager.get().NumShips; ++i)
            {
                var ship = ShipManager.get().GetShip(i);
                var shipCom = ship.GetComponent<Ship>();
                if(InfluenceMG.get().isHostile(m_Influence, shipCom.m_ShipStruct.Influence))
                {
                    var defference = ship.transform.position - transform.position;
                    if(defference.magnitude < distance)
                    {
                        distance = defference.magnitude;
                        m_Target = ship;
                    }
                }
            }
        }
    }
    public override void clean()
    {
        base.clean();
        m_Target = null;
    }

    protected override void OnBoom()
    {
        Object obj = Resources.Load<GameObject>("Perfabs/Effect/Bo");
        GameObject boomObj = Instantiate(obj) as GameObject;
        boomObj.transform.position = gameObject.transform.position;
        Destroy(boomObj, 3);
        base.OnBoom();
    }
    protected override void OnRangeMax()
    {
        OnBoom();
        base.OnRangeMax();
    }
}
