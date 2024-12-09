using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private int runHash = Animator.StringToHash("Run");
    
    public bool isChasing = false;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    
    public bool isAttacking = false;
    public float attackRate = 1f;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    
    private  void Start()
    {
        _agent.SetDestination(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
    }
    
    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= chaseDistance && distance > attackDistance)
        {
            Debug.Log("Chasing player");
            _agent.SetDestination(player.position);
            isChasing = true;
            _animator.SetBool(runHash, true);
        }
        else if (distance <= attackDistance)
        {
            _agent.ResetPath();
            isChasing = false;
            _animator.SetBool(runHash, false);
            if (!isAttacking)
            {
                Debug.Log("Attacking player");
                _animator.SetTrigger("Attack");
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
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
