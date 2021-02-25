using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Influence
{
    public string Name;
    List<string> HostileInfluence = new List<string>();

    public Influence(string name)
    {
        Name = name;
    }

    public void addHostileInfluence(string hostileInfluence)
    {
        if(findHostile(hostileInfluence) < 0)
        {
            HostileInfluence.Add(hostileInfluence);
        }
    }
    public void RemoveHostileInfluence(string hostileInfluence)
    {
        int index = findHostile(hostileInfluence);
        if (index >= 0)
        {
            HostileInfluence.RemoveAt(index);
        }
    }

    public int findHostile(string influence)
    {
        for(int i=0; i<HostileInfluence.Count; ++i)
        {
            if(influence == HostileInfluence[i])
            {
                return i;
            }
        }
        return -1;
    }
}