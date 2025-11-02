using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RMTestGame.UI
{
    public class UISelectFirst : MonoBehaviour
    {
        [SerializeField] private Selectable _first;

        void OnEnable()
        {
            if (_first == null) return;
            // Wait a frame so EventSystem exists & UI is enabled
            StartCoroutine(SelectNextFrame());
        }

        System.Collections.IEnumerator SelectNextFrame()
        {
            yield return null;
            EventSystem.current?.SetSelectedGameObject(_first.gameObject);
            _first.Select();
        }

        private void Update()
        {
            //Fixing glitch when editor window is unfocused and focused
            #if UNITY_EDITOR
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(_first.gameObject);
                _first.Select();
            }
            #endif
        }
    }
}