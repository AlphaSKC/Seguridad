using ApplicationCore.Commands.Categoria;
using ApplicationCore.Wrappers;
using Infraestructure.Persistence;
using AutoMapper;
using MediatR;

namespace Infraestructure.EnventHandlers.Categoria
{
    public class CreateCategoriaHandler : IRequestHandler<CreateCategoriaCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoriaHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Response<int>> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var u = new CreateCategoriaCommand();
            u.Nombre = request.Nombre;

            var us = _mapper.Map<Domain.Entities.Categoria>(u);
            await _context.categorias.AddAsync(us);
            await _context.SaveChangesAsync();
            return new Response<int>(us.pkCategoria, "Registro Creado");

        }
    }
}