using UnityEngine;
using System;

public static class AudioBus
{

    public static event Action PlayButtonClickAudio;
    public static void SendButtonClickAudio() =>  PlayButtonClickAudio?.Invoke();
    public static event Action PlayChangeOptionAudio;
    public static void SendChangeOptionAudio() => PlayChangeOptionAudio?.Invoke();
    
}
