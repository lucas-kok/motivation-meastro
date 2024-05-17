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

        var currentVolume = _audioState.audioMixer.GetFloat("volume", out var v) ? v : 0;
        audioSlider.value = currentVolume;

        muteToggle.isOn = _audioState.IsMuted;  
    }

    public void OpenMenu(MenuType type)
    {

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

    // Feat: if a user slides to the min value (-60), the mute Toggle should trigger
    public void OnChangeVolumeSlider(float volume)
    {
        if (volume <= -60f)
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
            _audioState.SetVolume(-60f);
        } else
        {
            _audioState.IsMuted = false;
            _audioState.SetVolume(audioSlider.value);
        }

        audioSlider.value = _audioState.GetCurrentVolume();
    }
}
