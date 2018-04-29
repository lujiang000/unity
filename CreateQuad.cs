using System.Collections;
using UnityEditor;
using UnityEngine;

public class CreateQuad : ScriptableWizard {
	public float Height=1f;
	public enum AnchorPoint
	{
		TopLeft,
		TopMiddle,
		TopRight,
		BottomLeft,
		BottomMiddle,
		BottomRight,
		RightMiddle,
		LeftMiddle,
		Center,
		Custom,

	}
	public string MeshName="Quad";
	public string GameObjectName="Plane_Object";
	public string AssetFolder ="Assets";
	public float Width=1f;
	public AnchorPoint Anchor=AnchorPoint.Center;
	public float AnchorX=0.5f;
	public float AnchorY=0.5f;


	// Use this for initialization
	[MenuItem("GameObject/Custom Plane")]
	// Use this for initialization
	static void CreateWizard () {
		ScriptableWizard.DisplayWizard ("Create Plane", typeof(CreateQuad));	

	}
	
	// Update is called once per frame
	void OnWizardCreate(){
		Vector3[] Vertices = new Vector3[4];
		Vertices [0].x = -AnchorX;
		Vertices [0].y = -AnchorY;
		Vertices [1].x = Vertices [0].x + Width;
		Vertices [1].y = Vertices [0].y;
		Vertices [2].x = Vertices [0].x;
		Vertices [2].y = Vertices [0].y+Height;
		Vertices [3].x = Vertices [0].x + Width;
		Vertices [3].y = Vertices [0].y+Height;
		Vector2[] UVs = new Vector2[4];
		UVs [0].x = 0f;
		UVs [0].y = 0f;
		UVs [1].x = 1f;
		UVs [1].y = 0f;
		UVs [2].x = 0f;
		UVs [2].y = 1f;
		UVs [3].x = 1f;
		UVs [3].y = 1f;
		int[] Triangles = new int[6];
		Triangles[0]=3;
		Triangles[1]=1;
		Triangles[2]=2;
		Triangles[3]=2;
		Triangles[4]=1;
		Triangles[5]=0;
		Mesh mesh = new Mesh ();
		mesh.name = MeshName;
		mesh.vertices = Vertices;
		mesh.uv = UVs;
		mesh.triangles = Triangles;
		mesh.RecalculateNormals ();
		AssetDatabase.CreateAsset (mesh, AssetDatabase.GenerateUniqueAssetPath (AssetFolder + "/" + MeshName) + ".asset");
		AssetDatabase.SaveAssets();
		GameObject plane = new GameObject (GameObjectName);
		MeshFilter meshfilter = (MeshFilter)plane.AddComponent (typeof(MeshFilter));
		plane.AddComponent (typeof(MeshRenderer));
		meshfilter.sharedMesh = mesh;
		mesh.RecalculateBounds ();
		plane.AddComponent (typeof(BoxCollider));
	}
	void OnInspectorUpdate(){
		switch (Anchor)
		{
		case AnchorPoint.TopLeft:
			AnchorX = 0f * Width;
			AnchorY = 1f * Height;
			break;
		case AnchorPoint.TopMiddle:
			AnchorX = 0.5f * Width;
			AnchorY = 1f * Height;
			break;
		case AnchorPoint.TopRight:
			AnchorX = 1f * Width;
			AnchorY = 1f * Height;
			break;
		case AnchorPoint.RightMiddle:
			AnchorX = 1f * Width;
			AnchorY = 0.5f * Height;
			break;
		case AnchorPoint.BottomRight:
			AnchorX = 1f * Width;
			AnchorY = 0f * Height;
			break;
		case AnchorPoint.BottomMiddle:
			AnchorX = 0.5f * Width;
			AnchorY = 0f * Height;
			break;
		case AnchorPoint.BottomLeft:
			AnchorX = 0f * Width;
			AnchorY = 0f * Height;
			break;
		case AnchorPoint.LeftMiddle:
			AnchorX = 0f * Width;
			AnchorY = 0.5f * Height;
			break;
		case AnchorPoint.Center:
			AnchorX = 0.5f * Width;
			AnchorY = 0.5f * Height;
			break;
		case AnchorPoint.Custom:
			default:
			break;
		}
	}
	void OnEnable(){
		GetFoldSelection ();
	}
	void GetFoldSelection(){
		if (Selection.objects != null && Selection.objects.Length == 1) {
			AssetFolder = AssetDatabase.GetAssetPath (Selection.objects [0]);
		}
	}
}
