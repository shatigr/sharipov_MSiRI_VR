using UnityEngine;

public class BilliardSpawner : MonoBehaviour
{
    // Массив с названиями
    private string[] shapeNames = { "Биток", "Шар_1", "Шар_2", "Шар_3" };

    void Start()
    {
        for (int i = 0; i < shapeNames.Length; i++)
        {
            // Создаем сферу
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = shapeNames[i];
            
            // Убираем гравитацию, чтобы шары не улетали вниз
            Rigidbody rb = sphere.AddComponent<Rigidbody>();
            rb.useGravity = false; 
            
            // ВАЖНО: Добавляем обработчик столкновений на каждый шар
            sphere.AddComponent<CollisionHandler>();

            // Расставляем их в виде бильярдного стола
            if (i == 0)
            {
                // Биток ставим далеко отдельно
                sphere.transform.position = new Vector3(0, 0, -5);
                sphere.AddComponent<CueBallStrike>();
            }
            else if (i == 1)
            {
                // Шар 1 - ставим его самым первым (вершина треугольника)
                sphere.transform.position = new Vector3(0, 0, 0);
            }
            else if (i == 2)
            {
                // Шар 2 - ставим сзади и слева от первого
                sphere.transform.position = new Vector3(-0.75f, 0, 1f);
            }
            else if (i == 3)
            {
                // Шар 3 - ставим сзади и справа от первого
                sphere.transform.position = new Vector3(0.75f, 0, 1f);
            }
            
            // Делаем их разных цветов
            sphere.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
}