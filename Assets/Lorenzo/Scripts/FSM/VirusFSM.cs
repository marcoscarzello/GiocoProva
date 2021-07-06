using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class VirusFSM : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _minSightDistance = 3f;
    [SerializeField] private float _portalDistance = 5f;
    [SerializeField] private float _stoppingDistance = 5f;

    //evento partito attacco
    public delegate void AttaccoVirus();
    public static event AttaccoVirus PartitoAttacco;

    [Range(0, 360)]
    [SerializeField] private float _viewAngle;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Transform _rotatingBase;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _targetFoundRotationSpeed;

    //Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;
    [SerializeField] private LayerMask _visibilityRaLayerMask;
    [SerializeField] private Particle ElectricParticle;


    bool goPortalBool;
    bool attackingHacker;

    private FiniteStateMachine<VirusFSM> _stateMachine;

    private NavMeshAgent agent;

    private Animator _animator;
    private Renderer _renderer;
    private Color _originalColor;
    public Renderer Renderer => _renderer;
    public Color OriginalColor => _originalColor;
    public Animator Animator => _animator;


    //Var Nuovo Movimento
    public float wanderRadius;
    public float wanderTimer;
    private float timer;
    private int rayLengthMeters = 1;
    private Vector3 rayDirection;
    private Vector3 portalToAttack;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _rotatingBase.Rotate(Vector3.up, UnityEngine.Random.Range(0f, 355f));
        _originalColor = _renderer.material.color;
        _stateMachine = new FiniteStateMachine<VirusFSM>(this); //instanzia una macchina a stati finiti
        attackingHacker = false;
        goPortalBool = true;

        if (GameObject.Find("Shooter") != null)
        {
            _target = GameObject.Find("Shooter");
        }

        //STATES
        State patrolState = new PatrolVirusState("PatrolVirus", this);
        State stopState = new StopVirusState("StopVirus", this);
        State attackHackerState = new AttackHackerState("AttackVirus", this);


        //TRANSITIONS definite dai parametri da soddisfare

        _stateMachine.AddTransition(patrolState, stopState, () => IsTargetInSight());

        _stateMachine.AddTransition(stopState, attackHackerState, () => goPortalBool == false);

        _stateMachine.AddTransition(attackHackerState, stopState, () => ArrivedInPortal()<= _stoppingDistance);



        //START STATE
        Vector3 walkpoint = RandomNavSphere(transform.position, wanderRadius, -1);
        _stateMachine.SetState(patrolState);
    }

    void Update()
    {

        //roba debug Marco
        if (Input.GetKeyDown(KeyCode.V)) {
            if (PartitoAttacco != null)
            {
                PartitoAttacco();
                Debug.Log("L'evento attacco virus parte dal client...");

            }
        }
        //fine roba debug marco



        _stateMachine.Tik();

        //NuovoMovimento
        timer += Time.deltaTime;
        rayDirection = transform.forward;
        RaycastHit hitInfo;

        if (goPortalBool == true) {

            if (Physics.Raycast(_rayOrigin.transform.position, rayDirection, out hitInfo, rayLengthMeters))
            {
                if (hitInfo.collider.gameObject.tag == "Wall")
                {
                    Debug.Log("Founded Wall");
                    nextPosition();
                }
            }

            if (timer >= wanderTimer || agent.remainingDistance <= 10.0f || agent.velocity.sqrMagnitude == 0f  )
            {
                nextPosition();
            }


        }
        //NuovoMovimento
    }
    public void StopAgent(bool stop) => agent.isStopped = stop;





    public void nextPosition()
    {
        Debug.Log("ARRIVATO IN POS");
        Vector3 walkpoint = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(walkpoint);
        timer = 0;
    }

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
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    /*
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

    //Nascondiglio Enemy
    public AttackPortal findClosestAttackPortal()
    {
        float distanceToClosestPortal = Mathf.Infinity;
        AttackPortal closestAttackablePortal = null;
        AttackPortal[] allAttackablePortal = GameObject.FindObjectsOfType<AttackPortal>();

        foreach (AttackPortal currentPortal in allAttackablePortal)
        {
            float distanceToAttackPortal = (currentPortal.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToAttackPortal < distanceToClosestPortal)
            {
                distanceToClosestPortal = distanceToAttackPortal;
                closestAttackablePortal = currentPortal;
                agent.SetDestination(closestAttackablePortal.transform.position);
                portalToAttack = closestAttackablePortal.transform.position;
            }
        }
        
        return closestAttackablePortal;
    }

    public void ArrivedInAttackPosition()
    {
        if (Vector3.Distance(portalToAttack, transform.position) <= agent.stoppingDistance  /*agent.remainingDistance <= agent.stoppingDistance*/)
        {
            Debug.Log("Arrivato Attacco");
            //invocazione evento di partito attacco virus
        
            if (PartitoAttacco != null)
            {
                PartitoAttacco();
                Debug.Log("Evento partito attacco virus inviato dal client");
                Instantiate(ElectricParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
                Destroy(gameObject, 2);
            }


        }

    }


    
    //Coroutines
    public IEnumerator goAttackHacker()
    {
        goPortalBool = true;
        yield return new WaitForSeconds(3);
        goPortalBool = false;

    }

    //TRANSITION FUNCTIONS
    private float ArrivedInPortal() => Vector3.Distance(portalToAttack, transform.position);



}


//sono nuove classi possono essere staccate da questa classe
public class PatrolVirusState : State
{
    private VirusFSM _virus;
    public PatrolVirusState(string name, VirusFSM virus) : base(name) //gli si passa un riferimento alla classe guardia e un nome
    {
        _virus = virus;
    }

    public override void Enter()
    {
        _virus.StopAgent(false);
        _virus.Animator.SetBool("patrol", true);


    }

    public override void Tik()
    {
        //_virus.Patroling();
    }

    public override void Exit()
    {

    }
}


public class StopVirusState : State
{
    private VirusFSM _virus;
    public StopVirusState(string name, VirusFSM virus) : base(name)
    {
        _virus = virus;
    }

    public override void Enter()
    {
        _virus.StartCoroutine(_virus.goAttackHacker());
        _virus.StopAgent(true);
        _virus.Animator.SetBool("patrol", false);
        
    }

    public override void Tik()
    {
        _virus.PointTarget();
    }

    public override void Exit()
    {
        _virus.StopAllCoroutines();
    }
}

public class AttackHackerState: State
{
    private VirusFSM _virus;
    public AttackHackerState(string name, VirusFSM virus) : base(name)
    {
        _virus = virus;
    }

    public override void Enter()
    {
        _virus.StopAgent(false);
        _virus.Animator.SetBool("patrol", false);
        _virus.findClosestAttackPortal();
        
    }

    public override void Tik()
    {
        _virus.ArrivedInAttackPosition();
    }

    public override void Exit()
    {

    }
}


