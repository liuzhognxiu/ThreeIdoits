using UnityEngine;
using System.Collections;

/** Helper for LocalSpaceRichAI */
public class LocalSpaceGraph : MonoBehaviour {

	protected Matrix4x4 originalMatrix;
	
	void Start () {
		originalMatrix = transform.localToWorldMatrix;
	}

	public Matrix4x4 GetMatrix ( ) {
		return transform.worldToLocalMatrix * originalMatrix;
	}
}
