using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    public class InvincibleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Coroutine _invincibleTimer;

        public void OnPointerDown(PointerEventData eventData)
        {
            Player.Instance.SetInvincible(true);
            _invincibleTimer = StartCoroutine(InvincibleTimer());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_invincibleTimer != null)
            {
                StopCoroutine(_invincibleTimer);
                Player.Instance.SetInvincible(false);
            }
        }

        private IEnumerator InvincibleTimer()
        {
            yield return new WaitForSeconds(2f);
            
            Player.Instance.SetInvincible(false);
            _invincibleTimer = null;
        }
    }
}