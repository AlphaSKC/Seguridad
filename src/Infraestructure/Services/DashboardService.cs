using ApplicationCore.DTOs.Log;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        
        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Response<object>> GetData()
        {
            object list = new object();
            list = await _dbContext.categorias.ToListAsync();
            return new Response<object>(list);
        }

        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress iPAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            string ip = iPAddress?.ToString() ?? "No se pudó obtener la ip";
            return new Response<string>(ip);
        }

        public async Task<Response<int>> CreateLog(LogDto request)
        {
            var ipAddress = await GetIp();
            var ip = ipAddress.Message.ToString();
            var l = new LogDto();
            l.ip = ip;
            l.nombreFuncion = request.nombreFuncion;
            l.fecha = request.fecha;
            l.datos = request.datos;
            l.response = request.response;

            var lo = _mapper.Map<Domain.Entities.Log>(l);
            await _dbContext.AddAsync(lo);
            await _dbContext.SaveChangesAsync();

            return new Response<int>(lo.id, "Registro Creado");
        }

    }
}