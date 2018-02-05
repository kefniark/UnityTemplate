using Config;

using DG.Tweening;

using Scenes.Game.Components.Characters;

using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Game.UI
{
	public class CharacterUi : MonoBehaviour
	{
		public Text Name;
		public Text Score;
		public Image Health;
		
		public PlayerData Player { get; private set; }
		public CharacterComponent Character { get; private set; }

		public void Setup(PlayerData player)
		{
			Player = player;
			Name.text = player.Name;

			this.Subscribe<CharacterComponent>(EventTopics.GameCharacterSpawn, OnCharacterSpawn);
			this.Subscribe<PlayerData>(EventTopics.GamePlayerUpdated, OnPlayerUpdate);
			UpdateScoreUi();
		}

		private void OnPlayerUpdate(PlayerData player)
		{
			if (Player != player)
			{
				return;
			}

			UpdateScoreUi();
		}

		private void OnCharacterSpawn(CharacterComponent character)
		{
			if (Player != character.Player)
			{
				return;
			}

			if (Character != null)
			{
				Character.PropertyChanged -= Character_PropertyChanged;
			}

			Character = character;
			Character.PropertyChanged += Character_PropertyChanged;
			UpdateHealthUi();
		}

		private void UpdateHealthUi()
		{
			Health.DOFillAmount(1f * Character.CurrentHealth / Character.Health, 0.25f).SetUpdate(true);
			if (Character.CurrentHealth == Character.Health)
			{
				return;
			}

			if (Character.CurrentHealth == 0)
			{
				transform.GetComponent<RectTransform>().DOShakeAnchorPos(0.5f, 8).SetUpdate(true);
				return;
			}
			transform.GetComponent<RectTransform>().DOShakeAnchorPos(0.3f, 1).SetUpdate(true);
		}

		private void UpdateScoreUi() => Score.text = Player.Score.ToString();

		private void Character_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Health":
					UpdateHealthUi();
					break;
			}
		}
	}
}
