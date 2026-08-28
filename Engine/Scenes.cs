namespace Engine
{
    public class Scene
    {
        public SceneManager manager;
        public EventBus events;

        private HashSet<Entity> entities;
        private HashSet<ECSSystem> systems;

        private HashSet<Entity> toRemove;
        private HashSet<Entity> toAdd;

        public Scene()
        {
            events = new EventBus();

            entities = new();
            systems = new();

            toRemove = new();
            toAdd = new();
        }
        public virtual void Initialize() { }

        public void Update(float dt)
        {
            RemoveEntities();
            AddEntities();
            foreach (ECSSystem system in systems)
            {
                system.Update(dt);
            }
            events.Update();
        }

        public void Draw(float dt)
        {
            foreach (ECSSystem system in systems)
            {
                system.Draw(dt);
            }
        }
        public void Clear()
        {
            entities = new();
            systems = new();

            toRemove = new();
            toAdd = new();
        }

        private void RemoveEntities()
        {
            foreach (Entity entitiy in toRemove)
                entities.Remove(entitiy);

            toRemove.Clear();
        }

        private void AddEntities()
        {
            foreach(Entity entitiy in toAdd)
                entities.Add(entitiy);

            toAdd.Clear();
        }
        public IEnumerable<Entity> Query(params object[] requierments)
        {
            List<Type> components = new();
            List<Tag> tags = new();
            foreach (object requier in requierments)
            {
                switch (requier)
                {
                    case Type c:
                        components.Add(c);
                        break;
                    case Tag t:
                        tags.Add(t);
                        break;
                }
            }
            return entities.Where(e => components.All(c => e.Has(c)) && tags.All(t => e.Has(t)));
        }
        public Entity CreateEntity()
        {
            Entity entity = new Entity();
            entity.scene = this;
            entity.inScene = true;
            toAdd.Add(entity);
            return entity;
        }

        public bool RemoveEntity(Entity entity)
        {
            if (!entities.Contains(entity))
                return false;
            toRemove.Add(entity);
            return true;
        }

        public bool AddSystem(ECSSystem system)
        {
            if (HasSystem(system))
                return false;

            system.scene = this;
            systems.Add(system);
            system.OnAdded();
            return true;
        }

        public bool RemoveSystem<T>() where T : ECSSystem
        {
            ECSSystem system = systems.Where(a => a.GetType() == typeof(T)).FirstOrDefault();
            if (system == null)
                return false;

            system.scene = null;
            systems.Remove(system);
            system.OnRemoved();
            return true;
        }
        public bool RemoveSystem(ECSSystem system)
        {
            if (!HasSystem(system))
                return false;
            system.scene = null;
            systems.Remove(system);
            return true;
        }

        public bool HasSystem<T>() where T : ECSSystem
        {
            return systems.Where(a => a.GetType() == typeof(T)).Any();
        }
        public bool HasSystem(ECSSystem system)
        {
            return systems.Where(a => a.GetType() == system.GetType()).Any();
        }
    }

    public class SceneManager
    {
        Dictionary<string, Scene> scenes;
        string? nextScene;
        Scene activeScene;
        public SceneManager()
        {
            activeScene = null;
            scenes = new();
        }
        public bool AddScene(string name, Scene scene)
        {
            if (scenes.ContainsKey(name))
                return false;
            scene.manager = this;
            scenes[name] = scene;
            return true;
        }
        public bool SetScene(string name)
        {
            if (!scenes.ContainsKey(name))            
                return false;

            nextScene = name;
            return true;
        }
        public void Update(float dt)
        {
            if (nextScene != null)
            {
                activeScene?.Clear();
                activeScene = scenes[nextScene];
                activeScene.Initialize();
                nextScene = null;
            }

            activeScene.Update(dt);
        }
        public void Draw(float dt)
        {
            activeScene.Draw(dt);
        }
    }
}
