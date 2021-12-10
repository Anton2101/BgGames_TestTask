using UnityEngine;

namespace Maze
{
    public class FinishZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                Game.Instance.Finish();
        }
    }
}
