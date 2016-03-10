using UnityEngine;
using System.Collections;

public class Battle_Manager : MonoBehaviour {
    
    public enum Battle_Manager_State
    {
        Pre,
        Load,
        Battle,
        Post
    }

    public Battle_Manager_State currentState;

    public GameObject creaturePrefab;

    public GameObject loading;
    public GameObject loadButton;
    public GameObject loadPanelContent;
    public GameObject creaturePanel;

    public Creature creatureA;
    public Creature creatureB;

    public Vector2 creatureAStartPos;
    public Vector2 creatureBStartPos;

    void Awake()
    {
        currentState = Battle_Manager_State.Pre;
    }

    void Update()
    {
        if (currentState == Battle_Manager_State.Battle)
        {
            switch (creatureA.myBattle.currentState)
            {
                case Creature_Battle.CreatureBattleState.Start:
                    creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                    creatureA.myBattle.battleAnimator.SetTrigger("walk");
                    break;

                case Creature_Battle.CreatureBattleState.Move:
                    if (Vector2.Distance(creatureA.transform.position,creatureB.transform.position) < creatureA.myBattle.attackDistance)
                    {
                        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Attack;
                        creatureA.myBattle.battleAnimator.SetTrigger("punch");
                        StartCoroutine("CreatureAAttack");
                        break;
                    }
                    creatureA.transform.position = Vector2.MoveTowards(creatureA.transform.position, creatureB.transform.position, 2*Time.deltaTime);
                    break;

                case Creature_Battle.CreatureBattleState.Attack:

                    break;

                case Creature_Battle.CreatureBattleState.Knockback:
                    if (creatureA.myBattle.iterator > 5 && creatureA.GetComponent<Rigidbody2D>().velocity.magnitude < 1)
                    {
                        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                        creatureA.myBattle.battleAnimator.SetTrigger("walk");
                        creatureA.myBattle.iterator = 0;
                    }
                    else
                    {
                        creatureA.myBattle.iterator++;
                    }
                    break;

                case Creature_Battle.CreatureBattleState.Stun:
                    if(creatureA.myBattle.iterator > 60 - creatureA.speed)
                    {
                        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                        creatureA.myBattle.battleAnimator.SetTrigger("walk");
                        creatureA.myBattle.iterator = 0;
                    }
                    else
                    {
                        creatureA.myBattle.iterator++;
                    }
                    break;

                case Creature_Battle.CreatureBattleState.Dead:

                    break;

                case Creature_Battle.CreatureBattleState.End:

                    break;
            }

            //creature B
            switch (creatureB.myBattle.currentState)
            {
                case Creature_Battle.CreatureBattleState.Start:
                    creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                    creatureB.myBattle.battleAnimator.SetTrigger("walk");
                    break;

                case Creature_Battle.CreatureBattleState.Move:
                    if (Vector2.Distance(creatureB.transform.position, creatureA.transform.position) < creatureB.myBattle.attackDistance+1)
                    {
                        creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Attack;
                        creatureB.myBattle.battleAnimator.SetTrigger("punch");
                        StartCoroutine("CreatureBAttack");
                        break;
                    }
                    creatureB.transform.position = Vector2.MoveTowards(creatureB.transform.position, creatureA.transform.position, 2 * Time.deltaTime);
                    break;

                case Creature_Battle.CreatureBattleState.Attack:

                    break;

                case Creature_Battle.CreatureBattleState.Knockback:
                    if (creatureB.myBattle.iterator > 5 && creatureB.GetComponent<Rigidbody2D>().velocity.magnitude < 1)
                    {
                        creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                        creatureB.myBattle.battleAnimator.SetTrigger("walk");
                        creatureB.myBattle.iterator = 0;
                    }
                    else
                    {
                        creatureB.myBattle.iterator++;
                    }
                    break;

                case Creature_Battle.CreatureBattleState.Stun:
                    if (creatureB.myBattle.iterator > 60 - creatureB.speed)
                    {
                        creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                        creatureB.myBattle.battleAnimator.SetTrigger("walk");
                        creatureB.myBattle.iterator = 0;
                    }
                    else
                    {
                        creatureB.myBattle.iterator++;
                    }
                    break;

                case Creature_Battle.CreatureBattleState.Dead:

                    break;

                case Creature_Battle.CreatureBattleState.End:

                    break;
            }
        }
    }

    IEnumerator CreatureAAttack()
    {
        yield return new WaitForSeconds(0.4f);
        if (creatureA.myBattle.currentState == Creature_Battle.CreatureBattleState.Attack)
        {
            creatureB.myBattle.currentHealth -= 10;
            if (creatureB.myBattle.currentHealth <= 0)
            {
                creatureB.myBattle.battleAnimator.SetTrigger("death");
                creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Dead;
                creatureA.myBattle.battleAnimator.ResetTrigger("walk");
                creatureA.myBattle.battleAnimator.ResetTrigger("punch");
                creatureA.myBattle.battleAnimator.ResetTrigger("hit");
                creatureA.myBattle.battleAnimator.SetTrigger("idle");
                creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.End;
                StopCoroutine("CreatureAAttack");
                StopCoroutine("CreatureBAttack");
            }
            else
            {
                creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Knockback;
                creatureB.myBattle.battleAnimator.SetTrigger("hit");
                creatureB.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0));
                creatureA.myBattle.battleAnimator.SetTrigger("idle");
            }
            StopCoroutine("CreatureBAttack");
        }
        yield return new WaitForSeconds(0.8f);
        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
        creatureA.myBattle.battleAnimator.SetTrigger("walk");
        yield return null;
    }
    
    IEnumerator CreatureBAttack()
    {
        yield return new WaitForSeconds(0.4f);
        if (creatureB.myBattle.currentState == Creature_Battle.CreatureBattleState.Attack)
        {
            creatureA.myBattle.currentHealth -= 10;
            if(creatureA.myBattle.currentHealth <= 0)
            {
                creatureA.myBattle.battleAnimator.SetTrigger("death");
                creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Dead;
                creatureB.myBattle.battleAnimator.ResetTrigger("walk");
                creatureB.myBattle.battleAnimator.ResetTrigger("punch");
                creatureB.myBattle.battleAnimator.ResetTrigger("hit");
                creatureB.myBattle.battleAnimator.SetTrigger("idle");
                creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.End;
                StopCoroutine("CreatureAAttack");
                StopCoroutine("CreatureBAttack");
            }
            else
            {
                creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Knockback;
                creatureA.myBattle.battleAnimator.SetTrigger("hit");
                creatureA.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 0));
                creatureB.myBattle.battleAnimator.SetTrigger("idle");
            }
            StopCoroutine("CreatureAAttack");
        }
        yield return new WaitForSeconds(0.8f);
        creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
        creatureB.myBattle.battleAnimator.SetTrigger("walk");
        yield return null;
    }

    public void OpenLoad()
    {
        currentState = Battle_Manager_State.Load;
        for (int i = 0; i < GameSave.current.creatures.Count; i++)
        {
            loadPanelContent.GetComponent<RectTransform>().sizeDelta = new Vector2(loadPanelContent.GetComponent<RectTransform>().sizeDelta.x, 200 + (i * 200));
            GameObject c = Instantiate(creaturePanel);
            c.transform.SetParent(loadPanelContent.transform, false);
            c.transform.localPosition = new Vector2(0, 0);
            c.GetComponent<Creature_UI>().stats = GameSave.current.creatures[i];
            c.GetComponent<Creature_UI>().InitUI();
        }
        loading.SetActive(true);
        loadButton.SetActive(false);
    }

    public void StartBattle()
    {
        if (creatureA != null && creatureB != null && currentState == Battle_Manager_State.Load)
        {
            currentState = Battle_Manager_State.Battle;
            loading.SetActive(false);
            creatureA.myBattle.currentHealth = 100;
            creatureB.myBattle.currentHealth = 100;
            creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Start;
            creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Start;
        }
    }

}
