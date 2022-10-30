using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Utils.Events
{
    public interface IEventHandler<T>
    {
        public void Handle(T e);
    }
}
