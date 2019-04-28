using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPossionDisc : MonoBehaviour
{
	private PossionDisc possionDisc;
	private List<Vector3> points;

	public void Start()
	{
		delayPlayer = new STimeStateTracker();
		possionDisc = new PossionDisc(200,200,2,30);
		StartCoroutine(MakePossionDisc());
	}

	private List<Vector3> totalPoints;
	IEnumerator MakePossionDisc()
	{
		
		possionDisc.Begin(100,100);
		do
		{
			bool isOver = false;
			bool hasSpwan = false;
			Vector3 spwanPoint = Vector3.zero;
		  totalPoints =	possionDisc.Next(ref isOver,ref hasSpwan,ref spwanPoint);
		  if (hasSpwan)
		  {
			  GameObject newPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			  newPoint.transform.position = spwanPoint;
			  Material m = new Material(Shader.Find("Unlit/Color"));
			  newPoint.GetComponent<Renderer>().material = m;
			  newPoint.GetComponent<Renderer>().material.color = Color.red;
			  yield return 1;
		  }
		  else
		  {
			  yield return 0;
		  }

		  if (isOver == true)
		  {
			  yield return 1;
			  break;
		  }
		} while (true);
	}

	
}
