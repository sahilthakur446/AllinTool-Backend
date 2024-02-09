using System;
using System.Collections.Generic;

namespace AllinTool.Data.Models;

public partial class BankDetail
{
    public string? Bank { get; set; }

    public string Ifsc { get; set; } = null!;

    public string? Branch { get; set; }

    public string? Centre { get; set; }

    public string? District { get; set; }

    public string? State { get; set; }

    public string? Address { get; set; }

    public string? Imps { get; set; }

    public string? City { get; set; }
}
