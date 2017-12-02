using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransformExtension))]
public class TransformCustom : Editor {
	
	private TransformExtension t = null;

	void OnEnable() {
		t = (TransformExtension)target;
	}

	public override void OnInspectorGUI() {
		EditorGUILayout.Space ();
		t.mode = (TransformExtension.ActionMode)EditorGUILayout.EnumPopup("Transform Mode", t.mode);
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		switch (t.mode) {
		case TransformExtension.ActionMode.Freeze:
			Freeze ();
			break;
		case TransformExtension.ActionMode.Follow:
			Follow ();
			break;
		case TransformExtension.ActionMode.Copy:
			Copy ();
			break;
		case TransformExtension.ActionMode.Apply:
			Apply ();
			break;
		case TransformExtension.ActionMode.Focus:
			Focus ();
			break;
		case TransformExtension.ActionMode.None:
			EditorGUILayout.LabelField ("Please select a mode", EditorStyles.centeredGreyMiniLabel);
			break;
		default:
			EditorGUILayout.LabelField ("An error occurred", EditorStyles.centeredGreyMiniLabel);
			break;
		}
	}

	void Freeze () {
		t.positionAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Positions Axis", t.positionAxis);
		t.rotationAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Rotations Axis", t.rotationAxis);
		t.scaleAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Scales Axis", t.scaleAxis);
		EditorGUILayout.Space ();
		if (t.positionAxis == TransformExtension.Axis.None && t.rotationAxis == TransformExtension.Axis.None && t.scaleAxis == TransformExtension.Axis.None) {
			EditorGUILayout.LabelField ("Please select an axis", EditorStyles.centeredGreyMiniLabel);
		}
	}

	void Follow () {
		t.target = EditorGUILayout.ObjectField ("Target", t.target, typeof(Transform), true) as Transform;
		EditorGUILayout.Space ();
		if (t.target == null) {
			EditorGUILayout.LabelField ("Please assign a target", EditorStyles.centeredGreyMiniLabel);
		} else {
			t.mirror = EditorGUILayout.Toggle ("Mirror", t.mirror);
			EditorGUILayout.Space ();
			t.positionAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Positions Axis", t.positionAxis);
			t.rotationAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Rotations Axis", t.rotationAxis);
			t.scaleAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Scales Axis", t.scaleAxis);
			EditorGUILayout.Space ();
			if (t.positionAxis == TransformExtension.Axis.None && t.rotationAxis == TransformExtension.Axis.None && t.scaleAxis == TransformExtension.Axis.None) {
				EditorGUILayout.LabelField ("Please select an axis", EditorStyles.centeredGreyMiniLabel);
			}
		}
	}

	void Copy () {
		t.target = EditorGUILayout.ObjectField ("Target", t.target, typeof(Transform), true) as Transform;
		EditorGUILayout.Space ();
		if (t.target == null) {
			EditorGUILayout.LabelField ("Please assign a target", EditorStyles.centeredGreyMiniLabel);
		} else {
			t.positionAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Positions Axis", t.positionAxis);
			t.rotationAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Rotations Axis", t.rotationAxis);
			t.scaleAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Scales Axis", t.scaleAxis);
			EditorGUILayout.Space ();
			if (t.positionAxis == TransformExtension.Axis.None && t.rotationAxis == TransformExtension.Axis.None && t.scaleAxis == TransformExtension.Axis.None) {
				EditorGUILayout.LabelField ("Please select an axis", EditorStyles.centeredGreyMiniLabel);
			}
		}
	}

	void Apply () {
		Move ();
		Spin ();
		Grow ();
		if (t.moveAxis == TransformExtension.Axis.None && t.spinAxis == TransformExtension.Axis.None && t.growAxis == TransformExtension.Axis.None){
			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("Please select an axis", EditorStyles.centeredGreyMiniLabel);
		}
	}

	void Move () {
		t.moveAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Move Axis", t.moveAxis);
		if (t.moveAxis != TransformExtension.Axis.None) {
			t.positionWorld = EditorGUILayout.Toggle ("World", t.positionWorld);
		}
		switch (t.moveAxis) {
		case TransformExtension.Axis.All:
			t.moveSpeedX = EditorGUILayout.Slider ("Speed X", t.moveSpeedX, -100f, 100f);
			t.moveSpeedY = EditorGUILayout.Slider ("Speed Y", t.moveSpeedY, -100f, 100f);
			t.moveSpeedZ = EditorGUILayout.Slider ("Speed Z", t.moveSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.XY:
			t.moveSpeedX = EditorGUILayout.Slider ("Speed X", t.moveSpeedX, -100f, 100f);
			t.moveSpeedY = EditorGUILayout.Slider ("Speed Y", t.moveSpeedY, -100f, 100f);
			t.moveSpeedZ = 0f;
			break;
		case TransformExtension.Axis.YZ:
			t.moveSpeedX = 0f;
			t.moveSpeedY = EditorGUILayout.Slider ("Speed Y", t.moveSpeedY, -100f, 100f);
			t.moveSpeedZ = EditorGUILayout.Slider ("Speed Z", t.moveSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.XZ:
			t.moveSpeedX = EditorGUILayout.Slider ("Speed X", t.moveSpeedX, -100f, 100f);
			t.moveSpeedY = 0f;
			t.moveSpeedZ = EditorGUILayout.Slider ("Speed Z", t.moveSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.X:
			t.moveSpeedX = EditorGUILayout.Slider ("Speed X", t.moveSpeedX, -100f, 100f);
			t.moveSpeedY = 0f;
			t.moveSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Y:
			t.moveSpeedX = 0f;
			t.moveSpeedY = EditorGUILayout.Slider ("Speed Y", t.moveSpeedY, -100f, 100f);
			t.moveSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Z:
			t.moveSpeedX = 0f;
			t.moveSpeedY = 0f;
			t.moveSpeedZ = EditorGUILayout.Slider ("Speed Z", t.moveSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.None:
			t.moveSpeedX = 0f;
			t.moveSpeedY = 0f;
			t.moveSpeedZ = 0f;
			break;
		default:
			EditorGUILayout.LabelField ("An error occurred", EditorStyles.centeredGreyMiniLabel);
			break;
		}
		if (t.moveAxis != TransformExtension.Axis.None) {
			if (t.moveSpeedX == 0f && t.moveSpeedY == 0f && t.moveSpeedZ == 0f) {
				EditorGUILayout.LabelField ("Please assign a float", EditorStyles.centeredGreyMiniLabel);
			}
			EditorGUILayout.Space ();
		}
	}

	void Spin () {
		if (t.spinAxis != TransformExtension.Axis.None) {
			EditorGUILayout.Space ();
		}
		t.spinAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Spin Axis", t.spinAxis);
		if (t.spinAxis != TransformExtension.Axis.None) {
			t.rotationWorld = EditorGUILayout.Toggle ("World", t.rotationWorld);
		}
		switch (t.spinAxis) {
		case TransformExtension.Axis.All:
			t.spinSpeedX = EditorGUILayout.Slider ("Speed X", t.spinSpeedX, -360f, 360f);
			t.spinSpeedY = EditorGUILayout.Slider ("Speed Y", t.spinSpeedY, -360f, 360f);
			t.spinSpeedZ = EditorGUILayout.Slider ("Speed Z", t.spinSpeedZ, -360f, 360f);
			break;
		case TransformExtension.Axis.XY:
			t.spinSpeedX = EditorGUILayout.Slider ("Speed X", t.spinSpeedX, -360f, 360f);
			t.spinSpeedY = EditorGUILayout.Slider ("Speed Y", t.spinSpeedY, -360f, 360f);
			t.spinSpeedZ = 0f;
			break;
		case TransformExtension.Axis.YZ:
			t.spinSpeedX = 0f;
			t.spinSpeedY = EditorGUILayout.Slider ("Speed Y", t.spinSpeedY, -360f, 360f);
			t.spinSpeedZ = EditorGUILayout.Slider ("Speed Z", t.spinSpeedZ, -360f, 360f);
			break;
		case TransformExtension.Axis.XZ:
			t.spinSpeedX = EditorGUILayout.Slider ("Speed X", t.spinSpeedX, -360f, 360f);
			t.spinSpeedY = 0f;
			t.spinSpeedZ = EditorGUILayout.Slider ("Speed Z", t.spinSpeedZ, -360f, 360f);
			break;
		case TransformExtension.Axis.X:
			t.spinSpeedX = EditorGUILayout.Slider ("Speed X", t.spinSpeedX, -360f, 360f);
			t.spinSpeedY = 0f;
			t.spinSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Y:
			t.spinSpeedX = 0f;
			t.spinSpeedY = EditorGUILayout.Slider ("Speed Y", t.spinSpeedY, -360f, 360f);
			t.spinSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Z:
			t.spinSpeedX = 0f;
			t.spinSpeedY = 0f;
			t.spinSpeedZ = EditorGUILayout.Slider ("Speed Z", t.spinSpeedZ, -360f, 360f);
			break;
		case TransformExtension.Axis.None:
			t.spinSpeedX = 0f;
			t.spinSpeedY = 0f;
			t.spinSpeedZ = 0f;
			break;
		default:
			EditorGUILayout.LabelField ("An error occurred", EditorStyles.centeredGreyMiniLabel);
			break;
		}
		if (t.spinAxis != TransformExtension.Axis.None) {
			if (t.spinSpeedX == 0f && t.spinSpeedY == 0f && t.spinSpeedZ == 0f) {
				EditorGUILayout.LabelField ("Please assign a float", EditorStyles.centeredGreyMiniLabel);
			}
			EditorGUILayout.Space ();
		}
	}

	void Grow () {
		if (t.growAxis != TransformExtension.Axis.None) {
			EditorGUILayout.Space ();
		}
		t.growAxis = (TransformExtension.Axis)EditorGUILayout.EnumPopup ("Grow Axis", t.growAxis);
		switch (t.growAxis) {
		case TransformExtension.Axis.All:
			t.growSpeedX = EditorGUILayout.Slider ("Grow X", t.growSpeedX, -100f, 100f);
			t.growSpeedY = EditorGUILayout.Slider ("Grow Y", t.growSpeedY, -100f, 100f);
			t.growSpeedZ = EditorGUILayout.Slider ("Grow Z", t.growSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.XY:
			t.growSpeedX = EditorGUILayout.Slider ("Grow X", t.growSpeedX, -100f, 100f);
			t.growSpeedY = EditorGUILayout.Slider ("Grow Y", t.growSpeedY, -100f, 100f);
			t.growSpeedZ = 0f;
			break;
		case TransformExtension.Axis.YZ:
			t.growSpeedX = 0f;
			t.growSpeedY = EditorGUILayout.Slider ("Grow Y", t.growSpeedY, -100f, 100f);
			t.growSpeedZ = EditorGUILayout.Slider ("Grow Z", t.growSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.XZ:
			t.growSpeedX = EditorGUILayout.Slider ("Grow X", t.growSpeedX, -100f, 100f);
			t.growSpeedY = 0f;
			t.growSpeedZ = EditorGUILayout.Slider ("Grow Z", t.growSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.X:
			t.growSpeedX = EditorGUILayout.Slider ("Grow X", t.growSpeedX, -100f, 100f);
			t.growSpeedY = 0f;
			t.growSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Y:
			t.growSpeedX = 0f;
			t.growSpeedY = EditorGUILayout.Slider ("Grow Y", t.growSpeedY, -100f, 100f);
			t.growSpeedZ = 0f;
			break;
		case TransformExtension.Axis.Z:
			t.growSpeedX = 0f;
			t.growSpeedY = 0f;
			t.growSpeedZ = EditorGUILayout.Slider ("Grow Z", t.growSpeedZ, -100f, 100f);
			break;
		case TransformExtension.Axis.None:
			t.growSpeedX = 0f;
			t.growSpeedY = 0f;
			t.growSpeedZ = 0f;
			break;
		default:
			EditorGUILayout.LabelField ("An error occurred", EditorStyles.centeredGreyMiniLabel);
			break;
		}
		if (t.growAxis != TransformExtension.Axis.None) {
			if (t.growSpeedX == 0f && t.growSpeedY == 0f && t.growSpeedZ == 0f) {
				EditorGUILayout.LabelField ("Please assign a float", EditorStyles.centeredGreyMiniLabel);
			}
			EditorGUILayout.Space ();
		}
	}

	void Focus () {
		t.target = EditorGUILayout.ObjectField ("Target", t.target, typeof(Transform), true) as Transform;
		EditorGUILayout.Space ();
		if (t.target != null) {
			t.lookAt = EditorGUILayout.Toggle ("Look At", t.lookAt);
			if (t.lookAt == true || t.moveToward == true) {
				EditorGUILayout.Space ();
			}
			t.moveToward = EditorGUILayout.Toggle ("Move Toward", t.moveToward);
			if (t.moveToward == true) {
				t.speed = EditorGUILayout.Slider ("Speed", t.speed, -100f, 100f);
				EditorGUILayout.Space ();
				if (t.speed == 0f) {
					EditorGUILayout.LabelField ("Please assign a float", EditorStyles.centeredGreyMiniLabel);
				}
			}
			if (t.lookAt == false && t.moveToward == false) {
				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Please assign a type", EditorStyles.centeredGreyMiniLabel);
			}
		} else {
			EditorGUILayout.LabelField ("Please assign a target", EditorStyles.centeredGreyMiniLabel);
		}
	}
}