using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();
    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private bool racing;
    [SerializeField] private int penaltyTimeVal;


    private void OnEnable()
    {
       
        StartGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceFinish;
        SlalonFlag.RacePenalty += AddRacePenalty;
    }
    void AddRacePenalty()
    {
        penaltyTime+= new TimeSpan(0,0,penaltyTimeVal);
    }
    void OnRaceStart()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Race Started");
    }
    


    void OnRaceFinish()
    {
        Debug.Log("Race finish");
        racing = false;

    }
    private void Update()
    {
        if (racing)
        {
            raceTime = DateTime.Now - raceStart;
            
        }
        Debug.Log("Race time " + raceTime + penaltyTime);

    }

}
