using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Pool
{
    protected Dictionary<string, List<GameObject>> m_Pool = new Dictionary<string, List<GameObject>>();

    public virtual void update()
    {

    }
}