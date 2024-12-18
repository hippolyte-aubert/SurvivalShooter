using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int _health;
    private bool _isDead;
    public int maxHealth = 100;
    public Image bloodScreen;
    public string damagingTag = "Enemy";
    
    private Animator _animator;
    private PlayerInput _playerInput;
    private InputHandler _starterAssetsInputs;
    
    public float hitRate = 0.5f;
    private float hitTimer = 0f;
    private bool resetTimer = false;

    public AudioSource source;
    public AudioClip hitSound;
    public AudioClip deathSound;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _starterAssetsInputs = GetComponent<InputHandler>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (resetTimer)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                hitTimer = hitRate;
                resetTimer = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damagingTag) && !resetTimer)
        {
            resetTimer = true;
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        
        _health -= damage;
        AnimateBloodScreen();
        if (_health <= 0)
        {
            Die();
            return;
        }
        
        source.PlayOneShot(hitSound);
    }
    
    // Change the blood screen alpha value based on missing health
    private void AnimateBloodScreen()
    {
        Color targetColor = new Color(bloodScreen.color.r, bloodScreen.color.g, bloodScreen.color.b, 1 - (float)_health / maxHealth);
        bloodScreen.color = Color.Lerp(bloodScreen.color, targetColor, Time.deltaTime * 150f);
    }

    private void Die()
    {
        _isDead = true; 
        source.PlayOneShot(deathSound);
        GameOverManager.instance.GameOver();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(false);
    }
}
