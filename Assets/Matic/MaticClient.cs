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
        public static MaticClient instance;
        public IMatic matic;
        public MaticClient()
        {
            instance = this;
            matic = Settings.GetMatic();
        }

        public async Task<BigInteger> GetERC20Balance()
        {
            return await matic.BalanceOfERC20(Settings.FROM_ADDRESS, Settings.ROPSTEN_TEST_TOKEN, true);
        }
    }
}
