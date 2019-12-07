using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, ITextPanel
{
   public GameObject NoteImage;
   public GameObject ReadButton;

   private void Start()
   {
        NoteImage.SetActive(false);
   }

   public void Read()
   {
        NoteImage.SetActive(true);
   }

   public void Close()
   {
        NoteImage.SetActive(false);
        ReadButton.SetActive(false);
   }
}
