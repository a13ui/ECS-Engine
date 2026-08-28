namespace Engine
{
    public class Component
    {
        public Entity entitiy;
    }
    public class Entity
    {
        public Scene scene;
        public bool inScene;

        private HashSet<Tag> tags;
        private HashSet<Component> components;

        public Entity()
        {
            components = new();
            tags = new();
        }

        public bool Add<T>() where T : Component, new()
        {
            if (Has<T>())
                return false;

            T derivative = new T();
            derivative.entitiy = this;
            components.Add(derivative);
            return true;
        }

        public bool Add(Component component)
        {
            if (Has(component))
                return false;
            component.entitiy = this;
            components.Add(component);
            return true;
        }

        public bool Remove<T>() where T : Component
        {
            if (!Has<T>())
                return false;
            Component component = components.Where(a => a.GetType() == typeof(T)).First();
            components.Remove(component);
            return true;
        }
        public bool Remove(Component element)
        {
            if (!Has(element))
                return false;
            components.Remove(element);
            return true;
        }

        public bool Has<T>() where T : Component
        {
            return components.Where(a => a.GetType() == typeof(T)).Any();
        }
        public bool Has(Type type)
        {
            return components.Where(a => a.GetType() == type).Any();
        }
        public bool Has(Component component)
        {
            return components.Where(a => a.GetType() == component.GetType()).Any();
        }

        public T Get<T>() where T : Component
        {
            return (T)components.Where(a => a.GetType() == typeof(T)).FirstOrDefault();
        }

        public bool TryGet<T>(out T component) where T : Component
        {
            component = (T)components.Where(a => a.GetType() == typeof(T)).FirstOrDefault();
            return component != null;
        }

        public bool Add(Tag tag)
        {
            if (tags.Contains(tag)) 
                return false;
            tags.Add(tag);
            return true;
        }

        public bool Remove(Tag tag)
        {
            if (!tags.Contains(tag)) 
                return false;
            tags.Remove(tag);
            return true;
        }
        public bool Has(Tag tag)
        {
            return tags.Contains(tag);
        }
    }
}
