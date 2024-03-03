using ApplicationCore.Commands.Categoria;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using AutoMapper;
using MediatR;
using ApplicationCore.Interfaces;
using System.Text.Json;
using ApplicationCore.DTOs.Log;

namespace Infraestructure.EnventHandlers.Categoria
{
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _service;

        public CreateCategoriaHandler(ApplicationDbContext context, IMapper mapper, IDashboardService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;

        }

        public async Task<Response<int>> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var c = new CreateCategoriaCommand();
            c.Nombre = request.Nombre;

            var json = JsonSerializer.Serialize(c);
            var log = new LogDto();
            log.datos =json;
            log.fecha = DateTime.Now;
            log.response = "200";
            log.nombreFuncion = "CreateCategoria()";
            await _service.CreateLog(log);

            var ca = _mapper.Map<Domain.Entities.Categoria>(c);
            await _context.categorias.AddAsync(ca);
            await _context.SaveChangesAsync();
            return new Response<int>(ca.pkCategoria, "Registro Creado");

        }
    }
}