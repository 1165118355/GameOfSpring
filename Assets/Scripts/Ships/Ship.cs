using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float VelocityMax = 20f;
    public float RotateSpeedMax = 50;
    bool m_EnabledShowRange = false;
    float m_EngineFire = 0;
    bool m_IsMoveNow = false;

    [SerializeField]
    public ShipStruct m_ShipStruct = new ShipStruct();
    public GameObject m_ShipInfluenceRelationFlag;

    List<Engine> m_Engines;
    List<WeaponSlot> m_WeaponSlots = new List<WeaponSlot>();
    List<FeatureSlot> m_FeatureSlots = new List<FeatureSlot>();
    string m_StructProcessUIPath = "Perfabs/StructProcess";
    string m_StructEnemyProcessUIPath = "Perfabs/StructEnemyProcess";
    string m_EnergyProcessUIPath = "Perfabs/EnergyProcess";

    GameObject m_StructValueObj;
    GameObject m_EnergyValueObj;
    float m_Radius=1;

    int m_isPlayerEnemy = 1;

    public bool EnabledShowRange {
        set {
            for(int i=0; i<m_WeaponSlots.Count; ++i)
            {
                m_WeaponSlots[i].ShowRange = value;
            }
            m_EnabledShowRange = value; }
        get { return m_EnabledShowRange; }
    }

    private void Awake()
    {
        ShipManager.get().AddShip(gameObject);
        m_ShipStruct.OnInFluenceChanged = onInfluenceChanged;
    }

    void Start()
    {
        m_Engines = new List<Engine>();
        collectEngines(gameObject);
        collectSlots(gameObject);
        m_ShipStruct.Influence = m_ShipStruct.Influence;

        var bound = GetComponent<SpriteRenderer>().bounds;
        m_Radius = (bound.size.y + bound.size.x) / 4;
        if(m_isPlayerEnemy == 1)
            createProcessUI(ref m_StructValueObj, m_StructEnemyProcessUIPath);
        else
            createProcessUI(ref m_StructValueObj, m_StructProcessUIPath);

        createProcessUI(ref m_EnergyValueObj, m_EnergyProcessUIPath);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_IsMoveNow)
        {
            m_EngineFire -= Time.deltaTime / 2;
            m_EngineFire = Mathf.Max(0, m_EngineFire);
        }
        else
        {
            m_EngineFire += Time.deltaTime / 2;
            m_EngineFire = Mathf.Min(1, m_EngineFire);
        }
        m_IsMoveNow = false;
        for (int i = 0; i < m_Engines.Count; ++i)
        {
            m_Engines[i].FirePower = m_EngineFire;
        }

        if (m_StructValueObj)
            m_StructValueObj.transform.position = transform.position + new Vector3(-m_Radius, m_Radius, 0);
        if (m_EnergyValueObj)
            m_EnergyValueObj.transform.position = transform.position + new Vector3(-m_Radius, m_Radius - m_StructValueObj.transform.localScale.y * 2, 0);
    }
    private void FixedUpdate()
    {
        m_ShipStruct.m_Electric = Mathf.Min(m_ShipStruct.m_ElectricMax, m_ShipStruct.m_Electric + m_ShipStruct.m_ElectricIncrement * Time.fixedDeltaTime);

        if(m_StructValueObj && m_StructValueObj.transform.childCount > 0)
        {
            var proccesValue = m_StructValueObj.transform.GetChild(0);
            proccesValue.localScale = new Vector3(m_ShipStruct.m_StructurePoint / m_ShipStruct.m_StructurePointMax, 1, 1);
        }
        if(m_EnergyValueObj && m_EnergyValueObj.transform.childCount > 0)
        {
            var proccesValue = m_EnergyValueObj.transform.GetChild(0);
            proccesValue.localScale = new Vector3(m_ShipStruct.m_Electric / m_ShipStruct.m_ElectricMax, 1, 1);
        }
    }

    void createProcessUI(ref GameObject objs, string path)
    {
        if(objs)
            Destroy(objs);

        var obj = Resources.Load<GameObject>(path);
        var uiParent = GameObject.Find("DynamicUI");
        if (obj)
        {
            GameObject proceesValue = Instantiate(obj as GameObject);

            var newScale = proceesValue.transform.localScale;
            newScale.x = m_Radius * 2;

            proceesValue.transform.localPosition = new Vector3(-m_Radius, m_Radius, 0);
            proceesValue.transform.localScale = newScale;
            objs = proceesValue.gameObject;
            if (uiParent)
                objs.transform.SetParent(uiParent.transform);
        }

    }

    private void OnDestroy()
    {
        Destroy(m_StructValueObj);
        Destroy(m_EnergyValueObj);
    }

    public void fireAll()
    {
        for(int i=0; i< m_WeaponSlots.Count; ++i)
        {
            var weaponSlot = m_WeaponSlots[i].GetComponent<WeaponSlot>();
            if(weaponSlot)
            {
                weaponSlot.fire();
            }
        }
    }
    public void fireFeatureAll()
    {
        for (int i = 0; i < m_FeatureSlots.Count; ++i)
        {
            var featureSlot = m_FeatureSlots[i].GetComponent<FeatureSlot>();
            if (featureSlot)
            {
                featureSlot.fire();
            }
        }
    }

    public void arm(Vector3 position)
    {
        for(int i=0; i< m_WeaponSlots.Count; ++i)
        {
            m_WeaponSlots[i].arm(position);
        }
    }

    public void Boom(float damage, Weapon.DamageType type)
    {
        switch(type)
        {
            case Weapon.DamageType.TYPE_EXPLOSION:
                damage *= 2;
                break;
            case Weapon.DamageType.TYPE_KINETIC:
                damage /= 2;
                break;
        }
        m_ShipStruct.StructurePoint -= damage;

        if(m_ShipStruct.StructurePoint <= 0)
        {
            Object obj = Resources.Load<GameObject>("Perfabs/Effect/Boom");
            GameObject boomObj = Instantiate(obj) as GameObject;
            boomObj.transform.position = gameObject.transform.position;
            ShipManager.get().RemoveShip(gameObject);
            Destroy(gameObject);
            Destroy(boomObj, 5);
        }
    }

    public void moveForward()
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.up * (VelocityMax * Time.deltaTime));
        m_IsMoveNow = true;
    }
    public void moveBack()
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(-transform.up * (VelocityMax * Time.deltaTime));
        m_IsMoveNow = true;
    }
    public void rotationLeft()
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddTorque(RotateSpeedMax*Time.deltaTime);
    }
    public void rotationRight()
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddTorque(-RotateSpeedMax * Time.deltaTime);
    }
 
    protected void collectEngines(GameObject obj)
    {
        Engine engine = obj.GetComponent<Engine>();
        if(engine != null)
        {
            m_Engines.Add(engine);
        }

        for(int i=0; i<obj.transform.childCount; ++i)
        {
            collectEngines(obj.transform.GetChild(i).gameObject);
        }
    }
    protected void collectSlots(GameObject obj)
    {
        WeaponSlot weaponSlot = obj.GetComponent<WeaponSlot>();
        FeatureSlot featureSlot = obj.GetComponent<FeatureSlot>();

        if(weaponSlot != null)
            m_WeaponSlots.Add(weaponSlot);

        if (featureSlot != null)
            m_FeatureSlots.Add(featureSlot);
        
        for (int i=0; i<obj.transform.childCount; ++i)
        {
            collectSlots(obj.transform.GetChild(i).gameObject);
        }
    }

    public void setAllWeaponSlotFireMode(WeaponSlot.FireMode fireMode)
    {
        for(int i=0; i< m_WeaponSlots.Count; ++i)
        {
            m_WeaponSlots[i].m_FireMode = fireMode;
        }
    }
    void onInfluenceChanged(string newInfluenceName)
    {
        m_isPlayerEnemy = -1;
        var influence = InfluenceMG.get().find(newInfluenceName);
        if (influence.Name != InfluenceMG.INFLUENCE_PLAYER)
        {
            if (influence.findHostile(InfluenceMG.INFLUENCE_PLAYER) >= 0)
            {
                SpriteRenderer sprite = m_ShipInfluenceRelationFlag.GetComponent<SpriteRenderer>();
                sprite.material = Resources.Load<Material>("Materials/Colors/Enemy");
                m_isPlayerEnemy = 1;
            }
            else
            {
                SpriteRenderer sprite = m_ShipInfluenceRelationFlag.GetComponent<SpriteRenderer>();
                sprite.material = Resources.Load<Material>("Materials/Colors/Friend");
                m_isPlayerEnemy = 0;
            }
        }
    }
}
