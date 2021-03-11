using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDescriptionPanel : MonoBehaviour
{
    public Image m_Perview;
    public Text m_Name2;
    public Text m_Level;
    public Text m_ShotRange;
    public Text m_TurnSpeed;
    public Text m_Damage;
    public Text m_ShotError;

    public Weapon PerviewWeapon
    {
        set { 
            m_Weapon = value;
            m_Perview.sprite = Resources.Load<Sprite>(m_Weapon.WeaponResource);
            m_Name2.text = m_Weapon.Name;
            m_Level.text = m_Weapon.m_Level.ToString();
            m_ShotRange.text = m_Weapon.ShutRange.ToString();
            m_TurnSpeed.text = m_Weapon.TurnSpeed.ToString();
            m_Damage.text = m_Weapon.Damage.ToString();
            m_ShotError.text = m_Weapon.m_ShotErrorValueMax.ToString();
        }
        get { return m_Weapon; }
    }

    Weapon m_Weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
