using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    public int Value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.Collect(Value, gameObject.tag);     // передаем тег собранного лута
            }

            Destroy(gameObject);
        }
    }
}
