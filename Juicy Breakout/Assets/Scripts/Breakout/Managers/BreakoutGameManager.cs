using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutGameManager : MonoBehaviour
{
    #region Singleton
    private static BreakoutGameManager instance;

    public static BreakoutGameManager Instance => instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public bool IsGameStarted { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
