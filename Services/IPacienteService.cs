using CitasMedicas.DTOs;
using dotnet5.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IPacienteService
    {

        public Task<ActionResult<IEnumerable<PacienteDTOResponse>>> GetAll();

        public Task<PacienteDTOResponse> Get(int id);



        public Task<int> Put(int id, PacienteDTOPut usuarioDTO);


        public Task<PacienteDTOResponse> Post(PacienteDTOPost pacienteDTO);

        public Task<PacienteDTOResponse> Login(LoginDTO loginForm);

        public Task<Boolean> Delete(int id);

    }
}
