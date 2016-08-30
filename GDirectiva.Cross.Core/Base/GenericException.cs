using log4net;

namespace GDirectiva.Cross.Core.Base
{
    public abstract class GenericException<T> : System.Exception, IGenericException
        where T : class
    {
        /// <summary>
        /// Bitácora
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(T));

        /// <summary>
        /// Exception base
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <param name="e">Excepción</param>
        protected GenericException(string message, System.Exception e)
            : base(message, e)
        {

        }

        protected GenericException(System.Exception e)
            : base("Error : " + typeof(T).Name + " , see more detail.(view innerException)", e)
        {

        }
    }
}
