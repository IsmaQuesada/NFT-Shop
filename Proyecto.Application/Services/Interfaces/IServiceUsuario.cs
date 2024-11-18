﻿using Proyecto.Application.DTOs;
using Proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.Services.Interfaces
{
    public interface IServiceUsuario
    {
        Task<ICollection<UsuarioDTO>> FindByNameAsync(string name);
        Task<ICollection<UsuarioDTO>> ListAsync();
        Task<UsuarioDTO> FindByIdAsync(string id);
        Task<UsuarioDTO> LoginAsync(string id, string password);
        Task<string> AddAsync(UsuarioDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, UsuarioDTO entity);
    }
}
