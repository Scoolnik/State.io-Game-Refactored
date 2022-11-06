using UnityEngine;

namespace StateIO
{
    public class Unit : MonoBehaviour
    {
        public Area Sender => _sender;

        [SerializeField] private float _speed = 5;

		private Area _target;
		private Area _sender;

		private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }

        public void Init(Area sender, Area target)
        {
            _target = target;
            _sender = sender;
        }
    }
}