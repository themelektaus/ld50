using UnityEngine;

namespace MT.LD50
{
	public class SimpleInterpolation2D : MonoBehaviour
	{
		public Transform source;
		public Transform target;

		[SerializeField] Vector2 interpolationSpeed = new Vector2(30, 30);
		[SerializeField] bool includeScale = false;
		[SerializeField] bool clampToViewport = false;
		[SerializeField] Vector2 viewportClampX = new Vector2(0, 1);
		[SerializeField] Vector2 viewportClampY = new Vector2(0, 1);

		Vector3 localPosition = Vector3.zero;
		Vector3 localScale = Vector3.zero;

		public void SetPositionImmediatelly() {
			source.position = target.position;
		}

		public void SetScaleImmediatelly() {
			source.localScale = target.localScale;
		}

		void Update() {
			if (!source || !target) {
				return;
			}
			if (interpolationSpeed != Vector2.zero) {
				UpdatePosition();
				if (includeScale) {
					UpdateScale();
				}
			} else {
				source.position = target.position;
				if (includeScale) {
					source.localScale = target.localScale;
				}
			}
		}

		void UpdatePosition() {
			var position = target.position;

			if (clampToViewport) {
				var point = Camera.main.WorldToViewportPoint(position);
				point.x = Mathf.Clamp(point.x, viewportClampX.x, viewportClampX.y);
				point.y = Mathf.Clamp(point.y, viewportClampY.x, viewportClampY.y);
				position = Camera.main.ViewportToWorldPoint(point);
			}

			localPosition = position - source.position;

			float x = interpolationSpeed.x;
			float y = interpolationSpeed.y;

			SetValue(ref localPosition.x, x);
			SetValue(ref localPosition.y, y);

			var newPosition = position - localPosition;

			UpdateValue(ref newPosition.x, position.x, x);
			UpdateValue(ref newPosition.y, position.y, y);

			source.position = newPosition;
		}

		void UpdateScale() {
			var scale = target.localScale;

			localScale = scale - source.localScale;

			float x = interpolationSpeed.x;
			float y = interpolationSpeed.y;

			SetValue(ref localScale.x, x);
			SetValue(ref localScale.y, y);

			var newScale = scale - localScale;

			UpdateValue(ref newScale.x, scale.x, x);
			UpdateValue(ref newScale.y, scale.y, y);

			source.localScale = newScale;
		}

		void SetValue(ref float localValue, float interpolationValue) {
			if (interpolationValue > 0) {
				localValue -= localValue * Mathf.Min(interpolationValue * Time.deltaTime, 1);
			}
		}

		void UpdateValue(ref float newValue, float targetValue, float interpolationValue) {
			if (interpolationValue < 0) {
				newValue = targetValue;
			}
		}
	}
}