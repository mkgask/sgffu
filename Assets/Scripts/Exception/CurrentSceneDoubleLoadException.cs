namespace sgffu.Exception {

    [System.Serializable]
    public class CurrentSceneDoubleLoadException : System.Exception
    {
        public CurrentSceneDoubleLoadException() { }
        public CurrentSceneDoubleLoadException(string message) : base(message) { }
        public CurrentSceneDoubleLoadException(string message, System.Exception inner) : base(message, inner) { }
        protected CurrentSceneDoubleLoadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}