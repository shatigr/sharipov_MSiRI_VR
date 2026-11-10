using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Вызывается при столкновении
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что мы столкнулись не с самим собой
        if (collision.gameObject != gameObject)
        {
            Debug.Log("Столкновение! " + gameObject.name + " врезался в " + collision.gameObject.name);
            
            // Можно немного изменить цвет при ударе, чтобы было видно
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}