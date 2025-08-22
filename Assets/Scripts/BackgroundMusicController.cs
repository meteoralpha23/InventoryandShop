using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Button playPauseButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Text playPauseButtonText;

    [Header("Settings")]
    [SerializeField] private bool showDebugInfo = true;

    private void Start()
    {
        // Set up UI controls if they exist
        SetupUIControls();
        
        // Update UI to reflect current state
        UpdateUI();
    }

    private void SetupUIControls()
    {
        // Set up volume slider
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = SoundManager.Instance.GetMusicVolume();
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        // Set up play/pause button
        if (playPauseButton != null)
        {
            playPauseButton.onClick.AddListener(OnPlayPauseClicked);
        }

        // Set up stop button
        if (stopButton != null)
        {
            stopButton.onClick.AddListener(OnStopClicked);
        }
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(value);
        }
    }

    private void OnPlayPauseClicked()
    {
        if (SoundManager.Instance != null)
        {
            if (SoundManager.Instance.IsBackgroundMusicPlaying())
            {
                SoundManager.Instance.PauseBackgroundMusic();
            }
            else
            {
                SoundManager.Instance.ResumeBackgroundMusic();
            }
            
            UpdateUI();
        }
    }

    private void OnStopClicked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBackgroundMusic();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (SoundManager.Instance == null) return;

        // Update play/pause button text
        if (playPauseButtonText != null)
        {
            playPauseButtonText.text = SoundManager.Instance.IsBackgroundMusicPlaying() ? "Pause" : "Play";
        }

        // Update volume slider
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = SoundManager.Instance.GetMusicVolume();
        }
    }

    // Public methods for external control
    public void PlayBackgroundMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StartBackgroundMusic();
            UpdateUI();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PauseBackgroundMusic();
            UpdateUI();
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ResumeBackgroundMusic();
            UpdateUI();
        }
    }

    public void StopBackgroundMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBackgroundMusic();
            UpdateUI();
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(volume);
        }
    }

    // Debug info
    private void OnGUI()
    {
        if (!showDebugInfo || SoundManager.Instance == null) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("Background Music Debug Info", GUI.skin.box);
        
        GUILayout.Label($"Music Playing: {SoundManager.Instance.IsBackgroundMusicPlaying()}");
        GUILayout.Label($"Music Volume: {SoundManager.Instance.GetMusicVolume():F2}");
        GUILayout.Label($"Master Volume: {SoundManager.Instance.GetMasterVolume():F2}");
        
        if (GUILayout.Button("Test Background Music"))
        {
            SoundManager.Instance.TestBackgroundMusic();
        }
        
        if (GUILayout.Button("Diagnose Audio System"))
        {
            SoundManager.Instance.DiagnoseAudioSystem();
        }
        
        GUILayout.EndArea();
    }
} 