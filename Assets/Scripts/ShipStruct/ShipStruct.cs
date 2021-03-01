using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ShipStruct
{
    public float StructurePoint {
        set { m_StructurePoint = Math.Min(value, StructurePointMax); }
        get { return m_StructurePoint; }
    }
    public float StructurePointMax{
        set { m_StructurePointMax = value; }
        get { return m_StructurePointMax; }
    }
    public string Influence {
        set { 
            m_Influence = value; 
            if (OnInFluenceChanged != null) 
                OnInFluenceChanged(m_Influence); 
        }
        get { return m_Influence; }
    }
    public float DetectRange
    {
        set { m_DetectRange = value; }
        get { return m_DetectRange; }
    }
    public GlobalDefine.OnStringCallback OnInFluenceChanged;
    public float m_StructurePoint = 1;
    public float m_StructurePointMax = 1;
    public string m_Influence="";
    public float m_DetectRange=20;
    public float m_ElectricMax=800;
    public float m_Electric= 800;
    public float m_ElectricIncrement=12;
    public float m_ElectricPower = 60;
}