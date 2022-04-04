using UnityEngine;

namespace MT.LD50
{
    public class Framerate : MonoBehaviour
    {
        [SerializeField] int targetFrameRate = 60;

        void OnEnable()
        {
            Application.targetFrameRate = targetFrameRate;
        }

        void OnDisable()
        {
            Application.targetFrameRate = -1;
        }
    }
}