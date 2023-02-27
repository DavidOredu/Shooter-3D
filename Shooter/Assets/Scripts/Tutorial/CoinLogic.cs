using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    AudioSource m_audioSource;

    Collider m_collider;
    MeshRenderer m_meshRenderer;

    [SerializeField]
    AudioClip m_coinSound;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_audioSource.PlayOneShot(m_coinSound);
            m_collider.enabled = false;
            m_meshRenderer.enabled = false;
        }
    }
}
