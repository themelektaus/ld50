using UnityEngine;

public static class InputManager
{
	public class MouseButton
	{
		readonly int button;

		public MouseButton(int button) {
			this.button = button;
		}

		public bool holding { get => Input.GetMouseButton(button); }
		public bool down { get => Input.GetMouseButtonDown(button); }
		public bool up { get => Input.GetMouseButtonUp(button); }
	}
	
	public static MouseButton leftMouseButton = new MouseButton(0);
	public static MouseButton rightMouseButton = new MouseButton(1);
	public static MouseButton middleMouseButton = new MouseButton(2);
	
	public static Vector2 mousePosition { get => Input.mousePosition; }
	
	public class AxisOrButton
	{
		readonly string name;

		public AxisOrButton(string name) {
			this.name = name;
		}

		public float axis { get => Input.GetAxis(name); }
		public float axisRaw { get => Input.GetAxisRaw(name); }
		public bool holding { get => Input.GetButton(name); }
		public bool down { get => Input.GetButtonDown(name); }
		public bool up { get => Input.GetButtonUp(name); }
	}
	
	// Cancel, (Input Types: KeyOrMouseButton)
	public static AxisOrButton cancel = new AxisOrButton("Cancel");
	
	// Fire1, (Input Types: KeyOrMouseButton)
	public static AxisOrButton fire1 = new AxisOrButton("Fire1");
	
	// Fire2, (Input Types: KeyOrMouseButton)
	public static AxisOrButton fire2 = new AxisOrButton("Fire2");
	
	// Fire3, (Input Types: KeyOrMouseButton)
	public static AxisOrButton fire3 = new AxisOrButton("Fire3");
	
	// Horizontal, (Input Types: KeyOrMouseButton, JoystickAxis)
	public static AxisOrButton horizontal = new AxisOrButton("Horizontal");
	
	// Jump, (Input Types: KeyOrMouseButton)
	public static AxisOrButton jump = new AxisOrButton("Jump");
	
	// Mouse ScrollWheel, (Input Types: MouseMovement)
	public static AxisOrButton mouseScrollWheel = new AxisOrButton("Mouse ScrollWheel");
	
	// Mouse X, (Input Types: MouseMovement)
	public static AxisOrButton mouseX = new AxisOrButton("Mouse X");
	
	// Mouse Y, (Input Types: MouseMovement)
	public static AxisOrButton mouseY = new AxisOrButton("Mouse Y");
	
	// Submit, (Input Types: KeyOrMouseButton)
	public static AxisOrButton submit = new AxisOrButton("Submit");
	
	// Vertical, (Input Types: KeyOrMouseButton, JoystickAxis)
	public static AxisOrButton vertical = new AxisOrButton("Vertical");
	
}
