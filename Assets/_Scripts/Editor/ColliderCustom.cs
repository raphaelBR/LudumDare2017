using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColliderExtension))]
public class ColliderCustom : Editor {

	private ColliderExtension c = null;

	public SerializedProperty enter;
	public SerializedProperty stay;
	public SerializedProperty exit;

	public SerializedProperty pMaterials;
	public SerializedProperty colliders;
	public SerializedProperty layers;
	private int temporaryTags;

	void OnEnable() {
		c = (ColliderExtension)target;
		layers = serializedObject.FindProperty ("layersList");
		colliders = serializedObject.FindProperty ("collidersList");
		pMaterials = serializedObject.FindProperty ("pMaterialsList");
		enter = serializedObject.FindProperty ("onEnter");
		stay = serializedObject.FindProperty ("onStay");
		exit = serializedObject.FindProperty ("onExit");
	}

	public override void OnInspectorGUI () {
		EditorGUILayout.Space ();
		c.types = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Triggers", c.types);
		if (c.types != ColliderExtension.SelectionTypes.Any || c.rigidbodiesCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.Space ();
		}

		c.rigidbodiesCondition = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Rigidbodies", c.rigidbodiesCondition);
		if (c.rigidbodiesCondition != ColliderExtension.SelectionTypes.Any || c.collidersCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.Space ();
		}

		c.collidersCondition = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Colliders", c.collidersCondition);
		if (c.collidersCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.PropertyField (colliders, true);
			if (c.collidersList.Count == 0) {
				EditorGUILayout.LabelField ("Please add items in the list", EditorStyles.centeredGreyMiniLabel);
			}
		}
		if (c.collidersCondition != ColliderExtension.SelectionTypes.Any || c.layersCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.Space ();
		}

		c.layersCondition = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Layers", c.layersCondition);
		if (c.layersCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.PropertyField (layers, true);
			if (c.layersList.value == 0) {
				EditorGUILayout.LabelField ("Please add items in the list", EditorStyles.centeredGreyMiniLabel);
			}
		}
		if (c.layersCondition != ColliderExtension.SelectionTypes.Any || c.tagsCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.Space ();
		}

		c.tagsCondition = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Tags", c.tagsCondition);
		if (c.tagsCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUI.BeginChangeCheck ();
			temporaryTags = EditorGUILayout.MaskField ("Mask", temporaryTags, UnityEditorInternal.InternalEditorUtility.tags);
			if (EditorGUI.EndChangeCheck ()) {
				c.tagsList.Clear ();
				for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++) {
					int layer = 1 << i;
					if ((temporaryTags & layer) != 0) {
						c.tagsList.Add (UnityEditorInternal.InternalEditorUtility.tags[(int)Mathf.Log (layer, 2)]);
					}
				}
			}
			if (c.tagsList.Count == 0) {
				EditorGUILayout.LabelField ("Please add items in the list", EditorStyles.centeredGreyMiniLabel);
			}
		}
		if (c.tagsCondition != ColliderExtension.SelectionTypes.Any || c.pMaterialsCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.Space ();
		}

		c.pMaterialsCondition = (ColliderExtension.SelectionTypes)EditorGUILayout.EnumPopup("Physic Materials", c.pMaterialsCondition);
		if (c.pMaterialsCondition != ColliderExtension.SelectionTypes.Any) {
			EditorGUILayout.PropertyField (pMaterials, true);
			if (c.pMaterialsList.Count == 0) {
				EditorGUILayout.LabelField ("Please add items in the list", EditorStyles.centeredGreyMiniLabel);
			}
		}

		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (enter);
		EditorGUILayout.PropertyField (stay);
		EditorGUILayout.PropertyField (exit);

		if (c.onEnter == null && c.onStay == null && c.onExit == null) {
			EditorGUILayout.LabelField ("Please add events", EditorStyles.centeredGreyMiniLabel);
		}

		serializedObject.ApplyModifiedProperties ();
	}
}