using UnityEditor;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Animator animator;
    private int fireHash = Animator.StringToHash("Fire");
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject fireLightPrefab;
    
    public Camera playerCamera;

    public float bulletForce = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire(bool state)
    {
        if (state)
        {
            animator.SetTrigger(fireHash);
        }
    }
    
    public void Shoot()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 bulletDirection = ray.direction;
        Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection);
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce, ForceMode.Impulse);
        
        Vector3 rotation = new Vector3(0, firePoint.rotation.eulerAngles.y + transform.forward.y, 0);
        
        Instantiate(muzzleFlashPrefab, firePoint.position, Quaternion.Euler(rotation));
        GameObject gunLight = Instantiate(fireLightPrefab, firePoint.position, Quaternion.identity);
        Destroy(gunLight, 0.1f);
    }
}
