using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAI : MonoBehaviour {

    private Animator _anim;
    public float Speed = 1.5f;

    public enum MotionDirections {Horizontal, Vertical};
    public MotionDirections MotionState = MotionDirections.Horizontal;

    private bool _isAlive;

    void Awake()
    {

        _anim = GetComponent<Animator>();
        _isAlive = true;
    }

    void Update ()
    {
        _isAlive = GetComponent<EnemyIteractingWithPlayer>().IsAlive;

        if (_isAlive)
        {
            switch (MotionState)
            {
                case MotionDirections.Horizontal:

                    transform.Translate(Vector2.right * Mathf.Sin(Time.timeSinceLevelLoad / 2) * Time.deltaTime * Speed);

                    _anim.SetFloat("inputH", Mathf.Sin(Time.timeSinceLevelLoad / 2));
                    _anim.SetFloat("inputV", 0);
                    _anim.SetBool("isWalkingTreant", true);

                    break;

                case MotionDirections.Vertical:
                    transform.Translate(Vector2.up * Mathf.Sin(Time.timeSinceLevelLoad / 2) * Time.deltaTime * Speed);

                    _anim.SetFloat("inputH", 0);
                    _anim.SetFloat("inputV", Mathf.Sin(Time.timeSinceLevelLoad / 2));
                    _anim.SetBool("isWalkingTreant", true);

                    break;

            }
        }

    }
}
