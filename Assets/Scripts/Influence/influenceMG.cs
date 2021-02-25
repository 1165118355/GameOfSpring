using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InfluenceMG
{
    public static string INFLUENCE_PLAYER = "Player";
    public static string INFLUENCE_FIRE_STAR = "FireStarHuman";
    public static string INFLUENCE_EARTH = "EarthHuman";


    static InfluenceMG m_Instance = new InfluenceMG();
    List<Influence> m_Influences = new List<Influence>();
    public static InfluenceMG get()
    {
        return m_Instance;
    }

    public bool isHostile(string self, string other)
    {
        var selfInfluence = find(self);
        int hostileIndex = selfInfluence.findHostile(other);
        return hostileIndex >= 0;
    }

    public void init()
    {
        Influence player = new Influence(INFLUENCE_PLAYER);
        Influence fireStar = new Influence(INFLUENCE_FIRE_STAR);
        Influence earth = new Influence(INFLUENCE_EARTH)    ;
        add(player);
        add(fireStar);
        add(earth);
        player.addHostileInfluence(fireStar.Name);
        earth.addHostileInfluence(fireStar.Name);
        fireStar.addHostileInfluence(earth.Name);
        fireStar.addHostileInfluence(player.Name);
    }

    public void add(Influence influence)
    {
        m_Influences.Add(influence);
    }

    public Influence find(string influenceName)
    {
        for(int i=0; i< m_Influences.Count; ++i)
        {
            if(m_Influences[i].Name == influenceName)
            {
                return m_Influences[i];
            }
        }
        return null;
    }
}