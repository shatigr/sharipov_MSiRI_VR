using UnityEngine;

public class Z1_class2 : MonoBehaviour
{
    public Z1_class1 Z1; 

    void Start()
    {
        if (Z1 != null)
        {
            Debug.Log("Сообщение от Z1_class2");
            Debug.Log(Z1.GetInfo());
        }
        else
        {
            Debug.LogError("Ошибка: Переменная Z1 не задана в инспекторе или объект ещё не создан!");
        }
    }
}