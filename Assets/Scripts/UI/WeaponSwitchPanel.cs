using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSwitchPanel : MonoBehaviour
{
    public GameObject m_WeaponDescriptionPanel;
    public GameObject m_Viewport;
    public WeaponPreviewItem m_CurrentPerviewItem;
    public GameObject m_PreviewItemPerfab;
    WeaponSlot m_Slot;

    private void Update()
    {
        if (m_Slot)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(m_Slot.transform.position);
        }
    }

    public void updateItemList(WeaponSlot slot)
    {
        m_Slot = slot;
        for (int i=0; i< m_Viewport.transform.childCount; ++i)
        {
            var child = m_Viewport.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        m_CurrentPerviewItem.WeaponItem = slot.m_Weapon;
        m_CurrentPerviewItem.m_Slot = slot;

        List<Weapon> weapons = new List<Weapon>();
        weapons.Add(new LightMachineGun());
        weapons.Add(new MissileStandard01());
        float height = 0;
        for (int i=0; i< weapons.Count; ++i)
        {
            var objItem = Instantiate<GameObject>(m_PreviewItemPerfab);
            objItem.transform.SetParent(m_Viewport.transform);

            var perviewItem = objItem.GetComponent<WeaponPreviewItem>();
            perviewItem.m_WeaponDescriptionPanel = m_WeaponDescriptionPanel;
            perviewItem.WeaponItem = weapons[i];
            perviewItem.m_Slot = m_Slot;
            height += perviewItem.GetComponent<RectTransform>().rect.height;
        }
        Vector2 rect = new Vector2(m_Viewport.GetComponent<RectTransform>().rect.width, m_Viewport.GetComponent<RectTransform>().rect.height);
        rect.y = height;
        var viewportTransform = m_Viewport.GetComponent<RectTransform>();
        viewportTransform.sizeDelta = rect;
    }
}
