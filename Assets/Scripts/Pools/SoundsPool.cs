using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SoundsPool : Pool
{
    static SoundsPool m_Instance = new SoundsPool();
    GameObject m_SoundPoolObj;
    public static SoundsPool get()
    {
        return m_Instance;
    }
    public void init()
    {
        m_SoundPoolObj = new GameObject();
        m_SoundPoolObj.name = "SoundPool";
    }
    public override void update()
    {
        base.update();

    }

    public void Recycle(GameObject bulletObject)
    {
        SoundFunction soundF = bulletObject.GetComponent<SoundFunction>();
        if (null == soundF)
            return;


        if (!m_Pool.ContainsKey(soundF.m_SoundPath))
            m_Pool.Add(soundF.m_SoundPath, new List<GameObject>());

        m_Pool[soundF.m_SoundPath].Add(bulletObject);
        bulletObject.transform.position = new Vector3(0, 100000000, 0);
    }

    public GameObject Take(string soundName)
    {
        if (m_Pool.ContainsKey(soundName))
        {
            var soundPool = m_Pool[soundName];
            if (soundPool.Count > 0)
            {
                var retValue = soundPool[soundPool.Count - 1];
                soundPool.RemoveAt(soundPool.Count - 1);
                return retValue;
            }
        }

        UnityEngine.Object obj = Resources.Load<GameObject>(soundName);
        if(obj)
        {
            var sound = UnityEngine.Object.Instantiate(obj) as GameObject;
            sound.transform.SetParent(m_SoundPoolObj.transform);

            var soundCom = sound.GetComponent<SoundFunction>();
            soundCom.m_SoundPath = soundName;
            return sound;
        }
        else
        {
            Debug.Log("not found sound file = "+ soundName);
            return null;
        }
    }
}