using UnityEditor;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 100;
    public string damagingTag = "Bullet";
    public int damageReceived = 10;
    public GameObject bloodEffect;
    
    [HideInInspector] public bool isDead = false;
    private Animator _animator;
    private readonly int _dieHash = Animator.StringToHash("Die");
    private readonly int _takeDamageHash = Animator.StringToHash("TakeDamage");
    private readonly int _deadHash = Animator.StringToHash("Dead");
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    // This method is called when the object collides with another object
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(damagingTag))
        {
            TakeDamage();
            if (bloodEffect != null) Instantiate(bloodEffect, other.GetContact(0).point, Quaternion.identity);
        }
    }
    
    private void TakeDamage()
    {
        health -= damageReceived;
        _animator.SetTrigger(_takeDamageHash);
        if (health <= 0 && !isDead)
        {
            GameManager.instance.AddKillCount();
            isDead = true;
            _animator.SetBool(_deadHash, true);
            _animator.SetTrigger(_dieHash);
            Destroy(gameObject, 5f);
        }
    }
}
