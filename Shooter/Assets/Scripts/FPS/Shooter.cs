using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunPosition;
    public float bulletSpeed;
    public float fireRate = .5f;

    private float m_fireRate;

    private bool canShoot;
    public Texture2D crosshair;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(crosshair, new Vector2(), CursorMode.Auto);
        Cursor.visible = true;

        m_fireRate = fireRate;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        CheckNextFire();
    }
    void CheckNextFire()
    {
        if (!canShoot)
        {
            if (m_fireRate > 0)
            {
                m_fireRate -= Time.deltaTime;
            }
            else if (m_fireRate <= 0)
            {
                m_fireRate = fireRate;
                canShoot = true;
            }
        }
    }
    void Shoot()
    {
        if (canShoot)
        {
            RaycastHit hit;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Physics.Raycast(Camera.main.ViewportPointToRay(Input.mousePosition), out hit, 100f);
            var bullet = Instantiate(bulletPrefab, gunPosition.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.transform.LookAt(hit.point);
            bulletScript.SetBullet(hit, transform.forward, bulletSpeed);
            
            canShoot = false;
        }
    }
    private void OnDrawGizmos()
    {
       Gizmos.DrawRay(new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward));
    }
}
