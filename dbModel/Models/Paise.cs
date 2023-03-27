using System;
using System.Collections.Generic;

namespace dbModel.Models;

public partial class Paise
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Acronimo { get; set; } = null!;
}
