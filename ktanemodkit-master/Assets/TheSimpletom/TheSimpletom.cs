using System.Collections;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

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


        string[] labels = { "The Simpletom", "The Simpletim", "The Simplebob", "The Simplejack", "The Simplejane" };
        TextMesh textMesh = transform.Find("The Everything/Claw Text").GetComponent<TextMesh>();
        buttonName = labels[Rnd.Range(0, labels.Length)];

        if (debug)
        {
            buttonName = labels[2];
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
