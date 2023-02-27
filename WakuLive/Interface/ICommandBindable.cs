using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Controller;

namespace WakuLive.Interface
{
    public interface ICommandBindable
    {
        void Bind(ServiceProvider provider);
    }
}
