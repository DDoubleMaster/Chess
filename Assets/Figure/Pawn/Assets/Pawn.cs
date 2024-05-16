using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    List<Vector3> points;
    List<Vector3> killPoints;
    Figure figure;
	GameController gameController;

	private void Start()
    {
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		figure = GetComponent<Figure>();

        if (figure.opponent)
        {
            points = new List<Vector3>() { new Vector3(0, 0, -1), new Vector3(0, 0, -2) };
            killPoints = new List<Vector3>() { new Vector3(1, 0, -1), new Vector3(-1, 0, -1) };
        }
        else
        {
            points = new List<Vector3>() { new Vector3(0, 0, 1), new Vector3(0, 0, 2) };
            killPoints = new List<Vector3>() { new Vector3(1, 0, 1), new Vector3(-1, 0, 1) };
        }
    }

    bool leftRay;
    bool rightRay;
    bool firstMove;
    RaycastHit leftHit;
    RaycastHit rightHit;

    private void OnMouseUp()
    {
        if (gameController.quene == figure.opponent)
        {
			Vector3 oldParent = figure.parentObj.transform.position;
			Vector3 newParent = figure.hit.transform.position;

			foreach (Vector3 point in points)
			{
				if (newParent == point + oldParent)
				{
					RaycastHit hitFigure;
					Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), newParent - oldParent, out hitFigure, 20);

					if (hitFigure.collider != null && Vector3.Distance(oldParent, hitFigure.transform.parent.transform.position) > Vector3.Distance(oldParent, newParent))
						LocalIsPoint();
					else if (hitFigure.collider == null)
						LocalIsPoint();
				}
			}
			if (points.Count > 1)
				points.RemoveAt(1);
			foreach (Vector3 killPoint in killPoints)
			{
				if (newParent == killPoint + oldParent)
				{
					Figure newParentFigure = figure.hit.transform.gameObject.GetComponentInChildren<Figure>();
					leftRay = Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), oldParent + new Vector3((figure.opponent ? 1 : -1), 0.2f, 0), out leftHit, 1, 64);
					rightRay = Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), oldParent + new Vector3((figure.opponent ? -1 : 1), 0.2f, 0), out rightHit, 1, 64);

					if (newParentFigure != null && newParentFigure.opponent != figure.opponent)
					{
						LocalIsKillPoint(newParentFigure);
					}
					else if (newParentFigure == null)
					{
						bool left = leftRay && killPoint.x == (figure.opponent ? 1 : -1);
						bool right = rightRay && killPoint.x == (figure.opponent ? -1 : 1);

						if (left && leftHit.transform.gameObject.GetComponent<Figure>().opponent != figure.opponent && leftHit.transform.gameObject.GetComponent<Pawn>() != null && leftHit.transform.gameObject.GetComponent<Pawn>().firstMove)
						{
							LocalIsKillPoint(newParentFigure);
						}
						else if (right && rightHit.transform.gameObject.GetComponent<Figure>().opponent != figure.opponent && rightHit.transform.gameObject.GetComponent<Pawn>() != null && rightHit.transform.gameObject.GetComponent<Pawn>().firstMove)
						{
							LocalIsKillPoint(newParentFigure);
						}
					}

				}
			}
		}
    }
    
    private void LocalIsPoint()
    {
        figure.IsPoint();

        if (points.Count == 1)
            firstMove = false;
        if (points.Count > 1)
			firstMove = true;
	}

    private void LocalIsKillPoint(Figure newLocalParentObj)
    {
		gameController.quene = !figure.opponent;

        Destroy(newLocalParentObj.gameObject);
        figure.parentObj = figure.hit.transform.gameObject;
        transform.SetParent(figure.parentObj.transform);
    }
}
