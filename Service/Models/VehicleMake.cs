using System;
using System.Collections.Generic;

namespace Service.Models;

public partial class VehicleMake
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Abrv { get; set; } = null!;

    public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
}
