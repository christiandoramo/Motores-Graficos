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

    public Camera activeCamera;
    [NonSerialized] public GameObject bubble = null;
    [SerializeField] GameObject bubblePrefab;
    [NonSerialized] public GameObject wave = null;
    [SerializeField] GameObject wavePrefab;
    public bool waveSpawned;
    GameObject wagon;

    // [SerializeField] private InputActionReference pauseResumePressed;

    void Start()
    {
        if (instance == null) instance = this;
        Time.timeScale = 1;
        dayManager = Instantiate(dayManagerOriginal);
        hudManager = Instantiate(hudManagerOriginal);
        dayManager.Initialize();
        activeCamera = Camera.main;
        wagon = GameObject.FindWithTag("Wagon");

    }
    void FixedUpdate()
    {
        dayManager.CalcTime();
        dayManager.DisplayTime();
    }

    public void CreateBubble()
    {
        bubble = Instantiate(bubblePrefab, wagon.transform.position, Quaternion.identity);
        bubble.transform.SetParent(wagon.transform, true);
    }

    public void NewWave()
    {
        waveSpawned = true;
        wave = Instantiate(wavePrefab,  wagon.transform.position, Quaternion.identity);
    }
}
