using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT.LD50
{
    public class AutoMove : MonoBehaviour
    {
        [SerializeField] Vector3 offset;
        [SerializeField] Vector3 position;
        [SerializeField] AnimationCurve curve;
        [SerializeField] float speed;

        void Update()
        {
            transform.localPosition = position * curve.Evaluate(Mathf.Abs(1 - Time.time * speed % 2)) - offset;
        }
    }
}