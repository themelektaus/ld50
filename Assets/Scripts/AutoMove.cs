using UnityEngine;

namespace LD50
{
    public class AutoMove : MonoBehaviour
    {
        void Update()
        {
            transform.position = new(Mathf.Abs(Time.time % 2 - 1) * 5 - 2.5f, 0, 0);
        }
    }
}