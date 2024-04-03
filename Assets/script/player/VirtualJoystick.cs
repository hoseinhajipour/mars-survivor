using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystickHandle;

    private Vector2 inputDirection = Vector2.zero;

    private void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>(); // Assuming the joystick handle is the first child
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBackground.sizeDelta.x);
            pos.y = (pos.y / joystickBackground.sizeDelta.y);

            inputDirection = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            // Move joystick handle
            joystickHandle.anchoredPosition = new Vector2(inputDirection.x * (joystickBackground.sizeDelta.x / 3), inputDirection.y * (joystickBackground.sizeDelta.y / 3));
        }
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}
