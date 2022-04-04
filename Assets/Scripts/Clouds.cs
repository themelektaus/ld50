using UnityEngine;

namespace MT.LD50
{
    public class Clouds : MonoBehaviour
    {
        [SerializeField] GameObject[] prefabs;

        GameObject[] pool;

        void OnEnable()
        {
            pool = new GameObject[prefabs.Length * 5];
            for (int i = 0; i < prefabs.Length; i++)
                for (int j = 0; j < 5; j++)
                    pool[i] = Instantiate(prefabs[i], transform);
        }

        void OnDisable()
        {
            foreach (var gameObject in pool)
                Destroy(gameObject);
            pool = null;
        }

        void Update()
        {
            
        }
    }
}