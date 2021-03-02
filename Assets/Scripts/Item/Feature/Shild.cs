using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Shild : Feature
{
    public float m_EnergyMax=400;
    public float m_Energy=0;
    public float m_Scale = 4;
    GameObject m_ShildObj;
    
    public Shild()
    {
        EmiterResource = "Perfabs/Feature/EmiterShild";
        EffectResource = "Perfabs/Feature/Shild";

        m_ShildObj = GameObject.Instantiate(Resources.Load<GameObject>(EffectResource));

        ShildFunction shildFunction = m_ShildObj.GetComponent<ShildFunction>();
        shildFunction.m_Shild = this;
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
        float needEnergy = m_EnergyMax / 4;
        if (m_Energy == 0)
        {

            if (needEnergy > ship.m_ShipStruct.m_Electric)
                return;
            m_Energy += needEnergy;
            ship.m_ShipStruct.m_Electric -= needEnergy;
            return;
        }
        if(!m_ShildObj.activeSelf)
            m_ShildObj.SetActive(true);

        needEnergy = (m_EnergyMax - m_Energy) * Time.deltaTime;

        if (needEnergy > ship.m_ShipStruct.m_ElectricPower * Time.deltaTime)
            needEnergy = ship.m_ShipStruct.m_ElectricPower * Time.deltaTime;

        if (needEnergy > ship.m_ShipStruct.m_Electric)
            needEnergy = ship.m_ShipStruct.m_Electric;

        m_Energy += needEnergy;
        ship.m_ShipStruct.m_Electric = Math.Max(0, ship.m_ShipStruct.m_Electric - needEnergy);

    }
}