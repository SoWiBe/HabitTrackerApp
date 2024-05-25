using System;
using System.Collections;
using System.Collections.Generic;
using HabitTrackerApp.Models.Core;

namespace HabitTrackerApp.Models;

public class Day : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Number { get; set; }
    public IEnumerable<Habit>? Habits { get; set; }
}