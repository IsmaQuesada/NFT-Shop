﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Proyecto.Application.Config;
using Proyecto.Application.DTOs;
using Proyecto.Infraestructure.Models;
using Proyecto.Infraestructure.Repository.Interfaces;
using Proyecto.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Application.Services.Interfaces;

namespace Proyecto.Application.Services.Implementations;

public class ServiceUsuario : IServiceUsuario
{
    private readonly IRepositoryUsuario _repository;
    private readonly IMapper _mapper;
    private readonly IOptions<AppConfig> _options;

    public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, IOptions<AppConfig> options)
    {
        _repository = repository;
        _mapper = mapper;
        _options = options;
    }
    public async Task<string> AddAsync(UsuarioDTO dto)
    {
        // Read secret
        string secret = _options.Value.Crypto.Secret;
        //  Get Encrypted password
        string passwordEncrypted = Cryptography.Encrypt(dto.Password!, secret);
        // Set Encrypted password to dto
        dto.Password = passwordEncrypted;
        var objectMapped = _mapper.Map<Usuario>(dto);
        // Return
        return await _repository.AddAsync(objectMapped);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }


    public async Task<ICollection<UsuarioDTO>> FindByNameAsync(string description)
    {
        var list = await _repository.FindByNameAsync(description);
        var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
        return collection;
    }

    public async Task<UsuarioDTO> FindByIdAsync(string id)
    {
        var @object = await _repository.FindByIdAsync(id);
        var objectMapped = _mapper.Map<UsuarioDTO>(@object);
        return objectMapped;
    }


    public async Task<ICollection<UsuarioDTO>> ListAsync()
    {
        // Get data from Repository
        var list = await _repository.ListAsync();
        // Map List<*> to ICollection<*>
        var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
        // Return Data
        return collection;
    }

    public async Task<UsuarioDTO> LoginAsync(string id, string password)
    {
        UsuarioDTO usuarioDTO = null!;

        // Read secret
        string secret = _options.Value.Crypto.Secret;
        //  Get Encrypted password
        string passwordEncrypted = Cryptography.Encrypt(password, secret);

        var @object = await _repository.LoginAsync(id, passwordEncrypted);

        if (@object != null)
        {
            usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
        }

        return usuarioDTO;
    }

    public async Task UpdateAsync(string id, UsuarioDTO dto)
    {
        string secret = _options.Value.Crypto.Secret;
        string passwordEncrypted = Cryptography.Encrypt(dto.Password!, secret);
        dto.Password = passwordEncrypted;
        var objectMapped = _mapper.Map<Usuario>(dto);

        await _repository.UpdateAsync(id, objectMapped);
    }
}