using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    Rigidbody m_rigidBody;

    [SerializeField]
    float bulletSpeed = 8f;
    [SerializeField]
    int damageAmount = 20;
    [SerializeField]
    float bulletLifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        if (m_rigidBody)
        {
            m_rigidBody.velocity = transform.up * bulletSpeed;
        }
    }
    private void FixedUpdate()
    {
        bulletLifeTime -= Time.deltaTime;
        if(bulletLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyLogic enemyLogic = other.GetComponent<EnemyLogic>();
            enemyLogic.TakeDamage(damageAmount);

            Destroy(gameObject);
        }
    }
}
