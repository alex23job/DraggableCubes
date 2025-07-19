using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggableObject : MonoBehaviour
{
    //private PointerEventData pointerEventData;
    [SerializeField] private float moveSpeed = 10f;
    private Vector3 initialOffset;
    private bool isDragging = false;

    private void OnEnable()
    {
        // Подписываемся на события перетаскивания
        InputActionAsset inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        print($"inputActions=<{inputActions}>");
        inputActions.FindActionMap("Dragging").FindAction("BeginDrag").performed += BeginDrag;
        inputActions.FindActionMap("Dragging").FindAction("Dragging").performed += ContinueDrag;
        inputActions.FindActionMap("Dragging").FindAction("EndDrag").performed += EndDrag;
        inputActions.FindActionMap("Dragging").FindAction("EndDrag").canceled += EndDrag;
    }

    private void OnDisable()
    {
        // Отписываемся от событий
        InputActionAsset inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        inputActions.FindActionMap("Dragging").FindAction("BeginDrag").performed -= BeginDrag;
        inputActions.FindActionMap("Dragging").FindAction("Dragging").performed -= ContinueDrag;
        inputActions.FindActionMap("Dragging").FindAction("EndDrag").performed -= EndDrag;
        inputActions.FindActionMap("Dragging").FindAction("EndDrag").canceled -= EndDrag;
    }

    public void BeginDrag(InputAction.CallbackContext context)
    {
        //print($"BeginDrag context={context}");
        // Находим ближайший объект под курсором
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //print($"hit=<<{hit.collider} {hit.collider.name}>>");
            if (hit.transform == transform)
            {                
                isDragging = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                initialOffset = transform.position - mousePos;
                print($"initialOffset=<{initialOffset}> hit=<<{hit.collider} {hit.collider.name}>>");
            }
        }
    }

    public void ContinueDrag(InputAction.CallbackContext context)
    {
        if (isDragging)
        {
            // Обновляем позицию объекта в зависимости от позиции мыши
            //Vector2 look = context.ReadValue<Vector2>();
            //print($"ContinueDrag look={look} context={context} mouse={Mouse.current.position.ReadValue()}");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            print($"ContinueDrag mouse={Mouse.current.position.ReadValue()}  mouseWorld={mousePos}");
            Vector3 newPosition = mousePos + initialOffset;
            //Vector3 newPosition = transform.position;
            /*look *= Time.deltaTime * moveSpeed;
            newPosition.x += look.x;
            newPosition.y += look.y * 2;*/
            //print($"isDragging => mousePos={mousePos}   pos={transform.position}  newPos={newPosition}  initialOffset={initialOffset}");
            transform.position = newPosition;
        }
    }

    public void EndDrag(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isDragging = false;
            print($"EndDrag  context={context}");
        }
    }
}