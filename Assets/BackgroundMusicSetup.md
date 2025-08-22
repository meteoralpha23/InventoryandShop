# Background Music Setup Guide

## Overview
Your Unity project now has a comprehensive background music system that automatically plays ambient music for the entire scene.

## Setup Instructions

### 1. Configure SoundManager
1. **Find your SoundManager GameObject** in the scene
2. **In the Inspector**, locate the new "Background Music" section
3. **Assign the Background Ambient.ogg** file to the "Scene Background Music" field
4. **Adjust settings**:
   - **Play Background Music On Start**: Enable this to auto-play music when scene loads
   - **Background Music Fade In Time**: Set to 2 seconds for smooth fade-in

### 2. Audio Source Setup
Ensure your SoundManager has **3 AudioSource components**:
- **Music Source**: For background music (will be configured automatically)
- **SFX Source**: For sound effects
- **UI Source**: For UI sounds

### 3. Background Music Features

#### Automatic Playback
- Music starts automatically when scene loads (if enabled)
- Smooth fade-in effect prevents sudden audio
- Loops continuously for ambient atmosphere

#### Manual Control
- **StartBackgroundMusic()**: Begin playing background music
- **StopBackgroundMusic()**: Stop with fade-out effect
- **PauseBackgroundMusic()**: Pause playback
- **ResumeBackgroundMusic()**: Resume from pause
- **SetBackgroundMusicVolume()**: Adjust volume dynamically

#### Volume Control
- **Master Volume**: Controls overall system volume
- **Music Volume**: Controls background music specifically
- **Fade Effects**: Smooth transitions between volume levels

### 4. Optional: BackgroundMusicController
The `BackgroundMusicController` script provides:
- **UI Controls**: Volume slider, play/pause, stop buttons
- **Debug Information**: Real-time audio system status
- **External Control**: Methods for other scripts to control music

### 5. Testing
Use these methods to test your audio system:
- **TestBackgroundMusic()**: Tests play/pause functionality
- **DiagnoseAudioSystem()**: Comprehensive audio system check

## File Locations
- **Background Music Asset**: `Dark UI/Sounds/Background Ambient.ogg`
- **Main Script**: `Scripts/SoundManager.cs`
- **Controller Script**: `Scripts/BackgroundMusicController.cs`

## Troubleshooting

### Music Not Playing
1. Check if `sceneBackgroundMusic` is assigned in SoundManager
2. Verify AudioSource components are properly set up
3. Check console for error messages
4. Use `DiagnoseAudioSystem()` method

### Volume Issues
1. Check Master Volume setting
2. Verify Music Volume setting
3. Ensure AudioSource volume is not muted
4. Check Unity's Audio Mixer settings

### Performance
- Background music uses minimal resources
- Fade effects are optimized with coroutines
- Audio clips are streamed efficiently

## Integration with Existing Systems
- **Inventory System**: Background music continues during inventory open/close
- **Shop System**: Music persists through shop interactions
- **UI System**: No interference with UI sound effects
- **Time Scaling**: Music continues regardless of game time scale

## Customization
You can easily:
- **Change music**: Assign different audio clips to `sceneBackgroundMusic`
- **Adjust timing**: Modify fade-in/out durations
- **Add variety**: Implement multiple background tracks
- **Create transitions**: Use the pause/resume methods for smooth track changes 