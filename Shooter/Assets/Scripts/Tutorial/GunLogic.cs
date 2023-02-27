using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunLogic : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    const float MAX_COOLDOWN = 0.5F;
    float currentCooldown;

    [SerializeField]
    int maxAmmo = 5;
    int ammoCount;

    [SerializeField]
    TextMeshProUGUI ammoText;

    [SerializeField]
    AudioClip pistolShot;
    [SerializeField]
    AudioClip pistolEmpty;
    [SerializeField]
    AudioClip pistolReload;

    AudioSource audioSource;

    bool isEquipped = false;

    Rigidbody m_rigidbody;

    Collider m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        ammoCount = maxAmmo;
        SetupAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEquipped) { return; }

        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && currentCooldown <= 0)
        {
            if (ammoCount > 0)
            {


                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * bulletPrefab.transform.rotation);
                currentCooldown = MAX_COOLDOWN;
                --ammoCount;

                SetupAmmoText();

                PlaySound(pistolShot);
            }
            else
            {
                PlaySound(pistolEmpty);
            }
        }
    }
    public void RefillAmmo()
    {
        PlaySound(pistolReload);
        ammoCount = maxAmmo;
        SetupAmmoText();
    }
    public void Equip()
    {
        isEquipped = true;

        if (m_rigidbody)
        {
            m_rigidbody.useGravity = false;
        }
        if (m_collider)
        {
            m_collider.enabled = false;
        }
    }
    public void Unequip()
    {
        isEquipped = false;

        if (m_rigidbody)
        {
            m_rigidbody.useGravity = true;
        }
        if (m_collider)
        {
            m_collider.enabled = true ;
        }
    }
    void SetupAmmoText()
    {
        ammoText.text = "Ammo: " + ammoCount;
    }
    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
