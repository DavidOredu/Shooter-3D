using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunLogic gun = other.GetComponentInChildren<GunLogic>();

            if (gun)
            {
                gun.RefillAmmo();
                Destroy(gameObject);
            }
        }
    }
}
