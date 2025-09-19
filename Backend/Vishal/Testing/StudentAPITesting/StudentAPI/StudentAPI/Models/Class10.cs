using System;
using System.Collections.Generic;

namespace StudentAPI.Models;

public partial class Class10
{
    public int AdmissionNum { get; set; }

    public string Name { get; set; } = null!;

    public string RollNumber { get; set; } = null!;

    public string? Section { get; set; }
}
