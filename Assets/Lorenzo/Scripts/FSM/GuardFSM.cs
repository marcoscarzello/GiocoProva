using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    //[SerializeField] private List<Hide> _hide;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _minChaseDistance = 3f;
    [SerializeField] private float _minAttackDistance = 2f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private float _hidingDistance = 1f;

    [Range(0, 360)]
    [SerializeField] private float _viewAngle;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Transform _rotatingBase;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _targetFoundRotationSpeed;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField] private LayerMask _visibilityRaLayerMask;
    bool goHideBool;


    private FiniteStateMachine<GuardFSM> _stateMachine;

    private NavMeshAgent agent;
    private int _currentWayPointIndex = 0;

    private Renderer _renderer;
    private Color _originalColor;
    public Renderer Renderer => _renderer;
    public Color OriginalColor => _originalColor;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _renderer = GetComponent<Renderer>();
        _rotatingBase.Rotate(Vector3.up, UnityEngine.Random.Range(0f, 355f));
        _originalColor = _renderer.material.color;
        _stateMachine = new FiniteStateMachine<GuardFSM>(this); //instanzia una macchina a stati finiti



        //STATES
        State patrolState = new PatrolState("Patrol", this);
        State chaseState = new ChaseState("Chase", this);
        State stopState = new StopState("Stop", this);
        State hideState = new HideState("Hide", this);

        //TRANSITIONS definite dai parametri da soddisfare
        _stateMachine.AddTransition(patrolState, chaseState, () => IsTargetInSight()); 
        _stateMachine.AddTransition(chaseState, patrolState, () => !IsTargetInSight());
        _stateMachine.AddTransition(chaseState, stopState, () => DistanceFromTarget() <= _stoppingDistance);
        _stateMachine.AddTransition(stopState, chaseState, () => DistanceFromTarget() > _stoppingDistance);
        _stateMachine.AddTransition(stopState, hideState, () => goHideBool==false);
        _stateMachine.AddTransition(hideState, patrolState, () => ArrivedInHide() <= _hidingDistance );
        //START STATE
        _stateMachine.SetState(patrolState);
    }

    void Update() => _stateMachine.Tik();
    public void StopAgent(bool stop) => agent.isStopped = stop;
    /*public void SetWayPointDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude <= 0f)
        {
            _currentWayPointIndex = (_currentWayPointIndex + 1) % _waypoints.Count;
            Vector3 nextWayPointPos = _waypoints[_currentWayPointIndex].position;
            agent.SetDestination(new Vector3(nextWayPointPos.x, transform.position.y, nextWayPointPos.z));
        }
    }*/

    public void PointTarget()
    {
        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        Vector3 newDir = Vector3.RotateTowards(_rotatingBase.forward, directionToTarget, _targetFoundRotationSpeed * Time.deltaTime, 0f);
        _rotatingBase.rotation = Quaternion.LookRotation(newDir);
    }
    public void FollowTarget() => agent.SetDestination(_target.transform.position);

    //FUNCTION FOR MOVING
    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, _visibilityRaLayerMask))
            walkPointSet = true;
    }

    private bool IsTargetInSight()
    {

        //CHECK IF IS WITHIN VIEW DISTANCE
        Vector3 directionToTarget = _target.transform.position - transform.position;
        float squareTargetDistance = (directionToTarget).sqrMagnitude;
        if (squareTargetDistance <= _minChaseDistance * _minChaseDistance)
        {
            //CHECK IF FALLS WITHIN VIEW ANGLE
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget.normalized);
            if (angleToTarget < _viewAngle * 0.5f)
            {
                //CHECK IF THERE ARE NO OBSTACLES
                RaycastHit hitInfo;
                Ray ray = new Ray(_rayOrigin.transform.position, (_target.transform.position - _rayOrigin.position).normalized);
                Debug.DrawRay(_rayOrigin.position, (_target.transform.position - _rayOrigin.position).normalized * _minChaseDistance, Color.cyan);

                if (Physics.Raycast(ray, out hitInfo, _minChaseDistance, _visibilityRaLayerMask))
                {
                    Target target = hitInfo.transform.GetComponentInParent<Target>();
                    if (target)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //Nascondiglio Enemy
    
    public Hide findClosestHide()
    {
        float distanceToClosestHide = Mathf.Infinity;
        Hide closestHide = null;
        Hide[] allHides = GameObject.FindObjectsOfType<Hide>();

        foreach(Hide currentHide in allHides)
        {
            float distanceToHide = (currentHide.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToHide < distanceToClosestHide)
            {
                distanceToClosestHide = distanceToHide;
                closestHide = currentHide;
                agent.SetDestination(closestHide.transform.position);

            }
        }
        return closestHide;
    }

    public IEnumerator goHide()
    {
        goHideBool = true;
        yield return new WaitForSeconds(3);
        goHideBool = false;

    }


    //TRANSITION FUNCTIONS
    private float DistanceFromTarget() => Vector3.Distance(_target.transform.position, transform.position);
    private float ArrivedInHide() => Vector3.Distance(findClosestHide().transform.position, transform.position);

}


//sono nuove classi possono essere staccate da questa classe
public class PatrolState : State
{
    private GuardFSM _guard;
    public PatrolState(string name, GuardFSM guard) : base(name) //gli si passa un riferimento alla classe guardia e un nome
    {
        _guard = guard;
    }

    public override void Enter()
    {
        _guard.StopAgent(false);
        _guard.Renderer.material.color = _guard.OriginalColor;
    }

    public override void Tik()
    {
        _guard.Patroling();
    }

    public override void Exit()
    {

    }
}

public class ChaseState : State
{
    private GuardFSM _guard;
    public ChaseState(string name, GuardFSM guard) : base(name)
    {
        _guard = guard;
    }

    public override void Enter()
    {
        _guard.StopAgent(false);
        _guard.Renderer.material.color = Color.yellow;
    }

    public override void Tik()
    {
        _guard.PointTarget();
        _guard.FollowTarget();
    }

    public override void Exit()
    {
    }
}

public class StopState : State
{
    private GuardFSM _guard;
    public StopState(string name, GuardFSM guard) : base(name)
    {
        _guard = guard;
    }

    public override void Enter()
    {
        _guard.StartCoroutine(_guard.goHide());
        _guard.StopAgent(true);
        _guard.Renderer.material.color = Color.red;
    }

    public override void Tik()
    {
        _guard.PointTarget();
    }

    public override void Exit()
    {
    }
}

public class HideState : State
{
    private GuardFSM _guard;
    public HideState(string name, GuardFSM guard) : base(name)
    {
        _guard = guard;
    }

    public override void Enter()
    {
        _guard.StopAgent(false);
        _guard.Renderer.material.color = Color.red;
    }

    public override void Tik()
    {
        _guard.findClosestHide();
    }

    public override void Exit()
    {
    }
}

