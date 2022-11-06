using UnityEngine;

namespace StateIO
{
    public class Unit : MonoBehaviour
    {
		[SerializeField] private Area _target;
        [SerializeField] private Area _sender;

        [SerializeField] private float _speed = 5;

        // Update is called once per frame
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