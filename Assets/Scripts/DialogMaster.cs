using System.Collections.Generic;
using UnityEngine;

namespace MT.LD50
{
    public class DialogMaster : MonoBehaviour
    {
        [System.Serializable]
        public class Quest
        {
            public int requiredFood;
            public DialogTrigger baseDialog;

            public bool readyToFeed;
            public DialogTrigger victoryDialog;
        }

        public List<Quest> quests;
        public int index;
        Quest quest => quests[index];

        [SerializeField] CharacterController2D playerController;
        [SerializeField] Inventory playerInventory;
        [SerializeField] EndGame endGame;

        Animator animator;
        DialogTrigger[] dialogTriggers;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            dialogTriggers = GetComponentsInChildren<DialogTrigger>(true);
            foreach (var trigger in dialogTriggers)
            {
                trigger.onPreMessage.AddListener(() => animator.SetTrigger("Talk"));
                trigger.gameObject.SetActive(false);
            }
            quest.baseDialog.gameObject.SetActive(true);
        }

        void Update()
        {
            if (playerController.activeObject is not null)
                return;

            if (!quest.readyToFeed && playerInventory.food >= quest.requiredFood)
            {
                quest.readyToFeed = true;
                quest.baseDialog.gameObject.SetActive(false);
                quest.victoryDialog.gameObject.SetActive(true);
            }
        }

        public void StartNextQuest()
        {
            playerInventory.food -= quest.requiredFood;

            index++;

            foreach (var trigger in dialogTriggers)
                trigger.gameObject.SetActive(false);

            quest.baseDialog.gameObject.SetActive(true);
            quest.baseDialog.Show(playerController);
        }

        public void EndGame()
        {
            playerController.activeObject = endGame;
            endGame.enabled = true;
        }
    }
}