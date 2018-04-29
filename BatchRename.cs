using System.Collections;
using UnityEngine;
using UnityEditor;
public class BatchRename :ScriptableWizard {
	public string BaseName="MyObject_";
	public int StartNumber = 0;
	public int Increment=1;
	[MenuItem("Edit/Batch Rename..")]
	// Use this for initialization
	static void Start () {
		ScriptableWizard.DisplayWizard ("Batch Name", typeof(BatchRename), "Rename");	
	}
	void OnEnable(){
		UpdateSelectionHelp ();
	}
	void OnSelectionChange(){
		UpdateSelectionHelp ();
	}
	void UpdateSelectionHelp(){
		helpString = "";
		if (Selection.objects != null)
			helpString = "Number"+Selection.objects.Length;
	}
	void OnWizardCreate(){
		if (Selection.objects == null)
			return;
		int PostFix = StartNumber;
		foreach(Object o in Selection.objects){
			o.name = BaseName + PostFix;
			PostFix += Increment;
		}
	}
}
