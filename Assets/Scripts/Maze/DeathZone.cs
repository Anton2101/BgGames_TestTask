using UnityEngine;

namespace Maze
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                Player.Instance.TryToKill();
        }
    }
}
