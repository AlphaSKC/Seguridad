﻿using ApplicationCore.DTOs.Log;
using ApplicationCore.Wrappers;

namespace ApplicationCore.Interfaces
{
    public interface IDashboardService
    {
        Task<Response<object>> GetData();
        Task<Response<string>> GetIp();
        Task<Response<int>> CreateLog(LogDto response);
    }
}
