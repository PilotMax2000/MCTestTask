using UnityEngine;

namespace RMTestGame.Arena
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private  float _moveSpeed = 3f;
        [SerializeField] private  float _stopDistance = 0.5f; // Distance at which the enemy stops moving
        private Transform _player;
        private bool _stopped = false;
        private Camera _cam;

        public void Init(Transform player)
        {
            _player = player;
            _cam = Camera.main;
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        private void Update()
        {
            if (_stopped || _player == null) return;

            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance <= _stopDistance)
            {
                _stopped = true;
                return;
            }

            Vector3 dir = (transform.position - _player.position).normalized;
            Vector3 pos = transform.position + dir * _moveSpeed * Time.deltaTime;
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