using UnityEngine;
using System.Collections;

public class Creature_GameObjects : MonoBehaviour {

    public GameObject raisingSprites;
    public GameObject battleSprites;

    public Animator raisingAnimator;
    public Animator battleAnimator;

    public SpriteRenderer[] baseSpritesRaising;
    public SpriteRenderer[] tintSpritesRaising;
    public SpriteRenderer[] eyeSpritesRaising;

    public SpriteRenderer[] baseSpritesBattle;
    public SpriteRenderer[] tintSpritesBattle;
    public SpriteRenderer[] eyeSpritesBattle;

}
