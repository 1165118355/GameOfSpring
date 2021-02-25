using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ShipManager
{
    public int NumShips { get { return m_Ships.Count; } }

    static ShipManager m_Instance = new ShipManager();
    List<GameObject> m_Ships = new List<GameObject>();

    public static ShipManager get()
    {
        return m_Instance;
    }

    public GameObject GetShip(int index)
    {
        return m_Ships[index];
    }

    public void AddShip(GameObject shipObj)
    {
        m_Ships.Add(shipObj);
    }
    public void RemoveShip(GameObject shipObj)
    {
        m_Ships.Remove(shipObj);
    }

    public int FindShip(GameObject shipObj)
    {
        for(int i=0; i<m_Ships.Count; ++i)
        {
            if(m_Ships[i] == shipObj)
            {
                return i;
            }
        }
        return -1;
    }


}