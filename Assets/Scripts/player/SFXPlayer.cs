using UnityEngine;

namespace player
{
    public class SFXPlayer : MonoBehaviour
    {
        public static SFXPlayer Instance;

        public AudioClip[] clips;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public AudioSource Play(string clipName, float volume = 1.0f)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;

            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    return audioSource;
                }
            }

            return null; // 클립을 찾지 못한 경우
        }
    }
}