using ApplicationCore.Commands.Categoria;
using ApplicationCore.DTOs.Log;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructure.EventHandlers.Categoria
{
    public class DeleteCategoriaHandler : IRequestHandler<DeleteCategoriaCommand,Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _service;

        public DeleteCategoriaHandler(ApplicationDbContext context, IMapper mapper, IDashboardService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response<int>> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var categoria = await _context.categorias.FindAsync(request.Id);
                categoria.Estatus = false;
                await _context.SaveChangesAsync();
                return new Response<int>(request.Id, "Categoría eliminada correctamente");
            }
            catch (Exception ex)
            {
                var log = new LogDto();
                log.datos = JsonSerializer.Serialize(request);
                log.fecha = DateTime.Now;
                log.nombreFuncion = "DeleteCategoria()";
                log.response = ex.Message;
                await _service.CreateLog(log);
                throw;
            }

        }
    }
}
