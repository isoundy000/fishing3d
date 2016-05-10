using UnityEngine;
using System.Collections;

public enum GameState
{
    Login = 0,
    MainLoop
}

public class Game : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        TimeManager. GetInstance().Update(dt);
        EventManager.GetInstance().Update(dt);
        FishManager. GetInstance().Update(dt);
        UIManager.GetInstance().Update(dt);
        UICommandSystem.GetInstance().Update(dt);
    }

    void Initialize()
    {
        TimeManager. GetInstance().Initialize();
        EventManager.GetInstance().Initialize();
        FishManager. GetInstance().Initialize();
        PathConfigManager.GetInstance().Initialize();
        GameTableManager.GetInstance().Initialize();
        UIManager.GetInstance().Initialize();
        UICommandSystem.GetInstance().Initialize();

        UIManager.GetInstance().Show("IslandSelect");
    }
}
