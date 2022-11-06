using UnityEngine;

namespace StateIO
{
    public class Unit : MonoBehaviour
    {
        public UnitsBase Sender => _sender;

        [SerializeField] private float _speed = 5;

		private UnitsBase _target;
		private UnitsBase _sender;

		private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }

        public void Init(UnitsBase sender, UnitsBase target)
        {
            _target = target;
            _sender = sender;
        }
    }
}