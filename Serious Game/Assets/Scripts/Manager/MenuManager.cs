using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Singleton
    private AudioState _audioState;

    // UI 
    public GameObject menu;

    // UI - panels
    public GameObject optionsPanel;

    // UI - interactables 
    public Button startButton;
    public Button resumeButton;
    public Toggle muteToggle;
    public Slider audioSlider;

    private void Start()
    {
        _audioState = AudioState.Instance;

        var currentVolume = _audioState.GetCurrentVolume();
        audioSlider.value = currentVolume;

        muteToggle.isOn = _audioState.IsMuted;  
    }

    public void OpenMenu(MenuType type)
    {
        _audioState.Play("open-scenario");

        startButton.gameObject.SetActive(type is MenuType.MAIN_MENU);
        resumeButton.gameObject.SetActive(type is MenuType.IN_GAME_MENU);

        menu.SetActive(true);
    }

    // Methods for showing and hiding the menu (items)
    public void CloseMenu() => menu.SetActive(false);

    public void CloseAllPanels() => optionsPanel.SetActive(false);

    public void OpenOptions()
    {
        CloseAllPanels();
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() => optionsPanel.SetActive(false);

    public void OnChangeVolumeSlider(float volume)
    {
        if (volume <= 0)
        {
            muteToggle.isOn = true;
            _audioState.IsMuted = true;

        } else
        {
            muteToggle.isOn = false;
            _audioState.IsMuted = false;
        }

        _audioState.SetVolume(volume);
    }

    public void OnToggleVolumeMute()
    {
        if (muteToggle.isOn)
        {
            _audioState.IsMuted = true;
            _audioState.SetVolume(0);
        } else
        {
            _audioState.IsMuted = false;
            _audioState.SetVolume(1);
        }

        audioSlider.value = _audioState.GetCurrentVolume();
    }
}
