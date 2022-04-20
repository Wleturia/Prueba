using System;

namespace Prueba.Common
{
    public interface ILog
    {
        public DateTime Error(string message);
        public DateTime Success(string message);
        public DateTime Message(string message);
    }
}
