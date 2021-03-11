using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public delegate void CallbackWeapon(Weapon weapon);

public class WeaponPreviewItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public GameObject m_WeaponDescriptionPanel;
    public Button m_Button;
    public Text m_NameText;
    public Text m_ShotRange;
    Weapon m_Weapon;
    public CallbackWeapon m_OnWeaponChoice;

    bool m_IsHover = false;
    float m_HoverTimeCount = 0;

    public Weapon WeaponItem {
        set { 
            m_Weapon = value;
            m_NameText.text = m_Weapon.Name;
            m_ShotRange.text = (m_Weapon.ShutRange * 100).ToString();
        }
        get { return m_Weapon; }
    } 
    void Start()
    {
        m_Button.onClick.AddListener(OnSelfClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsHover)
        {
            m_HoverTimeCount += Time.deltaTime;
            if (m_HoverTimeCount > 0.5)
            {
                showDescritionPanel();
                m_IsHover = false;
            }
        }
        else
        {
            m_HoverTimeCount = 0;
        }
    }

    void OnSelfClicked()
    {
        if(null != m_OnWeaponChoice)
        {
            m_OnWeaponChoice(m_Weapon);
        }
        hideDescriptionPanel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hideDescriptionPanel();
        m_IsHover = false;
    }

    void showDescritionPanel()
    {
        m_WeaponDescriptionPanel.GetComponent<RectTransform>().position = Input.mousePosition;
        m_WeaponDescriptionPanel.SetActive(true);
        WeaponDescriptionPanel panel = m_WeaponDescriptionPanel.GetComponent<WeaponDescriptionPanel>();
        panel.PerviewWeapon = m_Weapon;
    }
    void hideDescriptionPanel()
    {
        m_WeaponDescriptionPanel.SetActive(false);
    }

}
