using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureSlot : Slot
{
    // Start is called before the first frame update
    //public float EffectRange = 10;
    //public float EffectAngle = 10;
    Feature m_Feature;
    void Start()
    {
        m_Feature = new Shild();
        switchFeature(m_Feature);
        m_Feature.setParent(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void fire()
    {
        if (null == m_Feature)
            return;

        m_Feature.fire();

    }
    void switchFeature(Feature feature)
    {
        m_Feature = feature;
        EmiterObject = Instantiate(Resources.Load<GameObject>(m_Feature.EmiterResource)) as GameObject;
        EmiterObject.transform.SetParent(transform, false);
    }
}
