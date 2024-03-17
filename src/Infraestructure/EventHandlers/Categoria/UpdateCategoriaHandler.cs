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
    public class UpdateCategoriaHandler : IRequestHandler<UpdateCategoriaCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _service;

        public UpdateCategoriaHandler(ApplicationDbContext context, IMapper mapper, IDashboardService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response<int>> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var c = await _context.categorias.FindAsync(request.Id);
                c.Nombre = request.Nombre;
                c.Costo = request.Costo;
                c.Estatus = request.Estatus;

                await _context.SaveChangesAsync();

                return new Response<int>(c.pkCategoria, "Categoría actualizada exitosamente");
            }
            catch (Exception ex)
            {
                var log = new LogDto();
                log.datos = JsonSerializer.Serialize(request);
                log.fecha = DateTime.Now;
                log.nombreFuncion = "UpdateCategoria()";
                log.response = ex.Message;
                await _service.CreateLog(log);
                throw;
            }
        }
    }

}
