using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MachineSlide : MonoBehaviour
{
    [Header("Границы движения")]
    public Transform startPoint;
    public Transform endPoint;

    private Interactable interactable;
    private Hand grabbingHand; // Какая рука схватила

    void Start()
    {
        interactable = GetComponent<Interactable>();
        
        // Подписываемся на события захвата и отпускания
        interactable.onAttachedToHand += OnGrab;
        interactable.onDetachedFromHand += OnRelease;
    }

    private void OnGrab(Hand hand)
    {
        grabbingHand = hand;
    }

    private void OnRelease(Hand hand)
    {
        grabbingHand = null;
    }

    void Update()
    {
        // Если предмет в руке, двигаем его
        if (grabbingHand != null)
        {
            // Вычисляем линию движения от Start до End
            Vector3 direction = endPoint.position - startPoint.position;
            float maxDistance = direction.magnitude;
            direction.Normalize();

            // Берем позицию руки и проецируем её на нашу линию
            Vector3 handPos = grabbingHand.transform.position;
            Vector3 projectedPos = startPoint.position + Vector3.Project(handPos - startPoint.position, direction);

            // Считаем расстояние от Start до спроецированной точки
            float distanceFromStart = Vector3.Dot(projectedPos - startPoint.position, direction);

            // Ограничиваем (Clamp) это расстояние, чтобы не выехать за Start и End
            distanceFromStart = Mathf.Clamp(distanceFromStart, 0f, maxDistance);

            // Применяем новую позицию
            transform.position = startPoint.position + direction * distanceFromStart;
        }
    }
}