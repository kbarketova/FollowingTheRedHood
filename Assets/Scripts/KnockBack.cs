using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    public float KnockBackSpeed = 5.0f;
    public float KnockTime = 0.5f;
    private int _damage;

    private void Start()
    {
        _damage = transform.parent.GetComponent<EnemyIteractingWithPlayer>().Attack;
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<Rigidbody2D>() != null)  
            {
                HitSomeObject(other);
            }
        }
    }


    private void HitSomeObject(Collider2D other)
    {
        var objToKnock = other.GetComponent<Rigidbody2D>();

        if (objToKnock != null)
        {
            Vector2 difference = objToKnock.transform.position - transform.position;
            difference = difference.normalized * KnockBackSpeed;

            objToKnock.AddForce(difference, ForceMode2D.Impulse);

            if(other.gameObject.CompareTag("Enemy"))
            {
              StartCoroutine(DamageEnemy(objToKnock));
            }
            else
            {
              StartCoroutine(DamagePlayer(objToKnock));
            }         
        }
    }

    private IEnumerator KnockEnemy(Rigidbody2D objToKnock)
    {
        if (objToKnock != null)
        {
            yield return new WaitForSeconds(KnockTime);

            objToKnock.velocity = Vector2.zero;
        }
    }

    private IEnumerator KnockPlayer(Rigidbody2D objToKnock)
    {
        if (objToKnock != null)
        {
            var renderer = objToKnock.transform.GetComponent<Renderer>();
            renderer.material.color = Color.red;

            yield return new WaitForSeconds(KnockTime);

            renderer.material.color = Color.white;
            objToKnock.velocity = Vector2.zero;
        }
    }

    private IEnumerator DamageEnemy(Rigidbody2D other)
    {
        yield return StartCoroutine(KnockEnemy(other));   //Продолжить по завершению другого корутина

        other.GetComponent<EnemyIteractingWithPlayer>().ReceiveDamage(_damage);
    }

    private IEnumerator DamagePlayer(Rigidbody2D objToKnock)
    {
        yield return StartCoroutine(KnockPlayer(objToKnock));   //Продолжить по завершению другого корутина

        var player = objToKnock.GetComponent<PlayerBehaviour>();

        StartCoroutine(player.ReceiveDamage(_damage));
    }

}
