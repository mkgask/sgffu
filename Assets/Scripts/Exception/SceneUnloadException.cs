namespace sgffu.Exception {

    [System.Serializable]
    public class SceneUnloadException : System.Exception
    {
        public SceneUnloadException() { }
        public SceneUnloadException(string message) : base(message) { }
        public SceneUnloadException(string message, System.Exception inner) : base(message, inner) { }
        protected SceneUnloadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
