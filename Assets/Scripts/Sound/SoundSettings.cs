using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Sound
{
    [CreateAssetMenu(menuName = "Settings/Sound")]
    public class SoundSettings : ScriptableObject
    {
        public List<SoundGroup> groups;
    }


    public enum SoundChannel
    {
        Music,
        Effects,
        Interface
    }


    public enum SoundId 
    {
        // Effects
        Complete        = 101,
        Impact          = 102,
        Stack           = 103,
        Woosh           = 104,
        Shoot           = 105,
        SoftImpact      = 106,
        HardImpact      = 107,
        Blow            = 108,
        Drop            = 109,
        Select          = 110,
        
        // Interface
        PressButton     = 1001,
        WinPopUp        = 1002,
        LosePopUp       = 1003
    }


    [System.Serializable]
    public class SoundGroup
    {
        public SoundChannel channel;
        public List<SoundContent> sounds;
    }


    [System.Serializable]
    public class SoundContent 
    {
        public SoundId id;
        public List<AudioClip> clip;
        public AudioMixerGroup mixerGroup;
        public float minPlaybackDelay;
    }
}
