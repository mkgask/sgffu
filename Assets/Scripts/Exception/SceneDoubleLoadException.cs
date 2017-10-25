namespace OwrBase.Exception {

    [System.Serializable]
    public class SceneDoubleLoadException : System.Exception
    {
        public SceneDoubleLoadException() { }
        public SceneDoubleLoadException(string message) : base(message) { }
        public SceneDoubleLoadException(string message, System.Exception inner) : base(message, inner) { }
        protected SceneDoubleLoadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
