using UnityEngine;

namespace Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseMenu : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private Coroutine _fadeAnimation;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Game.Instance.OnGamePause += OnGamePause;
            Game.Instance.OnGameContinue += OnGameContinue;

            SetActive(false);
        }

        private void OnDestroy()
        {
            Game.Instance.OnGamePause -= OnGamePause;
            Game.Instance.OnGameContinue -= OnGameContinue;
        }

        private void OnGamePause() => SetActive(true);
        private void OnGameContinue() => SetActive(false);

        private void OnDisable()
        {
            if (_fadeAnimation != null) StopCoroutine(_fadeAnimation);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);

            if (value)
                _fadeAnimation = StartCoroutine(Game.Instance.FadeCoroutine(0, 1f, _canvasGroup));
            else
                _canvasGroup.alpha = 0;
        }
    }
}