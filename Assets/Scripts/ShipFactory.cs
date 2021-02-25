using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
    public string m_ProductionInfluence;
    public GameObject m_ProductioMachineType;
    public GameObject m_ProductioHeadquarters;
    public GameObject m_ProductionPoint;
    public GameObject m_Target;
    public float m_ProductionFrequency = 7;

    float m_ProductionFrequencyTimeCount=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_ProductionFrequencyTimeCount += Time.deltaTime;
        if (m_ProductionFrequencyTimeCount < m_ProductionFrequency)
            return;
        m_ProductionFrequencyTimeCount = 0;

        //  生产一个飞机
        GameObject ship = Instantiate(m_ProductioMachineType) as GameObject;
        GameObject headquarters = Instantiate(m_ProductioHeadquarters) as GameObject;
        ship.transform.SetParent(headquarters.transform);

        var shipCom = ship.GetComponent<Ship>();
        var headquartersCom = headquarters.GetComponent<Headquarters>();

        shipCom.m_ShipStruct.Influence = m_ProductionInfluence;
        headquartersCom.Target = m_Target;
        if (m_ProductionPoint)
            headquarters.transform.position = m_ProductionPoint.transform.position;
    }
}
