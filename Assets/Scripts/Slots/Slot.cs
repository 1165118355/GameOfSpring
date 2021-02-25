using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SlotSize
    {
        WEAPON_SIZE_SMALL,
        WEAPON_SIZE_MIDDLE,
        WEAPON_SIZE_BIG
    };
    public SlotSize Size = SlotSize.WEAPON_SIZE_SMALL;
    protected GameObject EmiterObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
