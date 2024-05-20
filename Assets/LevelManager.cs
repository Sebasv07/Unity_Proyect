using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool mission1Completed;
    public bool mission2Completed;
    public bool mission3Completed;
    public bool mission4Completed;
    public bool Juego_Finalizado;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
