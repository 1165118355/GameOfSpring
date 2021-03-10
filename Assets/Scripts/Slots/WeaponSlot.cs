using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : Slot
{
    public enum FireMode 
    {
        WEAPON_MODE_MANUAL,
        WEAPON_MODE_AUTO,
        WEAPON_MODE_DISABLE,
    };
    public int WeaponGroup = 1;
    public Vector3 ForwardDirection = new Vector3(0, 1, 0);
    public Vector3 CurrentWeaponDirection;
    public Vector3 TargetPosition;
    public float AngleRange = 180;
    public float m_Angle = 0;
    public FireMode m_FireMode = FireMode.WEAPON_MODE_AUTO;
    public Weapon.Level m_Level = Weapon.Level.LEVEL_SMALL;
    float ShutCooling = 0;//  开火冷却时间
    float ShotErrorValueCooling = 0;//  开火误差冷却
    bool m_FireReady = true;
    public Weapon m_Weapon;
    GameObject RangeRender;
    public bool m_ShowRange = false;
    public bool ShowRange { 
        get { return m_ShowRange; }
        set {
            m_ShowRange = value;
            if (RangeRender)
                RangeRender.SetActive(m_ShowRange);
            }
    }

    void Start()  
    {
        CurrentWeaponDirection = ForwardDirection;
        Weapon weapon;
        if(m_Level == Weapon.Level.LEVEL_SMALL)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue > 0.5)
                weapon = new MissileStandard01();
            else
                weapon = new LightMachineGun();
            switchWeapon(weapon);
        }
        else
        {
            weapon = new AutoGunnery();
            switchWeapon(weapon);
        }

    }

    // Update is called once per frame
    void Update()
    {
        m_Weapon.update();
        ShutCooling += Time.deltaTime;
        if (ShutCooling > m_Weapon.ShutVelocity)
        {
            m_FireReady = true;
            ShutCooling = 0;
        }

        ShotErrorValueCooling = Mathf.Max(0, ShotErrorValueCooling - (Time.deltaTime + m_Weapon.m_ShotErrorValueIncrement* Time.deltaTime));

        //  误差在没开火的时候不停减小
        m_Weapon.m_ShotErrorValue = Mathf.Max(0, m_Weapon.m_ShotErrorValue - m_Weapon.m_ShotErrorValueIncrement * (1 - ShotErrorValueCooling) * Time.deltaTime * 20);

        //  先计算目标方向
        var forward = transform.rotation * ForwardDirection;
        var targetDifference = TargetPosition - transform.position;
        var targetDirection = targetDifference.normalized;
        var angleDifference = Vector3.Angle(CurrentWeaponDirection, targetDirection);
        if (angleDifference < 1)
            return;
        float lr = 0;
        if (angleDifference >= 180)
            lr = -Vector3.Cross(forward, targetDirection).z;
        else
            lr = Vector3.Cross(CurrentWeaponDirection, targetDirection).z;

        if (lr >= 0)
            m_Angle += m_Weapon.TurnSpeed * Time.deltaTime;
        else
            m_Angle -= m_Weapon.TurnSpeed * Time.deltaTime;

        m_Angle = Mathf.Clamp(m_Angle, -AngleRange/2, AngleRange/2);
        var resultRotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
        CurrentWeaponDirection = resultRotation * forward;

        if (EmiterObject)
            EmiterObject.transform.localRotation = resultRotation;

    }
    private void FixedUpdate()
    {

        /// 攻击模式
        if (m_FireMode != FireMode.WEAPON_MODE_AUTO)
            return;

        ShipManager.get();
        Ship selfShip = transform.parent.GetComponent<Ship>();
        string selfInfluence = selfShip.m_ShipStruct.Influence;
        for (int i=0; i<ShipManager.get().NumShips; ++i)
        {
            var ship = ShipManager.get().GetShip(i);
            if (ship == transform.parent.gameObject)
                continue;
            var forward = transform.rotation * ForwardDirection;
            var targetDifference = ship.transform.position - transform.position;
            var targetDirection = targetDifference.normalized;
            var angle = Vector3.Angle(forward, targetDirection);
            if(angle > AngleRange / 2)
            {
                continue; 
            }
            float targetPositionDistance = targetDifference.magnitude;
            if (targetPositionDistance > m_Weapon.ShutRange)
            {
                continue;
            }
            if(InfluenceMG.get().isHostile(selfInfluence, ship.GetComponent<Ship>().m_ShipStruct.Influence))
            {
                arm(ship.transform.position);
                fire();
            }
            break;
        }
    }
    void OnRenderObject()
    {
        return;
        if (m_Weapon == null)
            return;
        var resultDirection = transform.rotation * ForwardDirection;
        resultDirection = Quaternion.AngleAxis(AngleRange / 2, new Vector3(0, 0, 1)) * resultDirection;

        var currentPositionInScreen = Camera.current.WorldToScreenPoint(transform.position);
        currentPositionInScreen.x = currentPositionInScreen.x / Camera.current.pixelWidth;
        currentPositionInScreen.y = currentPositionInScreen.y / Camera.current.pixelHeight;

        GL.PushMatrix(); //保存当前Matirx  
        GL.LoadOrtho();  
        GL.Begin(GL.LINES);
        GL.Color(new Color(0.4f, 0.4f, 0.1f, 0.5f));
        GL.Vertex3(currentPositionInScreen.x, currentPositionInScreen.y, 0);

        for(int i=0; i<AngleRange; ++i)
        {
            resultDirection = Quaternion.AngleAxis(1, new Vector3(0, 0, -1)) * resultDirection;
            resultDirection = resultDirection.normalized;
            var targetPosition = Camera.current.WorldToScreenPoint(transform.position + resultDirection * m_Weapon.ShutRange);
            targetPosition.x = targetPosition.x / Camera.current.pixelWidth;
            targetPosition.y = targetPosition.y / Camera.current.pixelHeight;
            GL.Vertex3(targetPosition.x, targetPosition.y, 0);
        }
        GL.Vertex3(currentPositionInScreen.x, currentPositionInScreen.y, 0);
        GL.End();

        GL.Begin(GL.LINES);
        GL.Color(new Color(0.4f, 0.4f, 0.1f, 0.5f));
        var currentDirectionPosition = Camera.current.WorldToScreenPoint(transform.position + CurrentWeaponDirection);
        currentDirectionPosition.x = currentDirectionPosition.x / Camera.current.pixelWidth;
        currentDirectionPosition.y = currentDirectionPosition.y / Camera.current.pixelHeight;
        GL.Vertex3(currentPositionInScreen.x, currentPositionInScreen.y, 0);
        GL.Vertex3(currentDirectionPosition.x, currentDirectionPosition.y, 0);
        GL.End();
        GL.PopMatrix();//读取之前的Matrix  


    }

    public void arm(Vector3 position)
    {
        TargetPosition = position;
    }

    public void fire()
    {
        if (null == m_Weapon ||
            m_FireReady == false)
        {
            return;
        }
        m_Weapon.Fire(transform.parent.gameObject, transform.position, CurrentWeaponDirection);
        ShotErrorValueCooling = 1;
        m_FireReady = false;
        ShutCooling = 0;
    }

    public void switchWeapon(Weapon weapon)
    {
        if(EmiterObject)
            Destroy(EmiterObject);
        m_Weapon = weapon;
        EmiterObject = Instantiate(Resources.Load<GameObject>(m_Weapon.WeaponResource)) as GameObject;
        EmiterObject.transform.SetParent(transform, false);

        /// 跟据新的武器生成范围线
        if(RangeRender == null)
        {
            Object obj = Resources.Load<GameObject>("Perfabs/Line");
            RangeRender = Instantiate(obj) as GameObject;
            RangeRender.transform.SetParent(transform, false);
        }
        RangeRender.SetActive(m_ShowRange);
        RangeRender.transform.localScale = new Vector3(1/transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        LineRenderer line = RangeRender.GetComponent<LineRenderer>();

        var resultDirection = ForwardDirection;
        resultDirection = Quaternion.AngleAxis(AngleRange / 2, new Vector3(0, 0, 1)) * resultDirection;
        List<Vector3> points = new List<Vector3>();
        points.Add(Vector3.zero);

        for (int i = 0; i < AngleRange; ++i)
        {
            resultDirection = Quaternion.AngleAxis(1, new Vector3(0, 0, -1)) * resultDirection;
            resultDirection = resultDirection.normalized;

            points.Add(resultDirection * m_Weapon.ShutRange);
        }
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
