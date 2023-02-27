using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerBox : MonoBehaviour
{
    [SerializeField]
    int damageAmount = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLogic playerLogic = other.GetComponent<PlayerLogic>();
            playerLogic.TakeDamage(damageAmount);
        }
    }
}
