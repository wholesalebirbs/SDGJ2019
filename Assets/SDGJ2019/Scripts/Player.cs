using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public ParticleSystem zapParticles;

    [Range(1,1000)]
    public float maximumHealth = 50;
    private float currentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            zapParticles.Play();
            for(int i = 0; i < 6; i++)
            {
                //Debug.DrawLine(this.transform.position + this.transform.forward * 0.25f + Vector3.up * 0.5f, this.transform.position + Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward + Vector3.up * 0.5f, Color.red, 2);
                RaycastHit raycastHit;
                if (Physics.Raycast(this.transform.position + this.transform.forward * 0.25f + Vector3.up * 0.5f, Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward, out raycastHit, 1.5f))
                {
                    EnemyAgent enemy = raycastHit.transform.root.gameObject.GetComponentInChildren<EnemyAgent>();
                    if (enemy != null)
                    {
                        enemy.Stun();
                    }
                }
            }
        }
    }

    public void ApplyDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("I have died.");
    }
}
