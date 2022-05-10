using AutoMapper;
using HousingAPI.Dtos;
using HousingAPI.Interfaces;
using HousingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnishingTypeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FurnishingTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var furninshingTypes = await unitOfWork.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furninshingTypeDTO = mapper.Map<IEnumerable<FurnishingTypeDto>>(furninshingTypes);
            return Ok(furninshingTypeDTO);
        }
    }
}
