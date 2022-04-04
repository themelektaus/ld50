using UnityEngine;

namespace MT.LD50
{
    public class Inventory : MonoBehaviour
    {
        public int food;

        public void AddFood(int amount)
        {
            food += amount;
        }
    }
}