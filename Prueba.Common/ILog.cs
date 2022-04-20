namespace Prueba.Common
{
    public interface ILog
    {
        public void Error(string message);
        public void Success(string message);
        public void Message(string message);
    }
}
