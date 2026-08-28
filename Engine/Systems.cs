namespace Engine
{
    public class ECSSystem
    {
        public Scene scene;
        public virtual void Update(float dt) { }
        public virtual void Draw(float dt) { }
        public virtual void OnAdded() { }
        public virtual void OnRemoved() { }
    }
}
