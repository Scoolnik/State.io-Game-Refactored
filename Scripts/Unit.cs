using UnityEngine;

namespace StateIO
{
    public class Unit : MonoBehaviour
    {
        public GameObject Target;
        public string Sender;

        public Color CL;
        public Color InsideCL;

        [SerializeField] private float _speed = 5;

        // Update is called once per frame
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.tag != this.gameObject.transform.tag && other.transform.name == "Unit")
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}