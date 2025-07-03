using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CodeBase.Views.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [Required, SerializeField] private AudioSource _audioSource;

        public void PlayClip(AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            if (clip == null || _audioSource == null)
            {
                return;
            }

            _audioSource.volume = volume;
            _audioSource.pitch = pitch;

            _audioSource.PlayOneShot(clip);
        }
    }
}
