using UnityEngine;

namespace MT.LD50
{
    public class SetTransformToMousePosition : MonoBehaviour
    {
#if DEBUG
        void Update()
        {
            if (!InputManager.leftMouseButton.down)
                return;

            var position = Camera.main.ScreenToWorldPoint(InputManager.mousePosition);
            position.z = transform.position.z;
            transform.position = position;
        }
#endif
    }
}