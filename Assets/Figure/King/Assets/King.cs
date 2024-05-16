using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    List<Vector3> points = new List<Vector3>()
    {
        new Vector3(1, 0, 0), new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1), new Vector3(0, 0, -1),
        new Vector3(1, 0, 1), new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1), new Vector3(-1, 0, -1)
    };
    List<Vector3> castlingPoints = new List<Vector3>()
    {
        new Vector3(2, 0, 0), new Vector3(-2, 0, 0)
    };
    bool firstMove = true;
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
					Figure newParentFigure = figure.hit.transform.gameObject.GetComponentInChildren<Figure>();
					if (newParentFigure != null && newParentFigure.opponent != figure.opponent)
					{
						gameController.quene = !figure.opponent;
						Destroy(newParentFigure.gameObject);
						figure.parentObj = figure.hit.transform.gameObject;
						transform.SetParent(figure.parentObj.transform);
						firstMove = false;
					}
					else if (newParentFigure == null)
					{
						gameController.quene = !figure.opponent;
						figure.parentObj = figure.hit.transform.gameObject;
						transform.SetParent(figure.parentObj.transform);
						firstMove = false;
					}
				}
			}
		}
		if (firstMove)
		{
			Vector3 oldParent = figure.parentObj.transform.position;
			Vector3 newParent = figure.hit.transform.position;

			foreach (Vector3 castlP in castlingPoints)
			{
				if (newParent == castlP + oldParent)
				{
					bool left = Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), oldParent + new Vector3(figure.opponent ? 1 : -1, 0.2f, 0), out RaycastHit leftHit, 1, 64);
					bool right = Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), oldParent + new Vector3(figure.opponent ? -1 : 1, 0.2f, 0), out RaycastHit rightHit, 1, 64);
					Debug.Log(left);
					Debug.Log(right);

					if (left && leftHit.transform.gameObject.GetComponent<Rook>() != null && leftHit.transform.gameObject.GetComponent<Rook>().moveNum == 1 && castlP.x == (figure.opponent ? 2 : -2))
					{
						Debug.Log("YesL");
						figure.parentObj = figure.hit.transform.gameObject;
						transform.SetParent(figure.parentObj.transform);
						firstMove = false;
					}
					else if (right && rightHit.transform.gameObject.GetComponent<Rook>() != null && rightHit.transform.gameObject.GetComponent<Rook>().moveNum == 1 && castlP.x == (figure.opponent ? -2 : 2))
					{
						Debug.Log("YesR");
						figure.parentObj = figure.hit.transform.gameObject;
						transform.SetParent(figure.parentObj.transform);
						firstMove = false;
					}
				}
			}
		}
	}

    private void OnDestroy()
    {
        Application.Quit();
    }
}
