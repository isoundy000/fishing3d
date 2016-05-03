using UnityEngine;
using System.Collections;

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
    }

    void Initialize()
    {
        TimeManager. GetInstance().Initialize();
        EventManager.GetInstance().Initialize();
        FishManager. GetInstance().Initialize();
    }
}
