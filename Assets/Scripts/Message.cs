using UnityEngine;

namespace MT.LD50
{
    [ExecuteAlways]
    public class Message : MonoBehaviour
    {
        public bool destroy;

        [SerializeField] string text;

        TMPro.TextMeshPro tmp;

        void Awake()
        {
            tmp = GetComponentInChildren<TMPro.TextMeshPro>();
        }

        void Start()
        {
            if (Application.isPlaying && destroy)
                Destroy(gameObject);
        }

        void Update()
        {
            tmp.text = text;
        }
    }
}