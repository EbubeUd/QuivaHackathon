using MaticNetwork.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Matic
{
    public class MaticClient : MonoBehaviour
    {

        public async Task<System.Numerics.BigInteger> BalanceOfERC20()
        {
            Debug.Log("Returning");
            var matic = Settings.GetMatic();

            return await matic.BalanceOfERC20(Settings.FROM_ADDRESS, Settings.ROPSTEN_TEST_TOKEN, true);
        }
    }
}
