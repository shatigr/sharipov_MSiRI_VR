using UnityEngine;

public class CueBallStrike : MonoBehaviour
{
    public float force = 50f; // Сила удара

    void Update()
    {
        // При нажатии на пробел бьем по битку
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Даем импульс вперед (по оси Z)
            GetComponent<Rigidbody>().AddForce(Vector3.forward * force, ForceMode.Impulse);
            Debug.Log("Удар по битку!");
        }
    }
}