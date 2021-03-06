using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGroupItem : MonoBehaviour
{
    public Text m_NameText;
    public Dropdown m_Group;
    public Toggle m_AutoFire;
    WeaponSlot m_WeaponSlot;

    public WeaponSlot WeaponSlot {
        set {
            if (null == value)
                return;
            m_WeaponSlot = value;
            m_NameText.text = m_WeaponSlot.m_Weapon.Name;
            m_Group.value = m_WeaponSlot.WeaponGroup - 1;
            m_AutoFire.isOn = (m_WeaponSlot.m_FireMode == WeaponSlot.FireMode.WEAPON_MODE_AUTO);
        }
        get { return m_WeaponSlot; }
    }

    private void Awake()
    {
        m_Group.onValueChanged.AddListener(onWeaponGroupValueChanged);
        m_AutoFire.onValueChanged.AddListener(onAutoFireValueChanged);
    }

    void onWeaponGroupValueChanged(int value)
    {
        if (!m_WeaponSlot)
            return;
        m_WeaponSlot.WeaponGroup = m_Group.value+1;
    }
    void onAutoFireValueChanged(bool value)
    {
        if (!m_WeaponSlot)
            return;
        m_WeaponSlot.m_FireMode = value ? WeaponSlot.FireMode.WEAPON_MODE_AUTO : WeaponSlot.FireMode.WEAPON_MODE_MANUAL;
    }
}
