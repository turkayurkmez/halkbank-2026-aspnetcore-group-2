namespace Demo.LifeTime.Services
{
    public interface IGuidGenerator
    {
        Guid Id { get; }
    }

    public interface ISingleton : IGuidGenerator { }
    public interface IScoped: IGuidGenerator { }
    public interface ITransient : IGuidGenerator { }

    public class Singleton : ISingleton
    {
        public Guid Id { get; }
        public Singleton()
        {
            Id = Guid.NewGuid();

        }
    }

    public class Scoped : IScoped
    {
        public Guid Id { get; }
        public Scoped()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Transient : ITransient
    {
        public Guid Id { get; }
        public Transient()
        {
            Id = Guid.NewGuid();
        }
    }

    public class POCService
    {
        public POCService(ISingleton singleton, IScoped scoped, ITransient transient)
        {
            Singleton = singleton;
            Scoped = scoped;
            Transient = transient;
        }

        public ISingleton Singleton { get; set; }
        public IScoped Scoped { get; set; }
        public ITransient Transient { get; set; }
    }
}
