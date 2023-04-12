using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseoverTarget : MonoBehaviour
{
	public UIController UI;

	public Color mouseoverColor;
	private Color originalColor;

	private GameObject parent;
	private MeshRenderer meshRenderer;

    void Start()
    {
		meshRenderer = GetComponent<MeshRenderer>();
		originalColor = meshRenderer.material.color;
		UI = UIController.Instance;
		parent = transform.parent.gameObject;
    }

	
	private void OnMouseEnter()
	{
		meshRenderer.material.color = mouseoverColor;
	}

	private void OnMouseExit()
	{
		meshRenderer.material.color = originalColor;
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			UI.SelectUnit(parent);
		}
	}

}
