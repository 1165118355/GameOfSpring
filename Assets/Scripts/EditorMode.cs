using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorMode : MonoBehaviour
{
    public WeaponSwitchPanel m_WeaponSwitcher;
    void Start()
    {
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                m_WeaponSwitcher.gameObject.SetActive(false);

            int layerMask = 1 << 10;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100000, layerMask);
            if (hit.collider != null)
            {
                var obj = hit.collider.gameObject;
                var weaponSlot = obj.GetComponent<WeaponSlot>();
                if(weaponSlot)
                {
                    Debug.Log("current weapon is "+ weaponSlot.m_Weapon.Name);
                    m_WeaponSwitcher.gameObject.SetActive(true);
                    m_WeaponSwitcher.updateItemList(weaponSlot);
                }
            }

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            m_WeaponSwitcher.gameObject.SetActive(false);
        }
    }
}
