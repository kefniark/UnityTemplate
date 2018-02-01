using Config;

using DG.Tweening;

using Scenes.Game.States;

using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.UI
{
	public class IntroComponent : MonoBehaviour
	{
		private CanvasGroup canvas;

		public void Awake()
		{
			canvas = GetComponent<CanvasGroup>();
			canvas.alpha = 0;
			gameObject.SetActive(false);

			this.Subscribe(string.Format(EventTopics.GameStateEnter, GameStates.Intro.ToString()), () =>
			{
				canvas.alpha = 1;
				gameObject.SetActive(true);
			});

			this.Subscribe(string.Format(EventTopics.GameStateExited, GameStates.Intro.ToString()), () =>
			{
				canvas.alpha = 0;
				gameObject.SetActive(false);
			});

			this.Subscribe<GameStateTransition>(string.Format(EventTopics.GameStateEntered, GameStates.Intro.ToString()), OnIntroBegan);
		}

		private void OnIntroBegan(GameStateTransition state)
		{
			// Get Component
			var text = GetComponentInChildren<Text>();
			var textRect = text.GetComponent<RectTransform>();

			// Animation sequence
			Sequence sequence = DOTween.Sequence();
			sequence.Append(textRect.DOLocalMoveX(800, 0));
			sequence.Append(textRect.DOLocalMoveX(0, 0.4f));
			sequence.Append(textRect.DOLocalMoveX(0, 1));
			sequence.Append(textRect.DOLocalMoveX(-800, 0.4f));
			sequence.AppendCallback(state.Finish);
			sequence.SetUpdate(UpdateType.Normal, true);
		}
	}
}
