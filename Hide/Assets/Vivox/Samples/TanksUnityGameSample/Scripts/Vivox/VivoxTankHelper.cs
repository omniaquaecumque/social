using System.Linq;
using UnityEngine;

public class VivoxTankHelper : MonoBehaviour
{
    public static TankSetup FindTankSetupInSceneByAccountId(string AccountID)
    {
		var tankSetups = FindObjectsOfType<TankSetup>();
		return tankSetups?.FirstOrDefault(ts => ts.m_AccountID == AccountID);
    }
}
