﻿namespace MatrizPlanificacion.ResponseModels
{
    public class procesoFiltro
    {
        public string IDproceso { get; set; }
        public decimal? Avance { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Etapa { get; set; }
        public string ProcesoContra { get; set; }
        public string MesPlanificado { get; set; }
    }
}
