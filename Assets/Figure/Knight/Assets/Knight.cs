using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    List<Vector3> points = new List<Vector3>
    {
        new Vector3(1, 0, 2), new Vector3(2, 0, 1),
        new Vector3(-1, 0, 2), new Vector3(-2, 0, 1),
        new Vector3(1, 0, -2), new Vector3(2, 0, -1),
        new Vector3(-1, 0, -2), new Vector3(-2, 0, -1)
    };
	Figure figure;
	GameController gameController;

	private void Start()
	{
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		figure = GetComponent<Figure>();
	}

    private void OnMouseUp()
    {
        if (gameController.quene == figure.opponent)
        {
			Vector3 oldParent = figure.parentObj.transform.position;
			Vector3 newParent = figure.hit.transform.position;

			foreach (Vector3 p in points)
			{
				if (newParent == p + oldParent)
				{
					figure.IsPoint(); break;
				}

			}
		}
    }
}
