using UnityEngine;

namespace MT.LD50
{
    public class Footsteps : MonoBehaviour
    {
        [SerializeField] AudioSource footstep;
        [SerializeField] Vector2 pitch;
        public void PlayFootstepSound()
        {
            footstep.pitch = Random.Range(pitch.x, pitch.y);
            footstep.Play();
        }
    }
}