using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Common
{
    public interface ILog
    {
        public void Error(string message);
        public void Success(string message);
        public void Message(string message);
    }
}
