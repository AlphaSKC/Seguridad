using ApplicationCore.DTOs.Categoria;
using ApplicationCore.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands.Categoria
{
    public class CreateCategoriaCommand : CategoriaDto, IRequest<Response<int>>
    {
        
    }
}
