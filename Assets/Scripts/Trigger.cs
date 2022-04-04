using UnityEngine;
using UnityEngine.Events;

namespace MT.LD50
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] bool unparentOnAwake;

        [SerializeField] UnityEvent<Collider2D> onTriggerEnter = new();
        [SerializeField] UnityEvent<Collider2D> onTriggerExit = new();

        void Awake()
        {
            if (unparentOnAwake)
                transform.SetParent(null);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (CompareTag(collision.tag))
                onTriggerEnter.Invoke(collision);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (CompareTag(collision.tag))
                onTriggerExit.Invoke(collision);
        }
    }
}