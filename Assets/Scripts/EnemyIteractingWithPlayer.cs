using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIteractingWithPlayer : MonoBehaviour {

    public string EnemyName;
    public int Attack = 1;
    public float Health = 1.0f;
    public bool IsAlive;

    public GameObject[] PickUps;
    private GameObject PickUpPrefab;

    private Animator _anim;

    void Awake ()
    {
        _anim = GetComponent<Animator>();

        IsAlive = true;
        _anim.SetBool("isAlive", IsAlive);
    }
	
	
	void Update ()
    {
   
    }

    public void ReceiveDamage(int takenDamage)
    {
        Health -= takenDamage;

        var healthBar = transform.Find("HealthCanvas").Find("HealthBar").GetComponent<BarAnimator>();
        healthBar.AnimateBar(takenDamage);

        if (Health <= 0)
        {
            StartCoroutine(WanishingAnimation());
        }
    }

    private IEnumerator WanishingAnimation()
    {
        IsAlive = false;
        _anim.SetBool("isAlive", IsAlive);
        yield return null;

        yield return new WaitForSeconds(.3f);

        Destroy(gameObject);

        InstantiateCoin();           

        yield return null;

    }

    public void InstantiateCoin()        
    {
        var index =  Random.Range(0, PickUps.Length);
        PickUpPrefab = PickUps[index];

        Instantiate(PickUpPrefab, transform.position, Quaternion.identity);
    }

}
