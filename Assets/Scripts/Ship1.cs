//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Ship : MonoBehaviour
//{
//    public float VelocityMax = 20f;
//    public float m_VelocityIncrement = 2f;
//    public float RotateSpeedMax = 50;
//    public float m_RotateSpeedIncrement = 160;
//    [SerializeField]
//    public ShipStruct m_ShipStruct = new ShipStruct();
//    public GameObject m_ShipInfluenceRelationFlag;
//    public Vector3 m_MotionVector = new Vector3();

//    float m_RotateSpeed = 0;
//    float m_EngineFire=0;

//    bool m_IsRotationNow = false;
//    bool m_IsMoveNow = false;
//    PolygonCollider2D m_Collider;
//    List<Engine> m_Engines;
//    List<WeaponSlot> m_WeaponSlots = new List<WeaponSlot>();
//    List<FeatureSlot> m_FeatureSlots = new List<FeatureSlot>();
//    bool m_EnabledShowRange = false;
//    public bool EnabledShowRange {
//        set {
//            for(int i=0; i<m_WeaponSlots.Count; ++i)
//            {
//                m_WeaponSlots[i].ShowRange = value;
//            }
//            m_EnabledShowRange = value; }
//        get { return m_EnabledShowRange; }
//    }

//    private void Awake()
//    {
//        ShipManager.get().AddShip(gameObject);
//        m_ShipStruct.OnInFluenceChanged = onInfluenceChanged;
//    }

//    void Start()
//    {
//        m_Engines = new List<Engine>();
//        m_Collider = GetComponent<PolygonCollider2D>();
//        collectEngines(gameObject);
//        collectSlots(gameObject);
//        m_ShipStruct.Influence = m_ShipStruct.Influence;
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log("Collision object:" + collision.gameObject.name);
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        if (!m_IsRotationNow)
//        {
//            if (m_RotateSpeed > 0)
//                m_RotateSpeed -= Mathf.Min((m_RotateSpeedIncrement/4) * Time.deltaTime, m_RotateSpeed);
//            if (m_RotateSpeed < 0)
//                m_RotateSpeed += Mathf.Min((m_RotateSpeedIncrement/4) * Time.deltaTime, Mathf.Abs(m_RotateSpeed));
//        }
        
//        if (!m_IsMoveNow)
//        {
//            m_EngineFire -= Time.deltaTime/2;
//            m_EngineFire = Mathf.Max(0, m_EngineFire);

//            var motionDir = m_MotionVector.normalized;
//            var shaForce = (GlobalDefine.OneUnit * m_VelocityIncrement * Time.deltaTime);
//            if(m_MotionVector.magnitude < shaForce)
//                m_MotionVector = Vector3.zero;
//            else
//                m_MotionVector = m_MotionVector - motionDir * shaForce;
//        }
//        else
//        {
//            m_EngineFire += Time.deltaTime/2;
//            m_EngineFire = Mathf.Min(1, m_EngineFire);
//        }
//        m_IsRotationNow = false;
//        m_IsMoveNow = false;
//        for (int i=0; i< m_Engines.Count; ++i)
//        {
//            m_Engines[i].FirePower = m_EngineFire;
//        }
//    }
//    private void FixedUpdate()
//    {
//        m_Collider.transform.position = m_Collider.transform.position + m_MotionVector;
//        m_Collider.transform.rotation = m_Collider.transform.rotation * Quaternion.AngleAxis(m_RotateSpeed * Time.fixedDeltaTime, new Vector3(0, 0, 1));
//        m_ShipStruct.m_Power = Mathf.Min(m_ShipStruct.m_PowerMax, m_ShipStruct.m_Power + m_ShipStruct.m_PowerIncrement * Time.fixedDeltaTime);
//    }

//    public void fireAll()
//    {
//        for(int i=0; i< m_WeaponSlots.Count; ++i)
//        {
//            var weaponSlot = m_WeaponSlots[i].GetComponent<WeaponSlot>();
//            if(weaponSlot)
//            {
//                weaponSlot.fire();
//            }
//        }
//    }
//    public void fireFeatureAll()
//    {
//        for (int i = 0; i < m_WeaponSlots.Count; ++i)
//        {
//            var featureSlot = m_FeatureSlots[i].GetComponent<FeatureSlot>();
//            if (featureSlot)
//            {
//                featureSlot.fire();
//            }
//        }
//    }

//    public void arm(Vector3 position)
//    {
//        for(int i=0; i< m_WeaponSlots.Count; ++i)
//        {
//            m_WeaponSlots[i].arm(position);
//        }
//    }

//    public void Boom(float damage, Weapon.DamageType type)
//    {
//        switch(type)
//        {
//            case Weapon.DamageType.TYPE_EXPLOSION:
//                damage *= 2;
//                break;
//            case Weapon.DamageType.TYPE_KINETIC:
//                damage /= 2;
//                break;
//        }
//        m_ShipStruct.StructurePoint -= damage;

//        if(m_ShipStruct.StructurePoint <= 0)
//        {
//            Object obj = Resources.Load<GameObject>("Perfabs/Effect/Boom");
//            GameObject boomObj = Instantiate(obj) as GameObject;
//            boomObj.transform.position = gameObject.transform.position;
//            ShipManager.get().RemoveShip(gameObject);
//            Destroy(gameObject);
//            Destroy(boomObj, 5);
//        }
//    }

//    public void moveForward()
//    {
//        m_MotionVector += transform.up * (GlobalDefine.OneUnit * m_VelocityIncrement * Time.deltaTime);
//        if (m_MotionVector.magnitude > GlobalDefine.OneUnit * VelocityMax)
//        {
//            var motionDirection = m_MotionVector.normalized;
//            m_MotionVector = motionDirection * (GlobalDefine.OneUnit * VelocityMax);
//        }
//        m_IsMoveNow = true;
//    }
//    public void moveBack()
//    {
//        m_MotionVector -= transform.up * (GlobalDefine.OneUnit * m_VelocityIncrement * Time.deltaTime);
//        if (m_MotionVector.magnitude > GlobalDefine.OneUnit * VelocityMax)
//        {
//            var motionDirection = m_MotionVector.normalized;
//            m_MotionVector = motionDirection * (GlobalDefine.OneUnit * VelocityMax);
//        }
//        m_IsMoveNow = true;
//    }
//    public void rotationLeft()
//    {
//        float value = m_RotateSpeed < 0 ? m_RotateSpeedIncrement * 2 : m_RotateSpeedIncrement;
//        m_RotateSpeed += m_RotateSpeedIncrement * Time.deltaTime;
//        m_RotateSpeed = Mathf.Min(m_RotateSpeed, RotateSpeedMax);
//        m_IsRotationNow = true;
//    }
//    public void rotationRight()
//    {
//        float value = m_RotateSpeed > 0 ? m_RotateSpeedIncrement * 2 : m_RotateSpeedIncrement;
//        m_RotateSpeed -= value * Time.deltaTime;
//        m_RotateSpeed = Mathf.Max(m_RotateSpeed, -RotateSpeedMax);
//        m_IsRotationNow = true;
//    }
 
//    protected void collectEngines(GameObject obj)
//    {
//        Engine engine = obj.GetComponent<Engine>();
//        if(engine != null)
//        {
//            m_Engines.Add(engine);
//        }

//        for(int i=0; i<obj.transform.childCount; ++i)
//        {
//            collectEngines(obj.transform.GetChild(i).gameObject);
//        }
//    }
//    protected void collectSlots(GameObject obj)
//    {
//        WeaponSlot weaponSlot = obj.GetComponent<WeaponSlot>();
//        FeatureSlot featureSlot = obj.GetComponent<FeatureSlot>();

//        if(weaponSlot != null)
//            m_WeaponSlots.Add(weaponSlot);

//        if (featureSlot != null)
//            m_FeatureSlots.Add(featureSlot);
        
//        for (int i=0; i<obj.transform.childCount; ++i)
//        {
//            collectSlots(obj.transform.GetChild(i).gameObject);
//        }
//    }

//    public void setAllWeaponSlotFireMode(WeaponSlot.FireMode fireMode)
//    {
//        for(int i=0; i< m_WeaponSlots.Count; ++i)
//        {
//            m_WeaponSlots[i].m_FireMode = fireMode;
//        }
//    }
//    void onInfluenceChanged(string newInfluenceName)
//    {
//        var influence = InfluenceMG.get().find(newInfluenceName);
//        if (influence.Name != InfluenceMG.INFLUENCE_PLAYER)
//        {
//            if (influence.findHostile(InfluenceMG.INFLUENCE_PLAYER) >= 0)
//            {
//                SpriteRenderer sprite = m_ShipInfluenceRelationFlag.GetComponent<SpriteRenderer>();
//                sprite.material = Resources.Load<Material>("Materials/Colors/Enemy");
//            }
//            else
//            {
//                SpriteRenderer sprite = m_ShipInfluenceRelationFlag.GetComponent<SpriteRenderer>();
//                sprite.material = Resources.Load<Material>("Materials/Colors/Friend");
//            }
//        }
//    }
//}
