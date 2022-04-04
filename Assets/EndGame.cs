using System.Collections;
using UnityEngine;

namespace MT.LD50
{
    public class EndGame : MonoBehaviour, IActiveObject
    {
        [SerializeField] Animator godAnimator;
        [SerializeField] GameObject player;
        [SerializeField] SpriteRenderer blackscreen;
        [SerializeField] TMPro.TextMeshPro text1;
        [SerializeField] TMPro.TextMeshPro text2;

        public void Next()
        {
            
        }

        void OnEnable()
        {
            StartCoroutine(EndGameRoutine());
        }

        IEnumerator EndGameRoutine()
        {
            godAnimator.SetTrigger("End Game");
            yield return new WaitForSeconds(1.3f);
            player.SetActive(false);

            yield return new WaitForSeconds(3.7f);

            while (blackscreen.color.a < .7f)
            {
                var color = blackscreen.color;
                color.a = Mathf.Min(.7f, color.a + Time.deltaTime * .35f);
                blackscreen.color = color;
                yield return null;
            }

            while (text1.color.a < 1)
            {
                var color = text1.color;
                color.a += Time.deltaTime * .5f;
                text1.color = color;
                yield return null;
            }

            while (text2.color.a < 1)
            {
                var color = text2.color;
                color.a += Time.deltaTime * .5f;
                text2.color = color;
                yield return null;
            }
        }
    }
}