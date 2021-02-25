using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Feature
{
    public string EmiterResource = "Perfabs/Feature/Small/LightMachineGun";
    public string EffectResource = "Perfabs/Bullet/LiveBullet";
    public GameObject m_Slot;

    public virtual void fire()
    {

    }
    public virtual void setParent(GameObject parent)
    {
        m_Slot = parent;
    }
}