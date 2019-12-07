using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IPlatformsInput
{
    private PlayerBehaviour _player;
    private float _inputH;
    private float _inputV;

    private Vector3 DeltaMovement;

    private Animator _anim;
    private Rigidbody2D _rb;

    private Shoot _attackWithBow;

    private Transform  _spawnPoint;
    private Quaternion _spawnPointRotation;      

    public float Speed = 2f;

    void Start()
    {
        _player = GetComponent<PlayerBehaviour>();

        _anim = _player.Anim;
        _rb = _player.Rb;

        _attackWithBow = _player.GetComponent<Shoot>();
        _spawnPoint = _player.PlaceToSpawn;
        _spawnPointRotation = _player.PlaceToSpawnRotation;
    }

    void Update()
    {
        _inputH = Input.GetAxisRaw("Horizontal");
        _inputV = Input.GetAxisRaw("Vertical");

        DeltaMovement = Vector3.zero;
        DeltaMovement.x = _inputH;
        DeltaMovement.y = _inputV;

        if (_player.State == PlayerBehaviour.PlayerStates.Idling || _player.State == PlayerBehaviour.PlayerStates.Walking)
        {
            AnimateWalk();
            Attack();
            SetArrowSpawnPoint();
        }
    }

    public void AnimateWalk()
    {       
        if (DeltaMovement != Vector3.zero)
        {
           Move();

           _anim.SetFloat("inputH", _inputH);
           _anim.SetFloat("inputV", _inputV);
           _anim.SetBool("isWalking", true);

           _player.State = PlayerBehaviour.PlayerStates.Walking;
        }
        else
        {
            _anim.SetBool("isWalking", false);

            _player.State = PlayerBehaviour.PlayerStates.Idling;
        }

    }

    public void Attack()
    {
        if (DeltaMovement == Vector3.zero && Input.GetButtonDown("Attack") && _player.State != PlayerBehaviour.PlayerStates.Attacking)
        {
            StartCoroutine(_attackWithBow.ToShoot(_spawnPoint, _spawnPointRotation));
        }
    }

    public void Move()
    {
        _rb.MovePosition(transform.position + DeltaMovement.normalized * Speed * Time.deltaTime);
    }

    public void SetArrowSpawnPoint()
    {
       if (Input.GetAxis("Vertical") < 0)          // игрок идет вниз
       {
         _spawnPoint = transform.GetChild(0);
         _spawnPointRotation = Quaternion.Euler(180, 0, 0);
       }
       if (Input.GetAxis("Vertical") > 0)          // игрок идет вверх
       {
         _spawnPoint = transform.GetChild(1);
         _spawnPointRotation = Quaternion.Euler(0, 0, 0);
       }
       if (Input.GetAxis("Horizontal") > 0)          // игрок идет вправо
       {
         _spawnPoint = transform.GetChild(2);
         _spawnPointRotation = Quaternion.Euler(0, 0, -90);
       }
       if (Input.GetAxis("Horizontal") < 0)         // игрок идет влево
       {
         _spawnPoint = transform.GetChild(3);
         _spawnPointRotation = Quaternion.Euler(new Vector3(0, 0, 90));
       }
    }

}
