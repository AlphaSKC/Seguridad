using ApplicationCore.DTOs.Log;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappers
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogDto, Log>()
                .ForMember(x => x.id, y => y.Ignore());
        }
    }
}
