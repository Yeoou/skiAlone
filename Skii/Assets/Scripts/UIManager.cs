using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup screenOverlay;
    [SerializeField] private float fadeSpeed = 2;
    [SerializeField] private GameObject raceOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenOverlay.gameObject.SetActive(true);
        raceOverPanel.SetActive(false);
        StartCoroutine(FadeOutOverlay());
    }
    private void OnEnable()
    {
        FinishGate.FinishRace += OnRaceFinished;
    }
    private void OnDisable()
    {
        FinishGate.FinishRace -= OnRaceFinished;
    }
    private void OnRaceFinished()
    {
        raceOverPanel.SetActive(true );
    }

    private IEnumerator FadeOutOverlay()
    {
        while(screenOverlay.alpha>0)
        {
            screenOverlay.alpha-= Time.deltaTime* fadeSpeed;
            yield return null;
        }
    }
    private IEnumerator FadeInOverlay()
    {
        while (screenOverlay.alpha <1)
        {
            screenOverlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    public void Restart()
    {
       StartCoroutine(RestartCoroutine());
    }
    private IEnumerator RestartCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
    public void NextLevel()
    {

    }
    public void Quit()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
