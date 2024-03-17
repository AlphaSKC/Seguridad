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
            try
            {
                var c = new CreateCategoriaCommand();
                c.Nombre = request.Nombre;
                c.Costo = request.Costo;
                c.Estatus = request.Estatus;
                var ca = _mapper.Map<Domain.Entities.Categoria>(c);
                await _context.categorias.AddAsync(ca);
                await _context.SaveChangesAsync();
                return new Response<int>(ca.pkCategoria, "Registro Creado");
            }
            catch (Exception ex)
            {
                var log = new LogDto();
                log.datos = JsonSerializer.Serialize(request);
                log.fecha = DateTime.Now;
                log.nombreFuncion = "CreateCategoria()";
                log.response = ex.Message;
                await _service.CreateLog(log);
                throw;
            }
        }
    }
}