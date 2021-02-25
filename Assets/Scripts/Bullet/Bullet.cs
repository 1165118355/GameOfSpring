using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FlyItem
{
    void Start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Death)
            return;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, FlyDirection);
        var moveOffset = FlyDirection * (FlyVelocity * Time.deltaTime);
        Life -= moveOffset.magnitude;
        transform.position += moveOffset;
    }

}
