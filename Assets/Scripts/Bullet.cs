using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    private TrailRenderer _tr;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponent<TrailRenderer>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        _rb.useGravity = true;
        _tr.emitting = false;
        Destroy(gameObject, 2f);
        
    }
}
