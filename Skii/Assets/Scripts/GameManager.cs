using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();
    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private bool racing = false;
    private TimeSpan bestTime;
    [SerializeField] private int penaltyTimeVal;
    [SerializeField] private TMP_Text raceTimeText, bestTimeText;
    [SerializeField] private string bestTimeKey = "LVLBestTime";


    private void OnEnable()
    {
       
        StartGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceFinish;
        SlalonFlag.RacePenalty += AddRacePenalty;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(bestTimeKey))
        {
            int bestTimeTicks = PlayerPrefs.GetInt(bestTimeKey);
            bestTime= new TimeSpan(bestTimeTicks);
            bestTimeText.text = " BEST TIME: " + bestTime.ToString("ss\\:ff");
        }
        else
        {
            bestTime= new TimeSpan(int.MaxValue);
            bestTimeText.text = " BEST TIME: --:--";
        }
    
        Debug.Log("best time: " + bestTime.ToString());

    }
    void AddRacePenalty()
    {
        penaltyTime+= new TimeSpan(0,0,penaltyTimeVal);
    }
    void OnRaceStart()
    {
        racing = true;
        raceStart = DateTime.Now;
   
    }
    


    void OnRaceFinish()
    {
        Debug.Log("Race finish");
        racing = false;
        if(raceTime<bestTime)
        {
            bestTime = raceTime;
            bestTimeText.text = " BEST TIME: " + raceTime.ToString("ss\\:ff");
            bestTimeText.color = Color.yellow;
            PlayerPrefs.SetInt(bestTimeKey,(int) bestTime.Ticks);
            PlayerPrefs.Save();
        }

    }
    private void Update()
    {
        if (racing)
        {
            raceTime = DateTime.Now - raceStart;
            
        }
        Debug.Log("Race time " + raceTime + penaltyTime);
        raceTimeText.text = "TIME: " + raceTime.ToString("ss\\:ff");
    }

}
