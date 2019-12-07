using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    PlayerBehaviour _player;
    public GameObject ArrowPrefab;

    public float SpeedOfArrow = 7f;

    void Start ()
    {

        _player = GetComponent<PlayerBehaviour>();
	}
	

	void FixedUpdate ()
    {
        
    }

    public IEnumerator InstantiateArrow(Transform placeToSpawn, Quaternion placeToSpawnRotation)
    {
        //new WaitForSeconds(.6f);
        //yield return null;
        GameObject arrow = Instantiate(ArrowPrefab, placeToSpawn.position, placeToSpawnRotation);

        var arrowSettings = arrow.GetComponent<ArrowSettings>();
        arrowSettings.velocity = new Vector2(placeToSpawn.localPosition.x, placeToSpawn.localPosition.y).normalized * SpeedOfArrow;
        
        Destroy(arrow, 3f);
        yield return null;
    }

    public IEnumerator ToShoot(Transform placeToSpawn, Quaternion placeToSpawnRotation)
    {
        _player.State = PlayerBehaviour.PlayerStates.Attacking;
        _player.Anim.SetBool("isAttacking", true);
      //  StartCoroutine(InstantiateArrow(placeToSpawn, placeToSpawnRotation));

        yield return null;

        yield return new WaitForSeconds(.6f);
        StartCoroutine(InstantiateArrow(placeToSpawn, placeToSpawnRotation));

        _player.Anim.SetBool("isAttacking", false);
        _player.State = PlayerBehaviour.PlayerStates.Idling;

        yield return null;
    }
}
