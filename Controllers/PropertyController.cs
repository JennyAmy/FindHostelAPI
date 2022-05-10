using AutoMapper;
using HousingAPI.Dtos.PropertyDto;
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
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [HttpGet("list/{sellRent}")] 
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }

        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await unitOfWork.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDTO = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDTO);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(AddPropertyDto addPropertyDto)
        {
            var property = mapper.Map<Property>(addPropertyDto);
            var userId = GetUserid();
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            unitOfWork.PropertyRepository.AddProperty(property);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPost("add/photo/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file, int propId)
        {
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
            var property = await unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if(property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }

            property.Photos.Add(photo);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        //
        [HttpPost("set-primary-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId, string photoPublicId)
        {
            var userId = GetUserid();

            var property = await unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property == null)
                return BadRequest("No such property or photo exists");

            //if (property.PostedBy != userId)
            //    return BadRequest("You are not authorized to change the photo");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("Photo does not exist");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo");

            var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if (currentPrimary != null) currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;

            if(await unitOfWork.SaveAsync()) return NoContent();

            return BadRequest("Some error occured, failed to set primary photo");

        }


        [HttpDelete("delete-photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int propId, string photoPublicId)
        {
            var userId = GetUserid();

            var property = await unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            if (property == null)
                return BadRequest("No such property or photo exists");

            //if (property.PostedBy != userId)
            //    return BadRequest("You are not authorized to delete this photo");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("Photo does not exist");

            if (photo.IsPrimary)
                return BadRequest("You cannot delete the primary photo");

            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if(result.Error != null) 
                return BadRequest(result.Error.Message);

            property.Photos.Remove(photo);

            if (await unitOfWork.SaveAsync()) return Ok();

            return BadRequest("Some error occured, failed to delete photo");

        }
    }
}
