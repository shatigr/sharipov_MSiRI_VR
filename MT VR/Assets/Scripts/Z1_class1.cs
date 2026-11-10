using UnityEngine;

public class Z1_class1 : MonoBehaviour 
{
    public string _owner;
    public int _balance;

    public string Owner 
    {
        get { return _owner; }
    }

    public int Balance
    {
        get { return _balance; }
        private set 
        {
            if (value < 0) 
            {
                Debug.LogWarning("Баланс не может быть отрицательным! Установлен 0.");
                _balance = 0;
            }
            else
            {
                _balance = value;
            }
        }
    }
    public Z1_class1(string owner, int initialBalance)
    {
        _owner = owner;
        Balance = initialBalance;
    }

    public void Deposit(int amount)
    {
        if (amount > 0)
        {
            Balance += amount; 
        }
    }

    public void Withdraw(int amount)
    {
        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount;
        }
        else
        {
            Debug.Log("Недостаточно средств или неверная сумма!");
        }
    }

    // Получение информации
    public string GetInfo()
    {
        return "Пользователь " + Owner + ". Баланс: " + Balance;
    }
}