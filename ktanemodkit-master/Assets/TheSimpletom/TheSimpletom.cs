using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class TheSimpletom : MonoBehaviour {


    [SerializeField]
    private bool debug;

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake () {
      ModuleId = ModuleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };
        string[] labels = {"The Simpletom", "The Simpletim", "The Simplebob", "The Simplejack", "The Simplejane"};
        TextMesh textMesh = transform.Find("The Everything/Claw Text").GetComponent<TextMesh>();
        string buttonName = labels[Rnd.Range(0, labels.Length)];

        if (debug)
        {
            buttonName = labels[0];
        }
        textMesh.text = buttonName.Replace(" ", "\n");
    }

   void Start () 
   {
        int indicatorCount = GetComponent<KMBombInfo>().GetIndicators().Count();
        int buttonCount = 4 * indicatorCount - 3;
        Debug.Log(buttonCount);
   }

   void Update () {
        
    }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
