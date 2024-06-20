using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gang : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;
    public bool isDead;


    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        /*if (EnemyDie)
            return;*/
        HP -= damageAmount;

        if (HP <= 0) 
        {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0) 
            {
                animator.SetTrigger("DIE1");
                /*EnemyDie = true;*/
            }
            else
            {
                animator.SetTrigger("DIE2");
                /*EnemyDie = true;*/
            }
            
            isDead = true;

            SoundManager.Instance.enemyChannel.PlayOneShot(SoundManager.Instance.enemyDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
            SoundManager.Instance.enemyChannel.PlayOneShot(SoundManager.Instance.enemyHurt);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);

    }
}
