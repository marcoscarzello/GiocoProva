using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlacer : MonoBehaviour
{
	public GameObject LightToPlace;

	public void DoThePlacement()
	{
		Transform[] kids = this.GetComponentsInChildren<Transform>();

		for (int i = 1; i < kids.Length; i++)
		{
			GameObject prefab = Instantiate(LightToPlace, this.transform, true);
			prefab.transform.position = kids[i].transform.position;
			prefab.name = $"light_model {i}" ;
		}
	}
}
