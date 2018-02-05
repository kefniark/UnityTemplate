using DG.Tweening;

using UnityEngine;

public class ShockwaveFx : MonoBehaviour
{
	public Vector2 Duration = new Vector2(0.3f, 0.8f);
	public Vector2 Intensity = new Vector2(0.001f, 0.05f);

	public void OnEnable()
	{
		foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
		{
			var duration = Random.Range(Duration.x, Duration.y);
			sprite.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			sprite.transform.DOScale(new Vector3(12, 12, 12), duration).SetDelay(Random.Range(0f, 0.2f)).SetEase(Ease.InOutSine);

			sprite.GetComponent<Renderer>().material.SetFloat("_Intensity", Random.Range(Intensity.x, Intensity.y));
		}
	}
}
