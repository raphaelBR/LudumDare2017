using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class AssignMaterial : ScriptableWizard{

	public Material theMaterial;
	private String strHelp = "Select Game Objects";
	private GameObject[] gos;

	void OnWizardUpdate ()
	{
		helpString = strHelp;
		isValid = (theMaterial != null);
	}

	void OnWizardCreate ()
	{
		gos = Selection.gameObjects;
		foreach (GameObject go in gos)
		{
			go.GetComponent<Renderer>().material = theMaterial;
		}
	}

	[MenuItem ("Custom/Assign Material", false, 4)]
	static void assignMaterial()
	{
		ScriptableWizard.DisplayWizard ("Assign Material", typeof(AssignMaterial), "Assign");
	}
}
