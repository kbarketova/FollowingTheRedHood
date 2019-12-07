using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlatformsInput
{
    void AnimateWalk();
    void Move();
    void Attack();
    void SetArrowSpawnPoint();
}

public class PlayerBehaviour : MonoBehaviour {

    [Header("Player Attributes")]
    public int Health = 5;
    public bool IsAlive = true;

    public enum PlayerStates {Walking, Idling, Hurt, Attacking};
    public PlayerStates State = PlayerStates.Idling;

    [Header("Input Settings")]
    public bool MobileInput = false;

    private MobileInput _mobileInput;
    private KeyboardInput _keyboardInput;

    [HideInInspector]
    public Animator Anim;

    [HideInInspector]
    public Rigidbody2D Rb;            

    [HideInInspector]
    public Transform PlaceToSpawn;

    [HideInInspector]
    public Quaternion PlaceToSpawnRotation;

    void Awake ()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        PlaceToSpawn = transform.GetChild(0);
        PlaceToSpawnRotation = Quaternion.Euler(180, 0, 0);

        if (MobileInput)
        {
            _mobileInput = GetComponent<MobileInput>();
            _mobileInput.enabled = true;

            _keyboardInput = GetComponent<KeyboardInput>();
            _keyboardInput.enabled = false;
        }
        else
        {
            _mobileInput = GetComponent<MobileInput>();
            _mobileInput.enabled = false;

            _keyboardInput = GetComponent<KeyboardInput>();
            _keyboardInput.enabled = true;
        }
    }
    
	void Update ()
    {

        if (Health <= 0)
        {
            Debug.Log("Player died! Oh no");
            IsAlive = false;

            GameManager.instance.GameOver();
        }

    }

    //void Flip() 
    //{
    //    if (Input.GetAxis("Horizontal") < 0)
    //     transform.localRotation = Quaternion.Euler(0, 180, 0);    // поворачиваем игрока, когда идем в обратную сторону

    //    if (Input.GetAxis("Horizontal") > 0)
    //     transform.localRotation = Quaternion.Euler(0, 0, 0);
    //}

    public IEnumerator ReceiveDamage(int takenDamage)
    {
        State = PlayerStates.Hurt;
        Health -= takenDamage;

        yield return null;
        State = PlayerStates.Idling;
    }

}
