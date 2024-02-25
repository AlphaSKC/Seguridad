using ApplicationCore.Commands.Log;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.EventHandlers.Log
{
    public class CreateLogHandler : IRequestHandler<CreateLogCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateLogHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLogCommand command, CancellationToken cancellationToken)
        {
            var l = new CreateLogCommand();
            l.ip = command.ip;
            l.nombreFuncion = command.nombreFuncion;
            l.fecha = command.fecha;
            l.datos = command.datos;
            l.response = command.response;

            var lo = _mapper.Map<Domain.Entities.Log>(l);
            await _context.AddAsync(lo);
            await _context.SaveChangesAsync();

            return new Response<int>(lo.id, "Registro Creado");
        }

    }
}
