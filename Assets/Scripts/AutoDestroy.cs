using System.Collections;
using UnityEngine;

namespace MT.LD50
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] float delay;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}