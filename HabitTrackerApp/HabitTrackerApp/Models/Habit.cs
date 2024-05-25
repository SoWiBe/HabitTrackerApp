using System;
using HabitTrackerApp.Models.Core;

namespace HabitTrackerApp.Models;

public class Habit : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Title { get; set; }
    public int CountDays { get; set; }
}