using GDirectiva.Cross.Core.Base;

namespace GDirectiva.Cross.Core.Exception
{
    public class ApplicationLayerException<T> : GenericException<T>
        where T : class
    {
        public ApplicationLayerException(string message, System.Exception e) : base(message, e) { }
        public ApplicationLayerException(System.Exception e) : base(e) { }
    }
}
