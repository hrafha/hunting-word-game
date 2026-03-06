using System.Collections.Generic;
using UnityEngine;

namespace Core.Sound
{
    public static class SoundManager
    {
        private static Dictionary<SoundId, float> playbackCache = null;
        private static Dictionary<SoundId, SoundContent> searchPair = null;

        private static SoundSettings settings;
        public static SoundSettings Settings
        {
            get
            {
#if UNITY_EDITOR
                if (settings == null)
                {
                    List<SoundSettings> editorSettings = new List<SoundSettings>(AssetsManager.FindAssetsByType<SoundSettings>());
                    if (editorSettings != null && editorSettings.Count > 0)
                        settings = editorSettings[0];
                }
#endif

                return settings;
            }
            set
            {
                settings = value;
                Initialize();
            }
        }


        private static SoundController controller;



        private static void Initialize()
        {
            SoundSettings sounds = Settings;

            playbackCache = new Dictionary<SoundId, float>();
            searchPair = new Dictionary<SoundId, SoundContent>();
            if (sounds != null && sounds.groups != null && sounds.groups.Count > 0)
            {
                sounds.groups.ForEach(group =>
                {
                    if (group == null || group.sounds == null || group.sounds.Count == 0)
                        return;

                    group.sounds.ForEach(sound => searchPair.Add(sound.id, sound));
                });
            }

            CreateController();
        }


        private static void CreateController()
        {
            if (controller != null) return;

            GameObject o = new GameObject("Sound Controller");
            controller = o.AddComponent<SoundController>();
        }


        private static AudioSource CreateSoundOutput(SoundContent content, int clipIndex = -1)
        {
            if (content == null || content.clip == null || content.clip.Count == 0)
                return null;

            GameObject o = new GameObject(content.id.ToString());
            o.transform.SetParent(controller.transform);

            AudioSource source = o.AddComponent<AudioSource>();

            if (clipIndex < 0)
                clipIndex = content.clip.Count == 1 ? 0 : Random.Range(0, content.clip.Count);
            else clipIndex = Mathf.Clamp(clipIndex, 0, content.clip.Count-1);

            source.clip = content.clip[clipIndex];
            source.outputAudioMixerGroup = content.mixerGroup;

            return source;
        }

        #region Calls
        public static void Play(SoundRequest request)
        { Play(request.soundId, request.clipIndex, request.looping); }


        public static void Play(SoundId soundId)
        { Play(soundId, -1, false); }


        public static void Play(SoundId soundId, int clipIndex)
        { Play(soundId, clipIndex, false); }


        public static void Play(SoundId soundId, bool looping)
        { Play(soundId, -1, looping); }


        public static void Play(SoundId soundId, int clipIndex, bool looping)
        {
            if (controller == null)
                Initialize();

            if (searchPair != null && searchPair.ContainsKey(soundId))
            {
                float lastPlaybackTime = 0;
                if (playbackCache.ContainsKey(soundId))
                    lastPlaybackTime = playbackCache[soundId];

                SoundContent content = searchPair[soundId];
                if(Time.time - lastPlaybackTime < content.minPlaybackDelay)
                    return;

                if (playbackCache.ContainsKey(soundId))
                    playbackCache[soundId] = Time.time;
                else playbackCache.Add(soundId, Time.time);

                AudioSource source = CreateSoundOutput(content, clipIndex);
                source.loop = looping;
                source.Play();

                controller.RegisterSource(source, soundId);
            }
            else Debug.LogWarning("Don't find sound with id => " + soundId.ToString());
        }


        public static void Stop(SoundId soundId)
        {
            if (controller == null)
                return;

            AudioSource source = controller.GetSource(soundId);
            if (source != null)
            {
                source.Stop();
                controller.UnregisterSource(soundId);
            }
        }


        public static void Pause(SoundId soundId)
        {
            if (controller == null)
                return;

            AudioSource source = controller.GetSource(soundId);
            if (source != null)
                source.Pause();
        }


        public static void UnPause(SoundId soundId)
        {
            if (controller == null)
                return;

            AudioSource source = controller.GetSource(soundId);
            if (source != null)
                source.UnPause();
        }
        #endregion
    }


    public class SoundRequest
    {
        public SoundId soundId;
        public int clipIndex = -1;
        public bool looping = false;
    }
}
