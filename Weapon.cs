using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon= true;
    public int weaponDamage;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset=true;
    public float shootingDelay = 2f;

    //burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //spread
    public float spreadIntensity;


    //bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    private Animator animator;

    public float reloadTime;
    public int magazineSize, bulletLeft;
    public bool isReloading;

    public enum WeaponModel
    {
        Pistol_1911,
        MCX
    }

    public WeaponModel thisWeaponModel;

   

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveWeapon) 
        {

            if (bulletLeft == 0 && isShooting)
            {
                SoundManager.Instance.emptyMagazinesound1911.Play();
            }



            if (currentShootingMode == ShootingMode.Auto)
            {
                //Holding
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && (bulletLeft < magazineSize) && !isReloading)
            {
                Reload();
            }

            if (readyToShoot && !isShooting && !isReloading && burstBulletsLeft <= 0)
            {
                Reload();
            }


            if (readyToShoot && isShooting && bulletLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            if (AmmoManager.Instance.ammoDisplay != null)
            {
                AmmoManager.Instance.ammoDisplay.text = $"{bulletLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
            }
        }
    }

    private void FireWeapon()
    {
        bulletLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Recoil");

        //SoundManager.Instance.shootingSound1911.Play();
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot=false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;


        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage=weaponDamage;


        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection* bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));    

        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset=false;
        }

        if(currentShootingMode==ShootingMode.Burst&&burstBulletsLeft>1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
            
  
        }

    }

    private void Reload()
    {
        //SoundManager.Instance.reloadingSound1911.Play();
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("Reload");

        isReloading = true;
        Invoke("ReloadCompleted",reloadTime);
    }
    private void ReloadCompleted()
    {
        bulletLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot=true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint-bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new Vector3(x, y, 0);

    }


    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
