namespace cs_pokedex.Utilities
{
    public class Observer
    {
        protected Action? _listeners;

        public void AddStateChangeListeners(Action? listener)
        {
            if (listener is not null)
                _listeners += listener;
        }

        public void RemoveStateChangeListeners(Action? listener)
        {
            if (listener is not null) 
                _listeners -= listener;
        }

        public void BroadcastStateChange()
        {
            _listeners?.Invoke();
        }
    }
}
