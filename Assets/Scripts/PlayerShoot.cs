using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public Animator animator;
    private int fireHash = Animator.StringToHash("Fire");
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject fireLightPrefab;
    
    public Camera playerCamera;
    
    public int magazineSize = 30;
    private int _currentAmmo;
    public TextMeshProUGUI ammoText;
    
    public Image reloadImage;
    public float reloadTime = 1.5f;
    private bool _isReloading = false;

    public float bulletForce = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentAmmo = magazineSize;
        ammoText.text = _currentAmmo + "/" + magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire(bool state)
    {
        if (state && !_isReloading)
        {
            animator.SetTrigger(fireHash);
        }
    }
    
    public void Shoot()
    {
        if (_currentAmmo <= 0) return;
        
        _currentAmmo--;
        ammoText.text = _currentAmmo + "/" + magazineSize;
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
    
    public void Reload(bool state)
    {
        if (state && _currentAmmo < magazineSize && !_isReloading)
        {
            StartCoroutine(ReloadImageAnimation());
        }
    }

    
    IEnumerator ReloadImageAnimation()
    {
        reloadImage.gameObject.SetActive(true);
        ammoText.transform.parent.gameObject.SetActive(false);
        reloadImage.fillAmount = 0;
        float timer = 0;
        _isReloading = true;
        while (timer < reloadTime)
        {
            timer += Time.deltaTime;
            reloadImage.fillAmount = timer / reloadTime;
            yield return null;
        }
        
        _isReloading = false;
        _currentAmmo = magazineSize;
        ammoText.text = _currentAmmo + "/" + magazineSize;
        reloadImage.gameObject.SetActive(false);
        ammoText.transform.parent.gameObject.SetActive(true);
    }
}
