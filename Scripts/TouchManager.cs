using UnityEngine;

namespace StateIO
{
    [RequireComponent(typeof(LineRenderer))]
    public class TouchManager : MonoBehaviour
    {
        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
            _line.enabled = false;
        }

        private void Update()
        {
            if (Input.touchCount == 0)
                return;
            var touch = Input.GetTouch(0);
            print("touched " + touch.phase);

			if (touch.phase == TouchPhase.Began)
            {
                _line.enabled = true;
                _line.SetPosition(0, touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _line.enabled = false;
            }
            _line.SetPosition(1, touch.position);
        }
    }
}