using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public Rigidbody rb { get; private set; }

    public GameObject destructParticle;
    public int damage = 30;
    
    private RaycastHit hit;
    private Vector3 direction;
    private float bulletSpeed;
    private bool canProject;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canProject)
            rb.AddForce(direction * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }
    public void SetBullet(RaycastHit hit, Vector3 direction, float bulletSpeed)
    {
        this.hit = hit;
        this.direction = direction;
        this.bulletSpeed = bulletSpeed;
        canProject = true;
    }
    private void OnCollisionEnter(Collision other)
    {
        // Damage Enemy
        if (other.collider.CompareTag("Enemy"))
        {
            other.collider.SendMessage("TakeDamage", damage);
        }

        //Destroy Bullet
        Instantiate(destructParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
