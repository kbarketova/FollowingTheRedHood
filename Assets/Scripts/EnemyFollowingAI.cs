using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowingAI : MonoBehaviour {

    public float Speed = 1.0f;
    public float ChaseRadius = 10f;
    public float StoppingDistance = 5.0f;
    public float RetreatDistance = 4.0f;

    private Animator _anim;
    private Transform _target;

    private enum MotionDirections { Up, Down, Left, Right };
    private MotionDirections _lastState;
    private Vector2 DeltaMovementEnemy;

    void Start () {
        // вместо определения target в инспекторе, можно использовать следующий код:
        _target = GameObject.FindWithTag("Player").transform;
        _anim = GetComponent<Animator>();
       // speed = target.GetComponent<PlayerMovement>().speed;
    }
	
	void FixedUpdate () {

        DeltaMovementEnemy = Vector3.zero;

        FollowTheTarget();

    }

    void FollowTheTarget()
    {
        var distanceToTarget = Vector3.Distance(transform.position, _target.position);
        DeltaMovementEnemy = new Vector2(Mathf.Abs(transform.position.x - _target.position.x), Mathf.Abs(transform.position.y - _target.position.y));

        if (distanceToTarget <= ChaseRadius && distanceToTarget > StoppingDistance)
        {
            AnimateEnemyChase();

            transform.position = Vector3.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);     
        }
        else if (distanceToTarget < RetreatDistance)
        {
            AnimateEnemyRetreatment();;

            transform.position = Vector3.MoveTowards(transform.position, _target.position, -Speed * Time.deltaTime);    
        }
        else
        {
            ChooseDirection(_lastState);
            _anim.SetBool("isWalkingMole", false);
        }
    }

    private void AnimateEnemyChase()
    {
        _anim.SetBool("isWalkingMole", true);

        if (_target.position.x > transform.position.x && DeltaMovementEnemy.x > DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Right);
        }
        else if (_target.position.x < transform.position.x && DeltaMovementEnemy.x > DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Left);
        }

        if (_target.position.y > transform.position.y && DeltaMovementEnemy.x < DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Up);
        }
        else if (_target.position.y < transform.position.y && DeltaMovementEnemy.x < DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Down);
        }
    }

    private void AnimateEnemyRetreatment()
    {
        _anim.SetBool("isWalkingMole", true);

        if (_target.position.x < transform.position.x && DeltaMovementEnemy.x > DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Right);
        }
        else if (_target.position.x > transform.position.x && DeltaMovementEnemy.x > DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Left);
        }

        if (_target.position.y < transform.position.y && DeltaMovementEnemy.x < DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Up);
        }
        else if (_target.position.y > transform.position.y && DeltaMovementEnemy.x < DeltaMovementEnemy.y)
        {
            ChooseDirection(MotionDirections.Down);
        }
    }

    private void ChooseDirection(MotionDirections motionState)
    {
        switch (motionState)
        {
            case MotionDirections.Up:
                _anim.SetFloat("inputH", 0);
                _anim.SetFloat("inputV", 1);
                break;

            case MotionDirections.Down:
                _anim.SetFloat("inputH", 0);
                _anim.SetFloat("inputV", -1);
                break;

            case MotionDirections.Right:
                _anim.SetFloat("inputH", 1);
                _anim.SetFloat("inputV", 0);
                break;

            case MotionDirections.Left:
                _anim.SetFloat("inputH", -1);
                _anim.SetFloat("inputV", 0);
                break;
        }

        _lastState = motionState;
    }

}
