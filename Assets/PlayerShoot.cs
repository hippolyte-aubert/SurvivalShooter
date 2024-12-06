using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Animator animator;
    private int fireHash = Animator.StringToHash("Fire");
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject fireLightPrefab;

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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(90, firePoint.rotation.y, firePoint.rotation.z)));
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        Vector3 rotation = new Vector3(0, firePoint.rotation.eulerAngles.y + transform.forward.y, 0);
        Instantiate(muzzleFlashPrefab, firePoint.position, Quaternion.Euler(rotation));
        GameObject gunLight = Instantiate(fireLightPrefab, firePoint.position, Quaternion.identity);
        Destroy(gunLight, 0.1f);
    }
}
