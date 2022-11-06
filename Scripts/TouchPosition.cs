using UnityEngine;

namespace StateIO
{
    public class TouchPosition : MonoBehaviour
    {
        public string MyTag;
        public GameObject MyArea;
        public string MyAreaName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.name != "Unit" && other.transform.name != MyAreaName)
            {
                MyArea.GetComponent<Area>().Attack = 1;
                MyArea.GetComponent<Area>().Target = other.gameObject;

            }
        }
    }
}