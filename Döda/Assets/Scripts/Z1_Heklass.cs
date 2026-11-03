using UnityEngine;

public class Z1_Heklass : MonoBehaviour
{
    public Z1_class1 MyAccount;
    void Start()
    {
        MyAccount = new Z1_class1("Тагир", 100);
        Debug.Log(MyAccount.GetInfo());
        MyAccount.Deposit(50);
        Debug.Log("После пополнения: " + MyAccount.Balance);
        MyAccount.Withdraw(30);
        Debug.Log("После снятия: " + MyAccount.Balance);
    }
}