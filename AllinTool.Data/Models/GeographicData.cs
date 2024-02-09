using System;
using System.Collections.Generic;

namespace AllinTool.Data.Models;

  public  class GeographicData
{
    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? County { get; set; }

    public string? Community { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}
