using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    public KeyCode changeSongButton;
    public int currentSongIndex;
    public List<AudioClip> songs;
    [SerializeField] private GameObject currentSongDisplay;

    [Header("Toggles")]
    public bool displayCurrentSong = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        PickStartSong();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(changeSongButton))
        {
            PickRandomSong();
        }

        CurrentSongDisplay();
    }

    void PickStartSong() //picks a random song to play when the game starts
    {
        int randomSongIndex = Random.Range(0, songs.Count);
        currentSongIndex = randomSongIndex;
        audioSource.clip = songs[randomSongIndex];
        audioSource.Play();
    }

    public void PickRandomSong()
    {
        bool foundNewIndex = false;
        int tempNum;

        while(!foundNewIndex)
        {
            tempNum = Random.Range(0, songs.Count);

            if(tempNum != currentSongIndex)
            {
                foundNewIndex = true;
                currentSongIndex = tempNum;
                audioSource.clip = songs[tempNum];
                audioSource.Play();
            }
        }
    }

    void CurrentSongDisplay()
    {
        if (displayCurrentSong)
            currentSongDisplay.SetActive(true);
        else
            currentSongDisplay.SetActive(false);

        currentSongDisplay.GetComponentInChildren<TMP_Text>().text = songs[currentSongIndex].name;
    }
}
