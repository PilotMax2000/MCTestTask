using UnityEngine;

namespace RMTestGame
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private string _arenaSceneName;
        [SerializeField] private string _mainMenuSceneName;
        
        public static SceneSwitcher Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SwitchToMainMenu() => 
            UnityEngine.SceneManagement.SceneManager.LoadScene(_mainMenuSceneName);

        public void SwitchToArena() => 
            UnityEngine.SceneManagement.SceneManager.LoadScene(_arenaSceneName);
    }
}