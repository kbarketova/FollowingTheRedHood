using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour, IPlatformsInput
{
    private PlayerBehaviour _player;

    [HideInInspector]
    public float InputH;
    [HideInInspector]
    public float InputV;         

    private Vector3 DeltaMovement;

    private Animator _anim;
    private Rigidbody2D _rb;

    private Shoot _attackWithBow;

    private Transform _spawnPoint;
    private Quaternion _spawnPointRotation;     

    public float Speed = 40f;

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

    }

    public void AnimateWalk()
    {
       DeltaMovement.x = InputH;
       DeltaMovement.y = InputV;

       if (DeltaMovement != Vector3.zero)
       {
          Move();
         _anim.SetFloat("inputH", InputH);
         _anim.SetFloat("inputV", InputV);
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
        SetArrowSpawnPoint();

        if (DeltaMovement == Vector3.zero && _player.State != PlayerBehaviour.PlayerStates.Attacking)
        {
            StartCoroutine(_attackWithBow.ToShoot(_spawnPoint, _spawnPointRotation));
        }
    }

    public void Move()
    {
      _rb.velocity = new Vector2(InputH, InputV) * Speed;
    }

    public void SetArrowSpawnPoint()
    {
        if (InputV < 0 || (InputV == 0 && InputH == 0))          // игрок идет вниз
        {
            _spawnPoint = transform.GetChild(0);
            _spawnPointRotation = Quaternion.Euler(180, 0, 0);
        }
        if (InputV > 0)          // игрок идет вверх
        {
            _spawnPoint = transform.GetChild(1);
            _spawnPointRotation = Quaternion.Euler(0, 0, 0);
        }
        if (InputH > 0)          // игрок идет вправо
        {
            _spawnPoint = transform.GetChild(2);
            _spawnPointRotation = Quaternion.Euler(0, 0, -90);
        }
        if (InputH < 0)         // игрок идет влево
        {
            _spawnPoint = transform.GetChild(3);
            _spawnPointRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    public void StopWalking()   
    {
         DeltaMovement = Vector3.zero;
        _rb.velocity = Vector3.zero;

        _anim.SetFloat("inputH", InputH);
        _anim.SetFloat("inputV", InputV);

        _anim.SetBool("isWalking", false);
        _player.State = PlayerBehaviour.PlayerStates.Idling;     
    }
}
