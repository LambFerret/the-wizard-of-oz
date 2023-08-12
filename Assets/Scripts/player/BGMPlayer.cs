using UnityEngine;
using UnityEngine.SceneManagement;

namespace player
{
    public class BGMPlayer : MonoBehaviour
    {
        public static BGMPlayer Instance;

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
            _audioSource.loop = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Play("start");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _audioSource.Stop();
            if (scene.name is "Stage_00" or "Stage_01")
            {
                Play("yellow");
            }
            else if (scene.name is "Stage_02" or "Stage_03")
            {
                Play("forest");
            }
            else if (scene.name is "Stage_04")
            {
                Play("wicked1");
            }
        }

        public void Play(string clipName, float volume = 1.0f)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName) _audioSource.PlayOneShot(clip, volume);
            }
        }
    }
}