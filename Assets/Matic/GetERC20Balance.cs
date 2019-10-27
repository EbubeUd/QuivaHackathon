using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Numerics;

public class GetERC20Balance : MonoBehaviour
{
    public Text tokenBalanceText;
    public static GetERC20Balance instance;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        UpdateBalance();
        instance = this;
    }



    public async void UpdateBalance() {
        var balance = await BalanceOfERC20();
        Debug.Log(balance);
        BigInteger division = BigInteger.Divide(balance, new BigInteger(1000000000000000000));
        tokenBalanceText.text = division.ToString() + "\n TEST";
    }

    public static async Task<BigInteger> BalanceOfERC20()
    {
        var matic = Settings.GetMatic();
        return await matic.BalanceOfERC20(Settings.FROM_ADDRESS, Settings.ROPSTEN_TEST_TOKEN, true);
    }
}
