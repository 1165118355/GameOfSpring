using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public float FirePower = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float currentTime = 0;
    public float findTargetTime = 1;
        void Update()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime < findTargetTime)
            return;
        currentTime = 0;
        findTargetTime = Random.Range(0.2f, 0.5f);
        setParticalScale(gameObject);
    }

    void setParticalScale(GameObject obj)
    {
        var partical = obj.GetComponent<ParticleSystem>();
        if (partical != null)
        {
            var particalMain = partical.main;
            particalMain.startLifetimeMultiplier = FirePower;
        }
        for (int i = 0; i < obj.transform.childCount; ++i)
        {
            var child = obj.transform.GetChild(i);
            setParticalScale(child.gameObject);
        }
    }
}
