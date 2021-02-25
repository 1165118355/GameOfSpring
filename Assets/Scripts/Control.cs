using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Ship ControlShp;
    public Camera ControlCamera;
    public ParticleSystem EffectBackground;
    bool m_EnabledShowRange = false;
    void Start()
    {
        ControlShp.setAllWeaponSlotFireMode(WeaponSlot.FireMode.WEAPON_MODE_MANUAL);
        m_EnabledShowRange = ControlShp.EnabledShowRange;
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
        checkFire();
        updateCamera();
        checkHotKey();

    }

    void checkMove()
    {
        if (ControlShp != null)
        {
            if (Input.GetKey(KeyCode.W))
                ControlShp.moveForward();
            if (Input.GetKey(KeyCode.S))
                ControlShp.moveBack();
            if (Input.GetKey(KeyCode.A))
                ControlShp.rotationLeft();
            if (Input.GetKey(KeyCode.D))
                ControlShp.rotationRight();
        }
    }

    void checkFire()
    {

        if (Input.GetMouseButton(0))
        {
            ControlShp.fireAll();
        }
        if (Input.GetMouseButton(1))
        {
            ControlShp.fireFeatureAll();
        }
    }

    void updateCamera()
    {

        if (ControlCamera != null)
        {
            ControlCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 3;
        }
        if (ControlShp)
        {
            var position = ControlShp.transform.position;
            var particalShpe = EffectBackground.shape;
            particalShpe.position = position;
            position.z = -1000;
            ControlCamera.transform.position = position;

            //
            var mousePosition = Input.mousePosition;
            var worldPosition = ControlCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            ControlShp.arm(worldPosition);
        }
    }

    void checkHotKey()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_EnabledShowRange = !m_EnabledShowRange;
            ControlShp.EnabledShowRange = m_EnabledShowRange;
        }

        /// Test Code 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale -= 1;
        }
    }
}
