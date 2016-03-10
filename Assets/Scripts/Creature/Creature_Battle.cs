using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Creature_Battle : MonoBehaviour {

    public Animator battleAnimator;

    public int currentHealth;

    public Image healthBar;

    public Creature creature;

    public CreatureBattleState currentState;

    public int attackDistance;

    public int currentTimer;

    public int iterator = 0;

    public AudioSource punchSound;

    public enum CreatureBattleState
    {
        Start,
        Move,
        Attack,
        Knockback,
        Stun,
        Dead,
        End
    }

    void Awake()
    {
        battleAnimator.GetComponent<Animator>();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / creature.health;
    }
}
