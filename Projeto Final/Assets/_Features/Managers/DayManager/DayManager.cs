using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using System.Linq;

[CreateAssetMenu(fileName = "DayManager", menuName = "ScriptableObjects/DayManager", order = 1)]
public class DayManager : ScriptableObject
{
    private GameManager gm;
    private TextMeshProUGUI hourDisplay; // Display Time
    private TextMeshProUGUI dayDisplay; // Display Day
    private Volume ppv; // this is the post processing volume
    private GameObject[] lights;
    private Light directionalLight; // Reference to the directional light

    public float tick; // Increasing the tick, increases second rate
    public float seconds;
    public int mins;
    public int hours;
    public int days = 0;
    public float startHourNight = 18;
    public float startHourDay = 6;

    public bool activateLights; // checks if lights are on

    public void Initialize()
    {
        ppv = GameManager.instance.ppv;
        hourDisplay = GameManager.instance.hourDisplay; // Display Time
        dayDisplay = GameManager.instance.dayDisplay;

        lights = GameObject.FindGameObjectsWithTag("Light");
        directionalLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();

        //Debug.Log("Lights: "+ lights.Length);

        hours = 17;
    }

    public void CalcTime() // Used to calculate sec, min and hours
    {
        seconds += Time.fixedDeltaTime * tick; // multiply time between fixed update by tick

        if (seconds >= 60) // 60 sec = 1 min
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60) //60 min = 1 hr
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24) //24 hr = 1 day
        {
            hours = 0;
            days += 1;
        }

        ControlPPV(); // changes post processing volume after calculation
        UpdateSun(); // Update the sun's position and state
    }

    public void ControlPPV() // used to adjust the post processing slider.
    {
        if (hours >= startHourNight && hours < startHourNight + 1) // dusk at 18:00 / 6pm    -   until 19:00 / 7pm
        {
            // troquei de 60 para 120 para weight não ser 1 e ficar num breu...
            ppv.weight = (float)mins / 120; // since dusk is 1 hr, we just divide the mins by 60 which will slowly increase from 0 - 1 

            if (activateLights == false) // if lights haven't been turned on
            {
                if (mins > 45) // wait until pretty dark
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true); // turn them all on
                    }
                    activateLights = true;
                }
            }
        }

        if (hours >= startHourDay - 1 && hours < startHourDay) // Dawn at 5:00 / 5am    -   until 6:00 / 6am
        {
            ppv.weight = .5f - (float)mins / 120; // we minus 1 because we want it to go from 1 - 0

            if (activateLights == true) // if lights are on
            {
                if (mins > 45) // wait until pretty bright
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false); // shut them off
                    }
                    activateLights = false;
                }
            }
        }
    }

    private void UpdateSun()
    {
        if (hours >= startHourDay && hours < startHourNight)
        {
            if (!directionalLight.gameObject.activeSelf)
            {
                directionalLight.gameObject.SetActive(true);
            }

            float timeProgress = ((hours - 6) * 60 + mins) / (12f * 60f); // Calculate the progress of the day (0 to 1)
            float sunAngle = Mathf.Lerp(0, 180, timeProgress); // Calculate the sun's angle based on the progress
            directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170, 0); // Rotate the sun
        }
        else
        {
            if (directionalLight.gameObject.activeSelf)
            {
                directionalLight.gameObject.SetActive(false);
            }
        }
        if (hours >= startHourNight && !GameManager.instance.waveSpawned)
        {
            GameManager.instance.NewWave();
        }
        else if (hours >= startHourDay && hours < startHourNight && GameManager.instance.waveSpawned) // apagando wave de dia
        {
            GameManager.instance.wave.GetComponent<Wave>().DestroyWave();
        }
    }

    public void DisplayTime() // Shows time and day in UI
    {
        hourDisplay.text = string.Format("Hours: {0:00}:{1:00}", hours, mins); // The formatting ensures that there will always be 0's in empty spaces
        dayDisplay.text = "Day: " + days; // display day counter
    }
}