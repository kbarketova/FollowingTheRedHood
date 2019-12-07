using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollLoot : MonoBehaviour, ITextPanel
{
    public GameObject NoteImage;
    public GameObject ReadButton;

    public Sprite[] Notes;
    private Image _image;

    void Start()
    {
        NoteImage.SetActive(false);
    }


    void Update()
    {

    }

    public void Read()
    {
        NoteImage.SetActive(true);

        _image = NoteImage.GetComponent<Image>();

        var index = Random.Range(0, Notes.Length);
        _image.sprite = Notes[index];

        Debug.Log("Read scroll");
    }

    public void Close()
    {
      //  Debug.Log("Destroy scroll" + NoteImage.transform.parent.parent.name);
        var scroll = NoteImage.transform.parent.parent.gameObject;
        Destroy(scroll);
    }

}
