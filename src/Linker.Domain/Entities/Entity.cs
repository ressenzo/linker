namespace Linker.Domain.Entities;

public abstract class Entity
{
    private readonly List<string> _errors;

    public Entity()
    {
        Id = Guid.NewGuid();
        _errors = [];
    }

    public Entity(Guid id)
    {
        Id = id;
        _errors = [];
    }

    public Guid Id { get; }

    public abstract void Validate();

    public bool IsValid => _errors.Count == 0;

    public IEnumerable<string> Errors => _errors;

    protected void AddError(string error)
    {
        _errors.Add(error);
    }
}
