﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.DTOs
{
    public class DiagnosticoDTOPost
    {
        public string ValoracionEspecialista { get; set; }
        public string Enfermedad { get; set; }
        public int CitaId { get; set; }
    }
}
