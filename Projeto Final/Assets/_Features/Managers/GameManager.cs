using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public DayManager dayManagerOriginal;
    [NonSerialized] public DayManager dayManager;

    public static GameManager instance;
    public Volume ppv; // this is the post processing volume
    public TextMeshProUGUI hourDisplay; // Display Time
    public TextMeshProUGUI dayDisplay;
    public ResourceManager rm;

    public bool isPaused = false;

    [NonSerialized] public HUDManager hudManager;
    public bool isDriving = false;

    public HUDManager hudManagerOriginal;

    // [SerializeField] private InputActionReference pauseResumePressed;

    void Start()
    {
        if (instance == null) instance = this;
        Time.timeScale = 1;
        dayManager = Instantiate(dayManagerOriginal);
        hudManager = Instantiate(hudManagerOriginal);
        dayManager.Initialize();
    }
    void FixedUpdate()
    {
        dayManager.CalcTime();
        dayManager.DisplayTime();
    }

}
