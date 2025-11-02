using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RMTestGame.UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        [Header("Main Menu")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
    
        [Header("Settings")] 
        [SerializeField] private Button _backButton;

        [Header("Menus")]
        [SerializeField] private CanvasGroup _mainMenuPanel;
        [SerializeField] private CanvasGroup _settingsMenuPanel;
    
        private void Start()
        {
            SetupButtons();
            SetupControls();
            SetStartingGameState();
        }

        private void OnDestroy() => 
            GameInputHandler.Instance.InputSystem.UI.Back.performed -= BackButtonPressed;

        private void SetupControls() => 
            GameInputHandler.Instance.InputSystem.UI.Back.performed += BackButtonPressed;

        private void SetStartingGameState() => 
            SwitchToMainMenu();

        private void SetupButtons()
        {
            _playButton.onClick.AddListener(StartArenaGame);
            _settingsButton.onClick.AddListener(SwitchToSettingsMenu);
            _exitButton.onClick.AddListener(ExitGame);
            _backButton.onClick.AddListener(SwitchToMainMenu);
        }
    
        private void StartArenaGame() =>
            SceneSwitcher.Instance.SwitchToArena();

        private void SwitchToSettingsMenu() => 
            SwitchMenu(GameMenu.Settings);
    
        private void SwitchToMainMenu() => 
            SwitchMenu(GameMenu.MainMenu);

        private void BackButtonPressed(InputAction.CallbackContext callbackContext) =>
            SwitchToMainMenu();

        private void ExitGame()
        {
            Debug.Log("Exiting game...");
            Application.Quit();
            //For the editor stop the play mode 
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void SwitchMenu(GameMenu switchToGameMenu)
        {
            switch (switchToGameMenu)
            {
                case GameMenu.MainMenu:
                    _settingsMenuPanel.gameObject.SetActive(false);
                    _mainMenuPanel.gameObject.SetActive(true);
                    break;
            
                case GameMenu.Settings:
                    _settingsMenuPanel.gameObject.SetActive(true);
                    _mainMenuPanel.gameObject.SetActive(false);
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException(nameof(switchToGameMenu), switchToGameMenu, null);
            }
        }
    }

    public enum GameMenu {Undefined, MainMenu, Settings}
}