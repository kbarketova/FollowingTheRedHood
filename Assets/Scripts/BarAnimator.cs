using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarAnimator : MonoBehaviour {

    private Material _material;

    private float _realHpLeft;
    private float _realHpMax;

    void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        Image img = GetComponent<Image>();

        if (rend != null)
        {
            _material = new Material(rend.material);
            rend.material = _material;
        }
        else if (img != null)
        {
            _material = new Material(img.material);
            img.material = _material;
        }
        else
        {
            Debug.LogWarning("No Renderer or Image attached to " + name);
        }


    }

    private void Start()
    {
        _realHpMax = transform.parent.parent.GetComponent<EnemyIteractingWithPlayer>().Health;
        _realHpLeft = _realHpMax;
    }

    public void AnimateBar (int damage) {

        _realHpLeft -= damage;
        float hpBarLeft = _realHpLeft / _realHpMax;

        _material.SetFloat("_Delta", -1);
        _material.SetFloat("_Fill", hpBarLeft);
    }
}
