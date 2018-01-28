using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int curHP;
	public int maxHP = 50;
	public GameManager gameloop;
    public SoundManager soundManager;
	public GameObject mana;
	public GameObject health;
    private Soldier solider;

    public SkinnedMeshRenderer[] rends;

    public Color HitColor;

    // Use this for initialization
    void Start () {
		curHP = maxHP;
        solider = GetComponent<Soldier>();

        if (solider.isAlberto)
        {
            soundManager.play(soundManager.albertoSsup);
        }
	}

	private void Awake()
	{
		gameloop = GameObject.Find("GM").GetComponent <GameManager> ();
		soundManager = GameObject.Find("SM").GetComponent <SoundManager> ();
    }

	public void TakeDamage(int amt)
    {
        solider.anim.SetTrigger("Hit");
        StartCoroutine("FlashRed");
		if (curHP > 0)
        {
            soundManager.playRandomFrom(soundManager.soldierHits);
			curHP -= amt;
			if (curHP <= 0)
            {
				curHP = 0;
				Die ();
			}
		}
	}

	void Die()
    {
		if (gameloop)
        {
			gameloop.Souls++;
			gameloop.numOfEnemies--;
		}
		Debug.Log(mana);

        //Destroy (gameObject);
        solider.Die();
		//transform.position = transform.position.z - 10;
		Instantiate(mana, transform.position , transform.rotation);
		Instantiate(health, transform.position , transform.rotation);
	}

    IEnumerator FlashRed()
    {
        print("FLASH!");
        foreach (SkinnedMeshRenderer mat in rends)
        {
            mat.material.SetColor("_Highlight", HitColor);
        }
        yield return new WaitForSeconds(.25f);

        foreach (SkinnedMeshRenderer mat in rends)
        {
            mat.material.SetColor("_Highlight", new Color(0, 0, 0, 0));
        }
    }
}
