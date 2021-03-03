using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPreviewItem : MonoBehaviour
{
    // Start is called before the first frame update

    public Text m_Name;
    public Text m_ShotRange;
    Weapon m_Weapon;

    public Weapon WeaponItem {
        set { 
            m_Weapon = value;
            m_Name.text = m_Weapon.Name;
            m_ShotRange.text = (m_Weapon.WeaponShutRange * 100).ToString();
        }
        get { return m_Weapon; }
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
