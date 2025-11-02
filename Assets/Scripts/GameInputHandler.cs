using UnityEngine;

namespace RMTestGame
{
    public class GameInputHandler : MonoBehaviour
    {
        public static GameInputHandler Instance { get; private set; }

        private InputSystem _inputSystem;
        public InputSystem InputSystem => _inputSystem;

        private void Awake()
        {
            if (Instance != null && Instance != this && gameObject  != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _inputSystem = new InputSystem();
        }

        private void OnEnable()
        {
            if(_inputSystem != null)
                _inputSystem.Enable();
        }

        private void OnDisable()
        {
            if(_inputSystem != null)
                _inputSystem.Disable();
        }
    }
}