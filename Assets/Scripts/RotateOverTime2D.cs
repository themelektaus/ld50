using UnityEngine;

namespace LD50
{
    public class RotateOverTime2D : MonoBehaviour
    {
        [SerializeField] float speed = -60;

        void Update()
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }
    }
}