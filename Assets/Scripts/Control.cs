using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour
{
    public Ship ControlShp;
    public Camera ControlCamera;
    public ParticleSystem EffectBackground;
    public WeaponGroupPanel m_WeaponGroupUI;
    bool m_EnabledShowRange = false;
    int m_CurrentWeaponGroup=1;
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

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ControlShp.fireByWeaponGourp(m_CurrentWeaponGroup);
        }
        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            ControlShp.fireFeatureAll();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_CurrentWeaponGroup = 1;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_CurrentWeaponGroup = 2;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_CurrentWeaponGroup = 3;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_CurrentWeaponGroup = 4;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_CurrentWeaponGroup = 5;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }
    }

    void updateCamera()
    {
        if (ControlCamera != null && !EventSystem.current.IsPointerOverGameObject())
        {
            ControlCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 3;
        }
        if (ControlShp)
        {
            var position = ControlShp.transform.position;
            var particalShpe = EffectBackground.shape;
            particalShpe.position = position;
            position.z = -1000;

            //
            var mousePosition = Input.mousePosition;
            var worldPosition = ControlCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            ControlShp.arm(worldPosition);


            float xS = mousePosition.x / UnityEngine.Screen.width  * 2-1;
            float yS = mousePosition.y / UnityEngine.Screen.height * 2-1;
            var screenPosScale = new Vector3(xS, yS);
            var offset = screenPosScale * ((ControlCamera.orthographicSize * Mathf.Sin(screenPosScale.magnitude)) *0.8f);
            ControlCamera.transform.position = position + offset;
        }
    }

    void checkHotKey()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_EnabledShowRange = !m_EnabledShowRange;
            ControlShp.EnabledShowRange = m_EnabledShowRange;
            ControlShp.SetShowRange(m_CurrentWeaponGroup);
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            if(m_WeaponGroupUI.gameObject.activeSelf)
            {
                m_WeaponGroupUI.Hide();
            }
            else
            {
                m_WeaponGroupUI.Show(ControlShp);
            }
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
