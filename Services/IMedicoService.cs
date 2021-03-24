using CitasMedicas.DTOs;
using dotnet5.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IMedicoService
    {

        public Task<ActionResult<IEnumerable<MedicoDTOResponse>>> GetAll();

        public Task<MedicoDTOResponse> Get(int id);



        public Task<int> Put(int id, MedicoDTOPut usuarioDTO);


        public Task<MedicoDTOResponse> Login(LoginDTO loginForm);

        public Task<MedicoDTOResponse> Post(MedicoDTOPost pacienteDTO);


        public Task<Boolean> Delete(int id);
    }
}
