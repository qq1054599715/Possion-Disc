using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPossionDisc2 : MonoBehaviour
{
	private PossionDisc2 possionDisc;
	private List<Vector3> points;

	public void Start()
	{
		possionDisc = new PossionDisc2(150,150,5,30, (point) =>
		{
			if (Vector3.Distance(point, new Vector3(45, 100)) < 15)
			{
				return false;
			}

			if (Vector3.Distance(point, new Vector3(115, 100)) < 15)
			{
				return false;
			}
			
			if (Vector3.Distance(point, new Vector3(75, 50)) < 15)
			{
				return false;
			}
			
			return true;
		});
		StartCoroutine(MakePossionDisc());
	}

	private List<Vector3> totalPoints;
	IEnumerator MakePossionDisc()
	{
		float radians = UnityEngine.Random.Range(0, Mathf.PI * 2);
		Vector3 newPos = new Vector3(Mathf.Cos(radians),Mathf.Sin(radians))*UnityEngine.Random.Range(30f,31f)+new Vector3(100,100);
		possionDisc.Begin(newPos.x,newPos.y);
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

	private void Update()
	{
		
	}
	
}
