
namespace sgffu.Exception {

    [System.Serializable]
    public class Texture2DdontLoadException : System.Exception
    {
        public Texture2DdontLoadException() { }
        public Texture2DdontLoadException(string message) : base(message) { }
        public Texture2DdontLoadException(string message, System.Exception inner) : base(message, inner) { }
        protected Texture2DdontLoadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}