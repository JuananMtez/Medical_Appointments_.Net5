using CitasMedicas.DTOs;
using dotnet5.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IDiagnosticoService
    {
        public Task<ActionResult<IEnumerable<DiagnosticoDTOResponse>>> GetAll();

        public Task<DiagnosticoDTOResponse> Get(int id);



        public Task<int> Put(int id, DiagnosticoDTOPut usuarioDTO);


        public Task<DiagnosticoDTOResponse> Post(DiagnosticoDTOPost pacienteDTO);


        public Task<Boolean> Delete(int id);

    }
}
