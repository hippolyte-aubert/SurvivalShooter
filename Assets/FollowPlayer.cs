using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform cameraTransform;
    private NavMeshAgent agent;

    public float followDistance = 2f;
    public Vector3 cameraOffset = new Vector3(0, 0, -2);
    private bool pauseFollow = false;

    private Vector3 targetPosition;
    
    [Header("Camera zoom")]
    public float defaultZoom = 60f;
    public float targetZoom = 40f;
    public float zoomSpeed = 2;
    private Camera cam;
    private bool zoom = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }
    
    private void Start()
    {
        // cameraTransform.position += cameraOffset;
    }

    void Update()
    {
        // Calculate the direction and target position to maintain follow distance
        Vector3 direction = (player.position - transform.position).normalized;
        targetPosition = player.position - direction * followDistance;

        // Move towards target position if distance is greater than followDistance
        if (Vector3.Distance(transform.position, player.position) > followDistance)
        {
            if (!pauseFollow)
            {
                agent.SetDestination(targetPosition);
            }
            else
            {
                agent.ResetPath();
            }
        }
        else
        {
            agent.ResetPath();
        }
        
        // Camera rotation to look at the player
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.LookRotation((player.position + cameraOffset) - cameraTransform.position), Time.deltaTime * 5);
    }

    public void Zoom(bool isPressed)
    {
        if (isPressed) zoom = !zoom;
    }
    
    public void PauseFollow(bool isPressed)
    {
        if (isPressed) pauseFollow = !pauseFollow;
    }
    
    private void LateUpdate()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom ? targetZoom : defaultZoom, Time.deltaTime * zoomSpeed);
    }
}
