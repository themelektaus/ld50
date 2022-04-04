using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float delay;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}