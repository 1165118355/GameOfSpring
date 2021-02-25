using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headquarters : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        if (!Target)
            return;
        
        for(int i=0; i<transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            var driver = child.GetComponent<Driver>();

            if (!driver)
                continue;
            if (driver.m_CurrentState == Driver.State.STATE_STANDBY)
            {
                driver.m_CurrentState = Driver.State.STATE_MOVE;
                driver.Target = Target;
            }

        }
    }
}
