﻿using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using ApplicationCore.Commands.Categoria;

namespace Host.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly IMediator _mediator;
        public DashboardController(IDashboardService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [Route("getData")]
        [HttpGet]

        public async Task<IActionResult> GetUsuarios()
        {
            var result = await _service.GetData();
            return Ok(result);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> Create(CreateCategoriaCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        //public async Task<IActionResult> GastoPendienteArea()
        //{
        //    var origin = Request.Headers["origin"];
        //    return Ok("test");
        //}


    }
}