using UnityEngine;

public class LaserLine : MonoBehaviour
{
    public Camera playerCamera;
    private LineRenderer _lineRenderer;
    public Transform firePoint;
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lineRenderer.positionCount = 2;
        // _lineRenderer.SetPosition(0, firePoint.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, playerCamera.transform.position + playerCamera.transform.forward * 100);
        }
    }
}
