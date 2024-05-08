using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject optionsMenu;  // Reference to the options menu panel
    public Slider volumeSlider;  // Reference to the slider
    public AudioSource musicSource;  // Reference to the AudioSource

    void Start()
    {
        optionsMenu.SetActive(false);  // Hide the menu at start
        volumeSlider.value = musicSource.volume;  // Set slider initial value
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);  // Add listener for slider changes
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void HandleVolumeChange(float value)
    {
        Debug.Log("Volume changed to: " + value);
        musicSource.volume = value;
    }

    void ToggleMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);  // Toggle menu visibility
    }
}