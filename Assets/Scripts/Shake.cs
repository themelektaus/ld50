using UnityEngine;

namespace MT.LD50
{
    public class Shake : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] Vector2 scale = Vector2.one;

        Vector2 position;
        Vector2 localPosition;

        void Awake()
        {
            position = transform.position;
            localPosition = transform.localPosition;
        }

        void OnDisable()
        {
            var position = transform.localPosition;
            position.x = localPosition.x;
            position.y = localPosition.y;
            transform.localPosition = position;
        }

        void Update()
        {
            var position = transform.localPosition;
            var t = Time.time * speed;
            position.x = localPosition.x + (Mathf.PerlinNoise(this.position.x + t, this.position.y) - .5f) * scale.x;
            position.y = localPosition.y + (Mathf.PerlinNoise(this.position.x, this.position.y + t) - .5f) * scale.y;
            transform.localPosition = position;
        }
    }
}