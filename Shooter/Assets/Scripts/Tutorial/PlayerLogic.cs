using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    CharacterController m_controller;

    int health = 100;

    float m_movementSpeed = 5f;
    float m_horizontalInput;
    float m_verticalInput;
    float m_jumpHeight = 0.6f;
    float m_gravity = 0.05f;

    Vector3 m_movementInput;
    Vector3 m_movement;

    bool m_jump = false;

    GameObject interactiveObject = null;
    GameObject equippedObject = null;

    [SerializeField]
    Transform weaponPosition;

    [SerializeField]
    TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if(!m_jump && Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            if (!equippedObject && interactiveObject)
            {
                GunLogic gun = interactiveObject.GetComponent<GunLogic>();

                if (gun)
                {
                    interactiveObject.transform.parent = transform;
                    interactiveObject.transform.position = weaponPosition.position;
                    interactiveObject.transform.rotation = weaponPosition.rotation;
                    gun.Equip();

                    equippedObject = interactiveObject;
                }
            }
            else if(equippedObject)
            {
                GunLogic gun = equippedObject.GetComponent<GunLogic>();

                if (gun)
                {
                    equippedObject.transform.parent = null;
                    gun.Unequip();

                    equippedObject = null;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        m_movement = m_movementInput * m_movementSpeed * Time.deltaTime;

        RotateCharacterTowardsMouseCursor();
        // Face movement direction
        /*if (m_movementInput != Vector3.zero)
        {
            transform.forward = Quaternion.Euler(0, -90f, 0) * m_movementInput.normalized;
        }*/
        // Apply gravity
        if (!m_controller.isGrounded)
        {
            if(m_movement.y > 0)
            {
                m_movement.y -= m_gravity;
            }
            else
            {
                m_movement.y -= m_gravity * 1.5f;
            }
        }
        else
        {
            m_movement.y = 0;
        }

        // Adding jump height to movement y
        if (m_jump)
        {
            m_movement.y = m_jumpHeight;
            m_jump = false;
        }
        if(m_controller)
            m_controller.Move(m_movement);
    }
    void RotateCharacterTowardsMouseCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - playerPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            interactiveObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon") && interactiveObject == other.gameObject)
        {
            interactiveObject = null;
        }
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        SetHealthText();

        if(health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
