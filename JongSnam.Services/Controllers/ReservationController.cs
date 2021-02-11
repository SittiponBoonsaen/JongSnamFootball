﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JongSnamFootball.Entities.Dtos;
using JongSnamFootball.Entities.Request;
using JongSnamFootball.Interfaces.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JongSnam.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationManager _reservationManager;
        public ReservationController(IReservationManager reservationManager)
        {
            _reservationManager = reservationManager;
        }

        [HttpGet("{storeId}")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(BasePagingDto<ReservationDto>))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BasePagingDto<FieldDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemsDetailDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> GetYourReservation(int storeId, int ownerId, int currentPage, int pageSize)
        {
            return Ok(await _reservationManager.GetYourReservation(storeId, ownerId, currentPage, pageSize));
        }

        [HttpGet("Search")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(BasePagingDto<FieldDto>))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BasePagingDto<FieldDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemsDetailDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> GetAll(int storeId, int ownerId, [FromQuery] SearchReservationRequest request, int currentPage, int pageSize)
        {
            return Ok(await _reservationManager.GetSearchReservation(storeId, ownerId, request, currentPage, pageSize));
        }

        [HttpGet("{Id}/Detail")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(BasePagingDto<ReservationDto>))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BasePagingDto<FieldDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemsDetailDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> GetShowDetailYourReservation(int Id)
        {
            return Ok(await _reservationManager.GetShowDetailYourReservation(Id));
        }

        [HttpPut("ApprovalStatus")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> UpdateApprovalStatus(int id , [FromBody] ReservationApprovalRequest request)
        {
            var result = await _reservationManager.UpdateApprovalStatus(id, request);
            return Ok(result);
        }
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> CreateReservation([FromBody] ReservationRequest request)
        {
            var result = await _reservationManager.CreateReservation(request);
            return Ok(result);
        }

        [HttpPut("{id}/DeleteReservation")]
        [Consumes("application/json")]
        [Produces("application/json", Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(bool))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemsDetailDto))]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var result = await _reservationManager.DeleteReservation(id);
            return Ok(result);
        }
    }
    
}