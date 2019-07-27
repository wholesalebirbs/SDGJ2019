using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public ParticleSystem zapParticles;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            zapParticles.Play();
            for(int i = 0; i < 6; i++)
            {
                //Debug.DrawLine(this.transform.position + this.transform.forward * 0.5f, this.transform.position + Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward, Color.red, 2);
                RaycastHit raycastHit;
                if (Physics.Raycast(this.transform.position + this.transform.forward * 0.5f, Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward, out raycastHit))
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
}
