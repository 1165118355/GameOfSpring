using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Shild : Feature
{
    public float m_EnergyMax=400;
    public float m_Energy=400;
    public float m_Scale = 4;
    GameObject m_ShildObj;
    
    public Shild()
    {
        EmiterResource = "Perfabs/Feature/EmiterShild ";
        EffectResource = "Perfabs/Feature/Shild";

        m_ShildObj = GameObject.Instantiate(Resources.Load<GameObject>(EffectResource));
    }
    public override void setParent(GameObject parent)
    {
        base.setParent(parent);
        m_ShildObj.transform.SetParent(m_Slot.transform, false);
        var parentScale = m_Slot.transform.localScale;
        m_ShildObj.transform.localScale = new Vector3(1/ parentScale.x * m_Scale, 1 / parentScale.y * m_Scale, 1 / parentScale.z * m_Scale);
    }

    public override void fire()
    {
        base.fire();
        if (m_ShildObj == null)
            return;


        var shipObject = m_Slot.transform.parent;
        Ship ship = shipObject.GetComponent<Ship>();
        var needEnergy = m_EnergyMax - m_Energy;
        if(ship.m_ShipStruct.m_Power >= m_EnergyMax/4)
        {
            m_ShildObj.SetActive(true);
            ShildFunction shildFunction = m_ShildObj.GetComponent<ShildFunction>();
            shildFunction.m_Shild = this;

            m_Energy += needEnergy + Math.Min(ship.m_ShipStruct.m_Power - needEnergy, 0);
            ship.m_ShipStruct.m_Power = Math.Max(0, ship.m_ShipStruct.m_Power - needEnergy);
        }

    }
}