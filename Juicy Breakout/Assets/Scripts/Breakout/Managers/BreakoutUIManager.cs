using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutUIManager : MonoBehaviour
{
    #region Singleton
    private static BreakoutUIManager instance;

    public static BreakoutUIManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public bool paused = false;

    [Header ("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    public List<GameObject> panels;

    void Start()
    {
        panels.Add(pausePanel);
        TurnOffAllPanels();
        
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void TurnOffAllPanels()
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(false);
        }
    }

    #region Buttons
    public void Button_Pause()
    {
        if (!paused)
            paused = true;
        else
            paused = false;

        if (paused)
        {
            Time.timeScale = 0;
            TurnOffAllPanels();
            pausePanel.SetActive(true);
        }
        else
            TurnOffAllPanels();
    }
    #endregion
}
