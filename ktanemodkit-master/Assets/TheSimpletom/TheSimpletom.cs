using System.Collections;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using NUnit.Framework;
using System.Collections.Generic;

public class TheSimpletom : MonoBehaviour
{
    [SerializeField]
    private bool debug;

    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;
    private string buttonName;
    private int buttonCount;
    private int buttonPresses = 0;
    private float timer = 0;
    IEnumerator SBTimer;
    IEnumerator morseDashCoroutine;
    const int morseDash = 2;
    const int morseReset = 5;
    const int morseBreak = 1;
    List<string> morseValues;

    KMSelectable button;
    KMBombModule status;

    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */


        button = transform.Find("The Everything/Button HL").GetComponent<KMSelectable>();
        status = GetComponent<KMBombModule>();


        button.OnInteract += delegate () { ButtonPress(); return false; };
        button.OnInteractEnded += delegate () { ButtonRelease(); };


        string[] labels = { "The Simpletom", "The Simpletim", "The Simplebob", "The Simplejack", "The Simplejill" };
        TextMesh textMesh = transform.Find("The Everything/Claw Text").GetComponent<TextMesh>();
        buttonName = labels[Rnd.Range(0, labels.Length)];

        morseValues = new List<string>();

        if (debug)
        {
            buttonName = labels[3];
        }
        textMesh.text = buttonName.Replace(" ", "\n");
    }

    private void ButtonPress()
    {
        //when butt is presssed, set timer to 0 amd start timer
        //when timer == 3, if button presses is answer, solve mod, otherwise strike
        if (ModuleSolved)
            return;
        if (buttonName == "The Simpletom")
        {
            if (buttonPresses == 0)
                StartCoroutine(SimpletomTimer());
            buttonPresses++;

            Log($"The button was pressed {buttonPresses} times");
        }
        if (buttonName == "The Simplebob")
        {
            SBTimer = SimplebobTimer();
            StartCoroutine(SBTimer);
        }
        if (buttonName == "The Simplejack" || buttonName == "The Simplejill")
        {
            morseDashCoroutine = MorseDashCoroutine();
            StartCoroutine(morseDashCoroutine);
        }
    }

    private void ButtonRelease()
    {
        if (buttonName == "The Simplebob")
        {
            StopCoroutine(SBTimer);
            Log($"You held the button for {timer.ToString("#.##")} seconds");
            if (3 < timer && timer < 4)
            {
                Solve();
            }
            else
            {
                status.HandleStrike();
                timer = 0;
            }
        }
        if (buttonName == "The Simplejack" || buttonName == "The Simplejill")
        {
            StopCoroutine(morseDashCoroutine);
            Debug.Log(timer);
            if (morseDash < timer)
            {
                Debug.Log("dash");
                morseValues[morseValues.Count - 1] = "dash";
            }
            timer = 0;
            Debug.Log(string.Join(" ", morseValues.ToArray()));
        }
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
            case "The Simplebob":
                Log($"To solve you must hold the button for 3 seconds");
                break;
            case "The Simplejack":
            case "The Simplejill":
                Log($"To solve you must submit 'HILL' in Morse Code");
                break;
            default:
                Debug.Log("Not Implemented");
                break;
        }


    }

    void Update()
    {

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
        status.HandlePass();
        ModuleSolved = true;
    }
    
    private IEnumerator SimpletomTimer()
    {
        float timer = 0;
        while (timer < 3)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        if (buttonPresses == buttonCount)
        {
            Solve();
        }
        else
        {
            status.HandleStrike();
        }
    }
    private IEnumerator SimplebobTimer()
    { 
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;
        }
    }
    private IEnumerator MorseDashCoroutine()
    {
        morseValues.Add("dot");
        while(true)
        {
            yield return null;
            timer += Time.deltaTime;
        }

    }

#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}
