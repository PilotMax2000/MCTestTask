using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace RMTestGame.Arena
{
    public class ArenaHandler : MonoBehaviour
    {
        [SerializeField] private  GameObject _playerPrefab;
        [SerializeField] private  GameObject _enemyPrefab;
        [SerializeField] private  int _enemyCount = 1000;


        private void Start()
        {
            //back button subscription
            GameInputHandler.Instance.InputSystem.UI.Back.performed += GoToMainMenu;
            StartArenaBattle();
        }

        private void OnDestroy()
        {
            GameInputHandler.Instance.InputSystem.UI.Back.performed -= GoToMainMenu;
        }

        private void GoToMainMenu(InputAction.CallbackContext callbackContext) => 
            SceneSwitcher.Instance.SwitchToMainMenu();

        private void StartArenaBattle()
        {
            var player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);

            for (int i = 0; i < _enemyCount; i++)
            {
                Vector2 pos = Random.insideUnitCircle * 8f;
                var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
                enemy.GetComponent<EnemyController>().Init(player.transform);
            }
        }
    }
}