using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    private void Awake()
    {
        if (_instance != null) return;
        _instance = this;
        DontDestroyOnLoad(this);
    }
}