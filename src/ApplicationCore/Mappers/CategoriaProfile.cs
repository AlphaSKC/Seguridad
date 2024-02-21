using ApplicationCore.Commands.Categoria;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappers
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaCommand, Categoria>()
                .ForMember(x => x.pkCategoria, y => y.Ignore());
        }
    }
}
