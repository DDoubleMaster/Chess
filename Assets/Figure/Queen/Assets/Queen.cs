using System;
using UnityEngine;

public class Queen : MonoBehaviour
{
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

			if ((oldParent.x == newParent.x || oldParent.z == newParent.z) && oldParent != newParent)
				figure.MoveChecher(oldParent, newParent);
			else if (Mathf.Abs((float)Math.Round(oldParent.x, 2) - (float)Math.Round(newParent.x, 2)) == Mathf.Abs((float)Math.Round(oldParent.z, 2) - (float)Math.Round(newParent.z, 2)) && oldParent != newParent)
				figure.MoveChecher(oldParent, newParent);
		}
    }


}
