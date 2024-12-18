using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private readonly int _runHash = Animator.StringToHash("Run");
    
    public bool isChasing = false;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    
    public bool isAttacking = false;
    public float attackRate = 1f;
    public BoxCollider attackCollider;
    
    private HealthSystem _healthSystem;

    public AudioSource source;
    public AudioClip footstepClip;
    [Range(0, 1)] public float footstepAudioVolume = 0.5f;
    public AudioClip attackClip;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();
        attackCollider.enabled = false;
    }
    
    private  void Start()
    {
        if (!GameManager.instance.isGameOver) _player = GameObject.FindWithTag("Player").transform;
        _agent.SetDestination(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
    }
    
    private void Update()
    {
        if (_healthSystem.isDead)
        {
            _agent.ResetPath();
            return;
        }

        if (_player == null)
        {
            _agent.SetDestination(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            return;
        }
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance <= chaseDistance && distance > attackDistance)
        {
            _agent.SetDestination(_player.position);
            isChasing = true;
            _animator.SetBool(_runHash, true);
        }
        else if (distance <= attackDistance)
        {
            _agent.ResetPath();
            isChasing = false;
            _animator.SetBool(_runHash, false);
            if (!isAttacking)
            {
                _animator.SetTrigger("Attack");
                source.PlayOneShot(attackClip);
                StartCoroutine(Attack());
            }
        }
    }
    
    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
    }
    
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }
    
    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    
    void OnZombieFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(footstepClip, transform.TransformPoint(Vector3.down * 1f), footstepAudioVolume);
        }
    }
}
