using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Enums
{
    public enum WalletType
    {
        pay = 1,
        Deposit = 2
    }
    public enum PlanType
    {
        OneMonth = 1,
        ThreeMonth = 3,
        SixMonth = 6,
        TwelveMonth = 12
    }
    public enum Quality
    {
        Q240=240,
        Q360=360,
        Q480=480,
        Q720 =720,
        Q1080 =1080,
        Q4k = 4000,

    }
}
