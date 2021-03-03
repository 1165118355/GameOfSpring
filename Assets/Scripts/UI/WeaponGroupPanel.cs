using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponGroupPanel : MonoBehaviour
{
    public GameObject Viewport;
    public GameObject WeaponGroupItemPerfab;

    public void Show(Ship ship)
    {
        gameObject.SetActive(true);
        for(int i=0; i< Viewport.transform.childCount; ++i)
        {
            var child = Viewport.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        for(int i=0; i< ship.m_WeaponSlots.Count; ++i)
        {
            GameObject obj = Instantiate<GameObject>(WeaponGroupItemPerfab);

            obj.transform.SetParent(Viewport.transform);
            WeaponGroupItem item = obj.GetComponent<WeaponGroupItem>();
            item.WeaponSlot = ship.m_WeaponSlots[i];
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < Viewport.transform.childCount; ++i)
        {
            var child = Viewport.transform.GetChild(i);
            Destroy(child.gameObject);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}