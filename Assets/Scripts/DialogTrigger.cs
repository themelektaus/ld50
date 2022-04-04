using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MT.LD50
{
    public class DialogTrigger : MonoBehaviour, IActiveObject
    {
        public UnityEvent onPreMessage = new();
        public UnityEvent onPreDialog = new();
        public UnityEvent onPostDialog = new();

        [SerializeField] AudioSource cursorAudio;

        [System.NonSerialized] public GameObject[] messages;

        int index;
        int currentIndex;

        CharacterController2D requester;
        GameObject closingMessage;

        void Awake()
        {
            messages = GetComponentsInChildren<Message>(true).Where(x => !x.destroy).Select(x => x.gameObject).ToArray();
            foreach (var message in messages)
                message.transform.localPosition = Vector3.zero;

            ResetIndex();
            DeactivateAllMessages();
        }

        void ResetIndex()
        {
            index = 0;
            currentIndex = -1;
        }

        void DeactivateAllMessages()
        {
            foreach (var message in messages)
                message.SetActive(false);
        }

        public void Show(CharacterController2D requester)
        {
            if (!requester.ground.isTouching)
                return;

            this.requester = requester;
            requester.activeObject = this;
            ResetIndex();
            onPreDialog.Invoke();
        }

        public void Next()
        {
            if (!closingMessage || !closingMessage.activeSelf)
            {
                index++;
                cursorAudio.Play();
            }
        }

        void Update()
        {
            if (closingMessage)
            {
                if (!closingMessage.activeSelf)
                {
                    closingMessage = null;
                    if (index < messages.Length)
                    {
                        messages[currentIndex].SetActive(true);
                        return;
                    }

                    requester.activeObject = null;
                    requester = null;
                    ResetIndex();
                    DeactivateAllMessages();
                    onPostDialog.Invoke();
                }
                return;
            }

            if (!requester)
                return;

            if (index == messages.Length)
            {
                Close();
                return;
            }

            if (currentIndex == index)
                return;

            if (currentIndex > -1)
                Close();
            else
                messages[index].SetActive(true);

            currentIndex = index;
            onPreMessage.Invoke();
        }

        void Close()
        {
            closingMessage = messages[currentIndex];
            closingMessage.GetComponent<Animator>().SetTrigger("Close");
        }
    }
}