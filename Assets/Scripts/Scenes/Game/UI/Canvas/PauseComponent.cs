using Config;

using DG.Tweening;

using Scenes.Game.States;

using UnityEngine;

namespace Scenes.Game.UI
{
	public class PauseComponent : MonoBehaviour
	{
		private CanvasGroup canvas;
		private GameStateTransition state;

		public void Awake()
		{
			canvas = GetComponent<CanvasGroup>();
			canvas.alpha = 0;
			gameObject.SetActive(false);
			this.Subscribe<GameStateTransition>(string.Format(EventTopics.GameStateEnter, GameStates.Pause.ToString()), OnPauseEnter);
			this.Subscribe<GameStateTransition>(string.Format(EventTopics.GameStateExit, GameStates.Pause.ToString()), OnPauseExit);
		}

		public void Resume()
		{
			state?.Finish();
		}

		private void OnPauseEnter(GameStateTransition obj)
		{
			state = obj;
			gameObject.SetActive(true);
			canvas.DOFade(1, 0.4f).SetUpdate(UpdateType.Normal, true).OnComplete(obj.EnterFinish);
		}

		private void OnPauseExit(GameStateTransition obj) => canvas.DOFade(0, 0.4f).SetUpdate(UpdateType.Normal, true).OnComplete(() =>
		{
			gameObject.SetActive(false);
			obj.ExitFinish();
		});
	}
}
