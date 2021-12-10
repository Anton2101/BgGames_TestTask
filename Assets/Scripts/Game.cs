using System;
using System.Collections;
using Maze;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public CanvasGroup loadScreen;
    public Action OnGameStart;
    public Action OnGamePause;
    public Action OnGameContinue;

    private void Awake()
    {
        Instance = this;
        MazeGenerator.Instance.OnMazeGenerated += OnMazeGenerated;
    }

    private void OnDestroy()
    {
        MazeGenerator.Instance.OnMazeGenerated -= OnMazeGenerated;
    }

    private void OnMazeGenerated()
    {
        StartCoroutine(FadeCoroutine(1f, 0f, loadScreen, callback: StartGame));
    }

    private void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void PauseGame()
    {
        OnGamePause?.Invoke();
    }

    public void ContinueGame()
    {
        OnGameContinue?.Invoke();
    }

    public void Finish()
    {
        Player.Instance.Finish();
        StartCoroutine(FadeCoroutine(0f, 1f, loadScreen, 1.5f, ResetGame));
    }

    private void ResetGame()
    {
        Player.Instance.ResetPlayer();
        MazeGenerator.Instance.GenerateMaze();
    }
    
    public IEnumerator FadeCoroutine(float from, float to, CanvasGroup screen, float cooldown = 0f, Action callback = null)
    {
        yield return new WaitForSeconds(cooldown);
        
        float time = 0;

        while (time < 1)
        {
            yield return new WaitForSeconds(0.008f);

            time += 0.05f;
            if (screen) screen.alpha = Mathf.Lerp(from, to, time);
        }

        callback?.Invoke();
    }
}
