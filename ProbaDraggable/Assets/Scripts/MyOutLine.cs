using UnityEngine;

[RequireComponent(typeof(OutlineAS))]
public class MyOutLine : MonoBehaviour
{
    [Range(1f,10f)]
    [SerializeField] float width = 3f;
    private OutlineAS myOutline;

    private void OnEnable()
    {
        myOutline = GetComponent<OutlineAS>();
        myOutline.OutlineWidth = 0f;
    }

    private void OnMouseEnter()
    {
        myOutline.OutlineWidth = 3f;
    }

    private void OnMouseExit()
    {
        myOutline.OutlineWidth = 0f;
    }
}
