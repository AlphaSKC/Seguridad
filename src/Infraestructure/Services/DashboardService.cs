using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        
        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
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


    }
}