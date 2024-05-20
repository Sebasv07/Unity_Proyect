using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        TimerAndScore timerAndScore = GameObject.FindObjectOfType<TimerAndScore>();
        if (timerAndScore != null)
        {
            timerAndScore.ResumeTimerAndScore();
        }
    }
}
