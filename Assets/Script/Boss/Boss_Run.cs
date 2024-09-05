using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    private float attackRange = 5f;
    public GameObject attackPoint;
    private bool attacking = false;

    private float timeToAttack = 0.2f;
    private float timer = 0f;

    Transform player;
    Rigidbody2D rb;
    Boss1 boss;

    public float attackCooldown = 2f;
    private float lastAttackTime = -100f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss1>();
        attackPoint = GameObject.FindGameObjectWithTag("BossAttackPoint");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, player.position.y);
        //Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        //rb.MovePosition(newPos);
        rb.velocity = player.position - boss.transform.position;

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            
            //int type = Random.Range(0, 2);
            rb.velocity = Vector2.zero;
            int type = 0;
            switch (type)
            {
                case 0:
                    timer += Time.deltaTime;
                    
                    if(timer > timeToAttack)
                    {
                        timer = 0;
/*                        boss.attackPoint.SetActive(true);
*/                    }
                    if (Time.time - lastAttackTime > attackCooldown)
                    {
                        attacking = true;
                        Debug.Log("chieu 1");
/*                        boss.attackPoint.SetActive(true);
*/                        animator.SetTrigger("isAttack");

                        lastAttackTime = Time.time;
                            
                    }
                    else
                    {
                        attacking = false;
/*                        boss.attackPoint.SetActive(false);
*/                    }

                    break;
                case 1:
                    Debug.Log("Chieu 2");
                    break;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isAttack");
    }

    IEnumerator waittingAttack()
    {
        yield return new WaitForSeconds(0.2f);
        boss.attackPoint.SetActive(true);
    }

}
