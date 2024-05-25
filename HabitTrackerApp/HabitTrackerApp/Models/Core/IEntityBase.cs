using System;

namespace HabitTrackerApp.Models.Core;

public interface IEntityBase
{
    public Guid Id { get; set; } 
}