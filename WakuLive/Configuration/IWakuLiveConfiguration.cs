using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Configuration
{
    public interface IWakuLiveConfiguration
    {
        IAccountConfiguration Account { get; }
    }
}
