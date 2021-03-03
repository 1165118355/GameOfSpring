using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchPanel : MonoBehaviour
{
    public GameObject m_Viewport;
    public GameObject m_PreviewItem;



    void updateItemList()
    {
        
        for(int i=0; i< m_Viewport.transform.childCount; ++i)
        {
            var child = m_Viewport.transform.GetChild(i);
            Destroy(child);
        }

        List<Weapon> weapons = new List<Weapon>();
        weapons.Add(new LightMachineGun());
        weapons.Add(new MissileStandard01());
        float height = 0;
        for (int i=0; i< weapons.Count; ++i)
        {
            var objItem = Instantiate<GameObject>(m_PreviewItem);
            objItem.transform.SetParent(transform);

            var perviewItem = objItem.GetComponent<WeaponPreviewItem>();
            perviewItem.WeaponItem = weapons[i];
            height += perviewItem.GetComponent<RectTransform>().rect.height;
        }
        var rect = m_Viewport.GetComponent<RectTransform>().rect.height;
    }
}
