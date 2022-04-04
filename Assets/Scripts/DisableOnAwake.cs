using UnityEngine;

namespace MT.LD50
{
    public class DisableOnAwake : MonoBehaviour
    {
        void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}