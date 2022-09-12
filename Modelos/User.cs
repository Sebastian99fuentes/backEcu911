﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MatrizPlanificacion.Modelos
{
    public partial class User : IdentityUser
    {
        public string AreaId { get; set; }
        [ForeignKey("AreaId")]
        public PlantaUnidadArea Area { get; set; }
    }
}
