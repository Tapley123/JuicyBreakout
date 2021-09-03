using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Music Settings")]
    [SerializeField] private Image currentMusicIcon;
    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    public Slider musicSlider;

    [Header("Sound Effect Settings")]
    [SerializeField] private Image currentSoundEffectIcon;
    [SerializeField] private Sprite soundEffectsOnIcon;
    [SerializeField] private Sprite soundEffectsOffIcon;
    public Slider soundEffectSlider;

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

        PauseMenuBehaviors();
    }

    void TurnOffAllPanels()
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(false);
        }
    }

    void PauseMenuBehaviors()
    {
        // Toggles the music Icon when the music is turned on/off
        if (musicSlider.value == 0)
            currentMusicIcon.sprite = musicOffIcon;
        else
            currentMusicIcon.sprite = musicOnIcon;

        // Toggles the sound effect Icon when the sound effects are turned on/off
        if (soundEffectSlider.value == 0)
            currentSoundEffectIcon.sprite = soundEffectsOffIcon;
        else
            currentSoundEffectIcon.sprite = soundEffectsOnIcon;
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
