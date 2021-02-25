using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSphere : MonoBehaviour
{
    public float RotationSelfVelocity = 1;
    public float RotationCenterVelocity = 1;
    public GameObject ParentSphere;
    public GameObject Model;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        float ifps = Time.fixedDeltaTime;
        if (Model)
        {
            transform.rotation *= Quaternion.AngleAxis(RotationSelfVelocity * ifps, new Vector3(0, 1, 1));
        }

        if (ParentSphere)
        {
            transform.RotateAround(ParentSphere.transform.position, new Vector3(0, 0, 1), RotationCenterVelocity *ifps);
        }
    }
}
