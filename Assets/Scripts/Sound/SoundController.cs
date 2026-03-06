using System.Collections.Generic;
using UnityEngine;

namespace Core.Sound
{
    public class SoundController : MonoBehaviour
    {
        private Dictionary<SoundId, AudioSource> sources;


        private void Awake()
        {
            Initialize();
        }


        private void Initialize()
        {
            DontDestroyOnLoad(this.gameObject);
        }


        public void RegisterSource(AudioSource source, SoundId id)
        {
            if (source.loop) sources.Add(id, source);
            else Destroy(source.gameObject, source.clip.length);
        }


        public void UnregisterSource(SoundId id)
        {
            AudioSource source = GetSource(id);

            if (source != null)
            {
                sources.Remove(id);
                Destroy(source.gameObject);
            }
        }


        public AudioSource GetSource(SoundId id)
        {
            if (sources.ContainsKey(id))
                return sources[id];

            return null;
        }
    }
}
