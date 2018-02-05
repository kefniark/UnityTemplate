using Config;

using DG.Tweening;

using Scenes.Game.Components.Characters;

using UnityEngine;

namespace Scenes.Game.UI
{
	public class CharactersUi : MonoBehaviour
	{
		public RectTransform Rect;
		public GameObject CharacterPrefab;
		public CanvasGroup Canvas;

		public void Awake()
		{
			Rect = GetComponent<RectTransform>();
			this.Subscribe<PlayerData>(EventTopics.GamePlayerCreated, OnPlayerCreated);
			this.Subscribe(string.Format(EventTopics.GameStateEnter, GameStates.Run.ToString()), OnRoundEnter);
			this.Subscribe(string.Format(EventTopics.GameStateEnter, GameStates.RoundResult.ToString()), OnRoundResult);
			Canvas.alpha = 0;
		}

		private void OnRoundEnter() => Canvas.DOFade(1, 0.4f).SetUpdate(true);

		private void OnRoundResult()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(Rect.DOAnchorPos(new Vector2(0, 80), 0.4f).SetEase(Ease.InOutQuad));
			sequence.Join(transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutQuad));
			sequence.AppendInterval(1.5f);
			sequence.Append(Rect.DOAnchorPos(new Vector2(0, 0), 0.6f).SetEase(Ease.InOutQuad));
			sequence.Join(transform.DOScale(1f, 0.6f).SetEase(Ease.InOutQuad));
			sequence.SetUpdate(true);
		}

		private void OnPlayerCreated(PlayerData playerData)
		{
			GameObject go = Instantiate(CharacterPrefab);
			go.transform.SetParent(transform, false);
			var character = go.GetComponent<CharacterUi>();
			if (character != null)
			{
				character.Setup(playerData);
			}
		}
	}
}
