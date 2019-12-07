using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanel : MonoBehaviour {

    private MobileInput _movement;

    private void Start()
    {
       var player = GameObject.FindGameObjectWithTag("Player");

       _movement = player.GetComponent<MobileInput>();

       _movement.InputV = 0.0f;
       _movement.InputH = 0.0f;
    }

    public void Up()
    {
        Debug.Log("Up");
        _movement.InputV = 1.0f;
        _movement.InputH = 0.0f;
        _movement.AnimateWalk();
    }

    public void Down()
    {
        Debug.Log("Down");
        _movement.InputV = -1.0f;
        _movement.InputH = 0.0f;
        _movement.AnimateWalk();
    }

    public void Left()
    {
        Debug.Log("Left");
        _movement.InputV = 0.0f;
        _movement.InputH = -1.0f;
        _movement.AnimateWalk();
    }

    public void Right()
    {
        Debug.Log("Right");
        _movement.InputV = 0.0f;
        _movement.InputH = 1.0f;
        _movement.AnimateWalk();
    }

    public void Attack()
    {
        Debug.Log("Attack");
        _movement.Attack();
    }

    public void Stop()
    {
        Debug.Log("Stop Walking!");
        _movement.StopWalking();
    }
}
