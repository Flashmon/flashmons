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
                    creatureA.myBattle.battleAnimator.SetTrigger("move");
                    break;

                case Creature_Battle.CreatureBattleState.Move:
                    if (Vector2.Distance(creatureA.transform.position,creatureB.transform.position) > creatureA.myBattle.attackDistance)
                    {
                        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Attack;
                        creatureA.myBattle.battleAnimator.SetTrigger("attack");
                    }
                    creatureA.transform.position = Vector2.MoveTowards(creatureA.transform.position, creatureB.transform.position, 1*Time.deltaTime);
                    break;

                case Creature_Battle.CreatureBattleState.Attack:

                    break;

                case Creature_Battle.CreatureBattleState.Knockback:
                    if (creatureA.myBattle.iterator > 5 && creatureA.GetComponent<Rigidbody2D>().velocity.magnitude < 1)
                    {
                        creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Move;
                        creatureA.myBattle.battleAnimator.SetTrigger("move");
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
                        creatureA.myBattle.battleAnimator.SetTrigger("move");
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
        }
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
            creatureA.myBattle.currentState = Creature_Battle.CreatureBattleState.Start;
            creatureB.myBattle.currentState = Creature_Battle.CreatureBattleState.Start;
        }
    }

}
