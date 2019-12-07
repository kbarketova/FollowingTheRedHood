using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSettings : MonoBehaviour {

    public int damageOfArrow = 2;

    [HideInInspector]
    public Vector2 velocity = new Vector2(0.0f, 0.0f);

    private Vector2 currentPosition, newPosition;
    private GameObject enemy;

    void Start() {
        
    }

    void Update ()
    {
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        newPosition = currentPosition + velocity * Time.deltaTime;
        DetectEnemy(damageOfArrow);

        transform.position = newPosition;
    }

    public void DetectEnemy(int damageOfArrow)
    {
        var hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (var obj in hits)
        {
            var targetObj = obj.collider.gameObject;
            if (targetObj.CompareTag("Enemy"))
            {
                enemy = targetObj;
                enemy.GetComponent<EnemyIteractingWithPlayer>().ReceiveDamage(damageOfArrow);

                Debug.Log("Enemy is down!");

                Destroy(gameObject);
                break;
            }
        }
    }
}
