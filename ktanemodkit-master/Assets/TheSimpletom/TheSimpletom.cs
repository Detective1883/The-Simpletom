using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using UnityEditor;

public class TheSimpletom : MonoBehaviour {


    [SerializeField]
    private bool debug;

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;
   private string buttonName;
   private int buttonCount;
   private int buttonPreses = 0;

    [SerializeField]
    KMSelectable button;

   void Awake () {
      ModuleId = ModuleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */
        

        button = transform.Find("The Everything/Button HL").GetComponent<KMSelectable>();

        button.OnInteract += delegate () { ButtonPress(); return false; };
        

        string[] labels = {"The Simpletom", "The Simpletim", "The Simplebob", "The Simplejack", "The Simplejane"};
        TextMesh textMesh = transform.Find("The Everything/Claw Text").GetComponent<TextMesh>();
        buttonName = labels[Rnd.Range(0, labels.Length)];

        if (debug)
        {
            buttonName = labels[0];
        }
        textMesh.text = buttonName.Replace(" ", "\n");
    }

    private void ButtonPress()
    {
        if (ModuleSolved)
            return;

        buttonPreses++;
        Log($"The button was pressed {buttonPreses} times");
        Solve();
    }

    void Start()
    {
        Log($"The button was labled {buttonName}");

        switch (buttonName)
        {
            case "The Simpletom":
                int indicatorCount = GetComponent<KMBombInfo>().GetIndicators().Count();
                buttonCount = Modulo(4 * indicatorCount - 3, 4);
                Log($"To solve you must press the button {buttonCount} times");
                break;

            default:
                Debug.Log("Not Implemented");
                break;
        }

        
   }

   void Update () {
        
    }

    public void Log(string s)
    {
        Debug.Log($"[The Simpletom #{ModuleId}] {s}");
    }

    private int Modulo(int value, int mod)
    {
        return ((value % mod) + mod) % mod;
    }

    private void Solve()
    {
        GetComponent<KMBombModule>().HandlePass();
        ModuleSolved = true;
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
