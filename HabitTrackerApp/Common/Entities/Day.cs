using Common.Entities.Core;

namespace Common.Entities;

public class Day : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public IEnumerable<Habit>? Habits { get; set; }
}