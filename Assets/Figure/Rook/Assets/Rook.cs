using UnityEngine;

public class Rook : MonoBehaviour
{
    [HideInInspector]public int moveNum;
	Figure figure;
	GameController gameController;

	private void Start()
	{
		gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
		figure = GetComponent<Figure>();
	}

    private void IsPoint()
    {
        Figure newParentFigure = figure.hit.transform.gameObject.GetComponentInChildren<Figure>();
        if (newParentFigure != null && newParentFigure.opponent != figure.opponent)
        {
            Destroy(newParentFigure.gameObject);
            figure.parentObj = figure.hit.transform.gameObject;
            transform.SetParent(figure.parentObj.transform);
            moveNum++;
        }
        else if (newParentFigure == null)
        {
            figure.parentObj = figure.hit.transform.gameObject;
            transform.SetParent(figure.parentObj.transform);
            moveNum++;
        }
    }

    private void OnMouseUp()
    {
        if (gameController.quene == figure.opponent)
        {
			Vector3 oldParent = figure.parentObj.transform.position;
			Vector3 newParent = figure.hit.transform.position;

			Debug.Log($"{oldParent} {newParent}");
			Debug.Log((oldParent != newParent && oldParent.x == newParent.x) || (oldParent != newParent && oldParent.z == newParent.z));
			if ((oldParent != newParent && oldParent.x == newParent.x) || (oldParent != newParent && oldParent.z == newParent.z))
			{
				RaycastHit hitFigure;
				Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), newParent - oldParent, out hitFigure, 20);

				if (hitFigure.collider != null && Vector3.Distance(oldParent, hitFigure.transform.parent.transform.position) > Vector3.Distance(oldParent, newParent))
					IsPoint();
				else if (hitFigure.collider == null)
					IsPoint();
			}
		}
    }
}
