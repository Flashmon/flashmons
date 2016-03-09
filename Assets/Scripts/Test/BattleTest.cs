using UnityEngine;
using System.Collections;

public class BattleTest : MonoBehaviour {

    public Creature creatureA;
    public Creature creatureB;

    bool creatureAAttacking;
    bool creatureBAttacking;

    bool creatureADead;
    bool creatureBDead;

    bool creatureAKnockback;
    bool creatureBKnockback;

    private int creatureADelay = 200;
    private int creatureBDelay = 200;

    private int creatureATimer;
    private int creatureBTimer;

    private int attackDelayA;
    private int attackDelayB;

    public float attackDistance;

    private IEnumerator aRoutine;
    private IEnumerator bRoutine;

    public Manager_ScreenShake screenShake;
    public Manager_SlowMotion slowMo;
    public Manager_HitPause hitPause;

    public BattleState currentState;

    public enum BattleState
    {
        Pre,
        Battle,
        Post
    }

	// Use this for initialization
	void Start ()
    {
        currentState = BattleState.Pre;
        attackDelayA = Random.Range(0, 20);
        attackDelayB = Random.Range(0, 19);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (currentState == BattleState.Battle)
        {
            Debug.Log(creatureA.GetComponent<Rigidbody2D>().velocity.magnitude);
            Debug.Log(creatureB.GetComponent<Rigidbody2D>().velocity.magnitude);
            if (creatureAKnockback == true)
            {
                if (creatureA.GetComponent<Rigidbody2D>().velocity.magnitude < 1)
                {
                    creatureAKnockback = false;
                }
            }
            else
            {
                creatureATimer--;
                if (creatureATimer < 0)
                {
                    if (Vector3.Distance(creatureA.transform.position, creatureB.transform.position) > attackDistance)
                    {
                        creatureA.transform.position = Vector3.MoveTowards(creatureA.transform.position, creatureB.transform.position, 1 * Time.deltaTime);
                    }
                    else
                    {
                        attackDelayA--;
                        if (attackDelayA <= 0 && creatureAAttacking == false && creatureADead == false && creatureAKnockback == false)
                        {
                            Debug.Log(creatureA.name + " attacking!");
                            creatureAAttacking = true;
                            aRoutine = StartAttack(creatureA, creatureB);
                            StartCoroutine(aRoutine);
                        }
                    }
                }
            }
            if (creatureBKnockback == true)
            {
                if (creatureB.GetComponent<Rigidbody2D>().velocity.magnitude < 1)
                {
                    creatureBKnockback = false;
                }
            }
            else
            {
                creatureBTimer--;
                if (creatureBTimer < 0)
                {
                    if (Vector3.Distance(creatureB.transform.position, creatureA.transform.position) > attackDistance)
                    {
                        creatureB.transform.position = Vector3.MoveTowards(creatureB.transform.position, creatureA.transform.position, 1 * Time.deltaTime);
                    }
                    else
                    {
                        attackDelayB--;
                        if (attackDelayB <= 0 && creatureBAttacking == false && creatureBDead == false && creatureBKnockback == false)
                        {
                            Debug.Log(creatureB.name + " attacking!");
                            creatureBAttacking = true;
                            bRoutine = StartAttack(creatureB, creatureA);
                            StartCoroutine(bRoutine);
                        }
                    }
                }
            }
        }
	}

    IEnumerator StartAttack(Creature attacker, Creature defender)
    {
        if(Vector3.Distance(attacker.transform.position, defender.transform.position) <= attackDistance)
        {
            Debug.Log(Vector3.Distance(attacker.transform.position, defender.transform.position));
            attacker.GetComponent<Animator>().SetTrigger("punch");

            
            yield return new WaitForSeconds(.8f);
            /*if (defender.currentHealth <= (attacker.attack - (defender.defense / 2) + 1))
            {
                Debug.Log(attacker.name + " is gonna kill!");
                slowMo.SlowMo(0.1f, 0.5f, 2f);
            }*/

            yield return new WaitForSeconds(.37f);
            //defender.currentHealth = defender.currentHealth - (attacker.attack - (defender.defense / 2) + 1);
            /*if (defender.currentHealth <= 0)
            {
                Debug.Log(defender + " is dead!");
                creatureA.GetComponent<Animator>().ResetTrigger("punch");
                creatureA.GetComponent<Animator>().ResetTrigger("death");
                creatureA.GetComponent<Animator>().ResetTrigger("hit");
                creatureA.GetComponent<Animator>().ResetTrigger("victory");
                creatureB.GetComponent<Animator>().ResetTrigger("punch");
                creatureB.GetComponent<Animator>().ResetTrigger("death");
                creatureB.GetComponent<Animator>().ResetTrigger("hit");
                creatureB.GetComponent<Animator>().ResetTrigger("victory");

                if (attacker.name == creatureA.name)
                {
                    creatureBDead = true;
                    creatureA.GetComponent<Animator>().SetTrigger("victory");
                    creatureB.GetComponent<Animator>().SetTrigger("death");
                    if (bRoutine != null)
                    {
                        StopCoroutine(bRoutine);
                        bRoutine = null;
                    }
                }
                if (attacker.name == creatureB.name)
                {
                    creatureADead = true;
                    creatureB.GetComponent<Animator>().SetTrigger("victory");
                    creatureA.GetComponent<Animator>().SetTrigger("death");
                    if (aRoutine != null)
                    {
                        StopCoroutine(aRoutine);
                        aRoutine = null;
                    }
                }
                //unparent objects
                //add force
            }
            else
            {
                Debug.Log(attacker + " hit " + defender + "!");
                Debug.Log(defender + " hp at " + defender.currentHealth);
                defender.GetComponent<Animator>().ResetTrigger("punch");
                attacker.GetComponent<Animator>().ResetTrigger("punch");
                defender.GetComponent<Animator>().SetTrigger("hit");
                screenShake.ScreenShake(10f);
                defender.GetComponent<Rigidbody2D>().AddForce(new Vector2(500*defender.transform.localScale.x,0));
                hitPause.HitPause(0.1f);
                Debug.Log(attacker.name + " equals " + creatureA.name);
                if (attacker.name == creatureA.name)
                {
                    creatureBKnockback = true;
                    creatureAAttacking = false;
                    creatureATimer = Random.Range(0, creatureADelay);
                    attackDelayA = Random.Range(0, 20);
                    creatureBAttacking = false;
                    if (bRoutine != null)
                    {
                        StopCoroutine(bRoutine);
                    }
                    bRoutine = null;
                    aRoutine = null;
                    Debug.Log("stopping " + defender.name+ " attack");
                }
                else if (attacker.name == creatureB.name)
                {
                    creatureAKnockback = true;
                    creatureBAttacking = false;
                    creatureBTimer = Random.Range(0, creatureBDelay);
                    attackDelayB = Random.Range(0, 19);
                    creatureAAttacking = false;
                    if (aRoutine != null)
                    {
                        StopCoroutine(aRoutine);
                    }
                    bRoutine = null;
                    aRoutine = null;
                    Debug.Log("stopping " + defender.name + " attack");
                }
                else
                {
                    Debug.LogError("oops");
                }
                yield break;
            }*/

        }
    }
    
    void EndBattle()
    {

    }

    public void StartBattle()
    {
        creatureATimer = Random.Range(0,creatureADelay-creatureA.speed);
        creatureBTimer = Random.Range(0,creatureBDelay-creatureB.speed);
        currentState = BattleState.Battle;
    }
}
