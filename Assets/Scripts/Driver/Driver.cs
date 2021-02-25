using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    public enum State
    {
        STATE_STANDBY,      //  待机
        STATE_CHASE,        //  追击
        STATE_MOVE,         //  移动
        STATE_RETREAT,      //  撤退 
        STATE_HARASS,       //  骚扰
        STATE_ENCIRCLE,     //  环绕
    };
    public float MinDistance = 7;
    public State m_CurrentState = State.STATE_STANDBY;
    public GameObject Target
    {
        set {m_Target = value;}
        get { return m_Target; }
    }
    public Vector3 m_TargetPosition;

    GameObject m_Target;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Target)
        {
            m_TargetPosition = Target.transform.position;
        }
        else
        {
            m_CurrentState = State.STATE_STANDBY;
        }
        var ship = GetComponent<Ship>();
        if(!ship)
            return;

        switch(m_CurrentState)
        {
        case State.STATE_CHASE:
                chase();
                break;
        case State.STATE_ENCIRCLE:
                encircle();
                break;
        case State.STATE_HARASS:
                harass();
                break;
        case State.STATE_MOVE:
                move();
                break;
        case State.STATE_RETREAT:
                retereat();
                break;
        }
    }

    public float currentTime = 0;
    public float findTargetTime = 1;
    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime < findTargetTime)
            return;
        currentTime = 0;
        findTargetTime = Random.Range(0.2f,1f);

        var selfShipCom = GetComponent<Ship>();
        var selfInfluence = selfShipCom.m_ShipStruct.Influence;
        float minDistance = 111111111110;
        GameObject nearObj = null;
        for (int i=0; i<ShipManager.get().NumShips; ++i)
        {
            var shipObj = ShipManager.get().GetShip(i);
            var shipDefference = shipObj.transform.position - transform.position;

            var shipCom = shipObj.GetComponent<Ship>();

            if(InfluenceMG.get().isHostile(selfInfluence, shipCom.m_ShipStruct.Influence))
            {
                var detectRange = shipCom.m_ShipStruct.DetectRange;
                var theDistance = shipDefference.magnitude;
                if (theDistance < detectRange && 
                    theDistance < minDistance)
                {
                    minDistance = theDistance;
                    nearObj = shipObj;
                }
            }
        }
        if (nearObj)
            Target = nearObj;
    }

    void chase()
    {
        move();
    }
    void move()
    {
        var ship = GetComponent<Ship>();
        if (!ship)
            return;
        var targetDifference = m_TargetPosition - transform.position;
        var targetDir = targetDifference.normalized;
        float z = Vector3.Cross(targetDir, transform.up).z;
        if(z <= 0)
        {
            ship.rotationLeft();
        }
        else
        {
            ship.rotationRight();
        }
        if(MinDistance < targetDifference.magnitude)
        {
            if(Vector3.Angle(targetDir, transform.up) < 90)
            {
                ship.moveForward();
            }
        }
    }
    void retereat()
    {

    }
    void harass()
    {

    }
    void encircle()
    {

    }
}
