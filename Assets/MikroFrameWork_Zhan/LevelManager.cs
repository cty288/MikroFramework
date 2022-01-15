using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager : MonoSingleton<LevelManager>
{
	public Camera CGCamera;

    public GameState gameState;
	private PlayableDirector director;

	void Start ()
	{
		director = CGCamera.GetComponent<PlayableDirector>();
        SwitchGameState(GameState.CG);
		DOVirtual.DelayedCall((float)director.duration + 1.5f, () =>
		{
			CGCamera.depth = -10;
            SwitchGameState(GameState.InGame);

        });

        AudioManager.Instance.PlayMusic(MusicAudio.InGameAudio);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameState == GameState.InGame)
            {
                SwitchGameState(GameState.Pause);
                UIManager.Instance.Push<UIScreenPause>(UIDepthConst.TopDepth);
            }
            else if(gameState == GameState.Pause)
            {
                SwitchGameState(GameState.InGame);
                UIManager.Instance.Pop(UIDepthConst.TopDepth);
            }
        }
    }

    public void SwitchGameState(GameState targetState)
    {
        gameState = targetState;
        switch (gameState)
        {
            case GameState.CG:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.InGame:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.Pause:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.End:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}

public enum GameState
{
    CG,
    InGame,
    Pause,
    End
}
