using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthBar;
    public GameObject destructParticle;
    public int maxHealth = 100;
    private int health;

    public float healthBarActiveTime = 5f;
    private float _healthBarActiveTime;
    // Start is called before the first frame update
    void Start()
    {
        _healthBarActiveTime = healthBarActiveTime;
        health = maxHealth;
        healthBar.value = health / maxHealth;
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        TurnOffHealthBar();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.gameObject.SetActive(true);

        healthBar.value = (float)health / maxHealth;

        if (health <= 0)
        {
            Instantiate(destructParticle, transform.position, destructParticle.transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

    void TurnOffHealthBar()
    {
        if (healthBar.gameObject.activeSelf)
        {
            if (_healthBarActiveTime > 0)
            {
                _healthBarActiveTime -= Time.deltaTime;
            }
            else if (_healthBarActiveTime <= 0)
            {
                healthBar.gameObject.SetActive(false);
                _healthBarActiveTime = healthBarActiveTime;
            }
        }
    }
}
