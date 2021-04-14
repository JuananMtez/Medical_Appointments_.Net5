using CitasMedicas.DTOs;
using dotnet5.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface ICitaService
    {

        public Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetAll();

        public Task<CitaDTOResponse> Get(int id);

        public Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetByPaciente(int idUsuario);

        public Task<ActionResult<IEnumerable<CitaDTOResponse>>> GetByMedico(int idUsuario);


        public Task<int> Put(int id, CitaDTOPut usuarioDTO);


        public Task<CitaDTOResponse> Post(CitaDTOPost pacienteDTO);


        public Task<Boolean> Delete(int id);
    }
}
