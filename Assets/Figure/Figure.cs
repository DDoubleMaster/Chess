using UnityEngine;

public class Figure : MonoBehaviour
{
    GameController gameController;
    Ray mouseDirection;
    public RaycastHit hit;
    bool selected;
    bool inPlane;
    public bool opponent;
    [HideInInspector] public GameObject parentObj;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit firstHit, Mathf.Infinity, 8);
        parentObj = firstHit.transform.gameObject;
        transform.position = parentObj.transform.position;
        transform.SetParent(parentObj.transform);
    }

    private void Update()
    {
        Vector3 figurePos = selected && inPlane ? hit.transform.position + new Vector3(0, selected ? 2 : 0, 0) : parentObj.transform.position;
        transform.position = Vector3.Lerp(transform.position, figurePos, 0.2f);
    }

    private void OnMouseDrag()
    {
        if (gameController.quene == opponent)
        {
			selected = true;
			mouseDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(mouseDirection, out hit, Mathf.Infinity, 8);
			inPlane = Physics.Raycast(mouseDirection, out hit, Mathf.Infinity, 8) ? true : false;
		}
    }

    private void OnMouseUp()
    {
        selected = false;
    }

    public void IsPoint()
    {
        Debug.Log("Move Complete");
        Figure newParentFigure = hit.transform.gameObject.GetComponentInChildren<Figure>();
        if(newParentFigure != null && newParentFigure.opponent != opponent)
        {
            Destroy(newParentFigure.gameObject);
            parentObj = hit.transform.gameObject;
            transform.SetParent(parentObj.transform);
            gameController.quene = !opponent;
        }
        else if (newParentFigure == null)
        {
            parentObj = hit.transform.gameObject;
            transform.SetParent(parentObj.transform);
			gameController.quene = !opponent;
		}
    }

    public void MoveChecher(Vector3 oldParent, Vector3 newParent)
    {
        RaycastHit hitFigure;
        Physics.Raycast(oldParent + new Vector3(0, 0.2f, 0), newParent - oldParent, out hitFigure, 20);

        if (hitFigure.collider != null && Vector3.Distance(oldParent, hitFigure.transform.parent.transform.position) >= Vector3.Distance(oldParent, newParent))
            IsPoint();
        else if (hitFigure.collider == null)
            IsPoint();
    }
}
