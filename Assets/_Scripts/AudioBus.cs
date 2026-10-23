using UnityEngine;
using System;

public static class AudioBus
{
    public static event Action PlayButtonClickAudio;
    public static void SendButtonClickAudio() =>  PlayButtonClickAudio?.Invoke();
}
