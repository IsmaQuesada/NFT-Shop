﻿using Proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Infraestructure.Repository.Interfaces;

public interface IRepositoryTarjeta
{
    Task<ICollection<Tarjeta>> FindByDescriptionAsync(string description);
    Task<ICollection<Tarjeta>> ListAsync();
    Task<Tarjeta> FindByIdAsync(int id);
    Task<int> AddAsync(Tarjeta entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, Tarjeta entity);

}
