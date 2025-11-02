using UnityEngine;

namespace RMTestGame.Arena
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        
        private Camera _cam;
        private Vector2 _moveInput;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            var input = GameInputHandler.Instance.InputSystem.Player.Move.ReadValue<Vector2>();
            _moveInput = input.normalized;
            Move();
        }

        private void Move()
        {
            Vector3 pos = transform.position + (Vector3)_moveInput * _moveSpeed * Time.deltaTime;
            pos = ClampToScreen(pos);
            transform.position = pos;
        }

        private Vector3 ClampToScreen(Vector3 pos)
        {
            Vector3 min = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
            Vector3 max = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            pos.z = 0;
            return pos;
        }
    }
}