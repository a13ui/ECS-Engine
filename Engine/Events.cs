namespace Engine
{
    public interface IEvent { }
    public class EventBus
    {
        public Dictionary<Type, List<Action<IEvent>>> events = new();
        public List<IEvent> ToInvoke = new();
        public void Invoke(IEvent evt)
        {
            ToInvoke.Add(evt);
        }
        public void Subscribe<T>(Action<T> action) where T : IEvent
        {
            if (!events.ContainsKey(typeof(T)))
                events[typeof(T)] = new();

            events[typeof(T)].Add(e => action((T)e));
        }
        public void Publish(IEvent evt)
        {
            ToInvoke.Add(evt);
        }
        public void Update()
        {
            for (int i = 0; i < ToInvoke.Count; i++)
            {
                List<Action<IEvent>> actions = events[ToInvoke[i].GetType()];
                for (int j = 0; j < actions.Count; j++)
                {
                    actions[j].Invoke(ToInvoke[i]);
                }
            }
            ToInvoke.Clear();
        }
    }
}