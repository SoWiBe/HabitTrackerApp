using System;
using HabitTrackerApp.Models.Core;

namespace HabitTrackerApp.Models;

public class DayHabit : IEntityBase
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public Day Day { get; set; }
    public Habit Habit { get; set; }
    public bool IsComplete { get; set; } = false;
}