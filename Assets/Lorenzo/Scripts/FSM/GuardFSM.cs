using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    //[SerializeField] private List<Hide> _hide;
    //[SerializeField] private float _hidingDistance = 1f;

    [SerializeField] private GameObject _target;
    [SerializeField] private float _minSightDistance = 3f;
    [SerializeField] private float _stoppingDistance = 5f;
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private Transform _gunPivot2;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _shootForce;

    [Range(0, 360)]
    [SerializeField] private float _viewAngle;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Transform _rotatingBase;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _targetFoundRotationSpeed;
    
    //Attack Parameter
    //public bool alreadyAttacked;
    //public float timeBetweenAttacks;

    //Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;
    [SerializeField] private LayerMask _visibilityRaLayerMask;
   
    
    bool goHideBool;
    bool stopChaseBool;
  
   
    private FiniteStateMachine<GuardFSM> _stateMachine;

    private NavMeshAgent agent;
    private int _currentWayPointIndex = 0;

    private Animator _animator;
    private Renderer _renderer;
    private Color _originalColor;
    public Renderer Renderer => _renderer;
    public Color OriginalColor => _originalColor;
    public Animator Animator => _animator;

    //Var Nuovo Movimento
    public float RaggioMovimento;
    public float TempoAlProssimoPunto;
    private float timer;



    void Start()
    {
        timer = TempoAlProssimoPunto;
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _rotatingBase.Rotate(Vector3.up, UnityEngine.Random.Range(0f, 355f));
        _originalColor = _renderer.material.color;
        _stateMachine = new FiniteStateMachine<GuardFSM>(this); //instanzia una macchina a stati finiti
        
        if(GameObject.Find("Shooter") != null)
        {
            _target = GameObject.Find("Shooter");
        }


        //STATES
        State patrolState = new PatrolState("Patrol", this);
        State chaseState = new ChaseState("Chase", this);
        State stopState = new StopState("Stop", this);
        //State hideState = new HideState("Hide", this);

        //TRANSITIONS definite dai parametri da soddisfare
        
        _stateMachine.AddTransition(patrolState, stopState, () => DistanceFromTarget() <= _stoppingDistance);
        _stateMachine.AddTransition(patrolState, chaseState, () => IsTargetInSight());

        _stateMachine.AddTransition(chaseState, patrolState, () => stopChaseBool);
        _stateMachine.AddTransition(chaseState, stopState, () => DistanceFromTarget() <= _stoppingDistance );

        //_stateMachine.AddTransition(stopState, hideState, () => goHideBool==false);
        _stateMachine.AddTransition(stopState, chaseState, () => DistanceFromTarget() > _stoppingDistance);

        //_stateMachine.AddTransition(hideState, chaseState, () => ArrivedInHide() <= _hidingDistance );


        //START STATE
        Vector3 walkpoint = RandomNavSphere(transform.position, RaggioMovimento, -1);
        _stateMachine.SetState(patrolState);
    }

    void Update() { 
        _stateMachine.Tik();

        //NuovoMovimento
        timer += Time.deltaTime;

        if (timer >= RaggioMovimento || agent.remainingDistance <= 10.0f || agent.velocity.sqrMagnitude == 0f)
        {
            Debug.Log("ARRIVATO IN POS");
            Vector3 walkpoint = RandomNavSphere(transform.position, RaggioMovimento, -1);
            agent.SetDestination(walkpoint);
            timer = 0;
        }
        //NuovoMovimento
    }
    public void StopAgent(bool stop) => agent.isStopped = stop;


    public void PointTarget()
    {
        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        Vector3 newDir = Vector3.RotateTowards(_rotatingBase.forward, directionToTarget, _targetFoundRotationSpeed * Time.deltaTime, 0f);
        _rotatingBase.rotation = Quaternion.LookRotation(newDir);
       

    }
    public void FollowTarget() => agent.SetDestination(_target.transform.position);

    //NuovoMovimento
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    //FUNZIONI PER IL MOVIMENTO VECCHIA
    /*    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);



        Vector3 distanceToWalkPoint = transform.position - walkPoint;


        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 10f)
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
    }*/

    public bool IsTargetInSight()
    {

        //CHECK IF IS WITHIN VIEW DISTANCE
        Vector3 directionToTarget = _target.transform.position - transform.position;
        float squareTargetDistance = (directionToTarget).sqrMagnitude;
        if (squareTargetDistance <= _minSightDistance * _minSightDistance)
        {
            //CHECK IF FALLS WITHIN VIEW ANGLE
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget.normalized);
            if (angleToTarget < _viewAngle * 0.5f)
            {
                //CHECK IF THERE ARE NO OBSTACLES
                RaycastHit hitInfo;
                Ray ray = new Ray(_rayOrigin.transform.position, (_target.transform.position - _rayOrigin.position).normalized);
                Debug.DrawRay(_rayOrigin.position, (_target.transform.position - _rayOrigin.position).normalized * _minSightDistance, Color.cyan);

                if (Physics.Raycast(ray, out hitInfo, _minSightDistance, _visibilityRaLayerMask))
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

    //NASCONDIGLI
    /*
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
    }*/

    //ATTACCO
    /*public void AttackPlayer()
    {
        //Make sure enemy doesn't move
        

        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.y = 0f;
        directionToTarget.Normalize();

        Vector3 newDir = Vector3.RotateTowards(_rotatingBase.forward, directionToTarget, _targetFoundRotationSpeed * Time.deltaTime, 0f);
        _rotatingBase.rotation = Quaternion.LookRotation(newDir);

    }

    private void ResetAttack()
    {
        firstShoot = false;
        secondShoot = false;
        alreadyAttacked = false;
    }*/

    private void FirstShoot()
    {
        Vector3 targetHead = _target.transform.position;
        Vector3 shootingDirection = (targetHead - _gunPivot2.position).normalized;
        Bullet bullet = Instantiate(_bullet, _gunPivot2.position, Quaternion.identity);
        bullet.transform.up = shootingDirection;


        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shootingDirection * _shootForce, ForceMode.Impulse);

        Debug.Log("Sparo1");
    }
    private void SecondShoot()
    {
        Vector3 targetShoot = _target.transform.position;
        Vector3 secondShootingDirection = (targetShoot - _gunPivot.position).normalized;
        Bullet bullet2 = Instantiate(_bullet, _gunPivot.position, Quaternion.identity);
        bullet2.transform.up = secondShootingDirection;

        Rigidbody bulletRb2 = bullet2.GetComponent<Rigidbody>();
        bulletRb2.AddForce(secondShootingDirection * _shootForce, ForceMode.Impulse);

        Debug.Log("Sparo2");
    }

    //Coroutines
    public IEnumerator goHide()
    {
        goHideBool = true;
        yield return new WaitForSeconds(3);
        goHideBool = false;

    }
  

    public IEnumerator stopChase()
    {
        stopChaseBool = false;
        yield return new WaitForSeconds(3);
        stopChaseBool = true;

    }

    //TRANSITION FUNCTIONS
    private float DistanceFromTarget() => Vector3.Distance(_target.transform.position, transform.position);
    
    //FUNCTION NASCONDIGLIO
    //private float ArrivedInHide() => Vector3.Distance(findClosestHide().transform.position, transform.position);



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
        _guard.Animator.SetBool("Attack", false);
        //_guard.Renderer.material.color = _guard.OriginalColor;
    }

    public override void Tik()
    {
        // _guard.Patroling();
        
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
        _guard.StartCoroutine(_guard.stopChase());
        _guard.Animator.SetBool("Attack", false);
    }

    public override void Tik()
    {
        _guard.PointTarget();
        _guard.FollowTarget();
        if (_guard.IsTargetInSight())
        {
            _guard.Animator.SetBool("Attack", true);
            //_guard.AttackPlayer();
        }
      
    }

    public override void Exit()
    {
        _guard.Animator.SetBool("Attack", true);
        _guard.StopAllCoroutines();
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

    }

    public override void Tik()
    {
        _guard.PointTarget();
        if (_guard.IsTargetInSight())
        {
            _guard.Animator.SetBool("Attack", true);
            //_guard.AttackPlayer();
        }

    }

    public override void Exit()
    {
        _guard.StopAllCoroutines();
    }
}


//STATO NASCONDIGLIO
/*public class HideState : State
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
        _guard.Animator.SetBool("Attack", true);
    }

    public override void Tik()
    {
        if (_guard.IsTargetInSight())
        {
            _guard.PointTarget();
            _guard.Animator.SetBool("Attack", true);
            //_guard.AttackPlayer();
        }
        else
        {
            _guard.Animator.SetBool("Attack", false);
        }
        _guard.findClosestHide();
    }

    public override void Exit()
    {

    }
}*/

