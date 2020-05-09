﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Core;
using Vega.Controllers.Resources;
using Vega.Core.Models;

namespace vega.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotosController : Controller
    {
		private readonly IHostingEnvironment host;
		private readonly IVehicleRepository repository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly PhotoSettings photoSettings;
		public PhotosController(IHostingEnvironment host, IVehicleRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
		{
			this.photoSettings = options.Value;
			this.host = host;
			this.repository = repository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		[HttpPost]
		public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
		{
			var vehicle = await repository.GetVechicle(vehicleId, IncluceRelated: false);
			if (vehicle == null)
				return NotFound();

			if (file == null) return BadRequest("Null File");
			if (file.Length == 0) return BadRequest("Empty File");
			if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size Exceeded");
			//if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");

			var uploadsFolderPath =  Path.Combine(host.WebRootPath, "uploads");
			if (!Directory.Exists(uploadsFolderPath))
				Directory.CreateDirectory(uploadsFolderPath);

			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var filePath = Path.Combine(uploadsFolderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			var photo = new Photo { FileName = fileName};
			vehicle.Photos.Add(photo);
			await unitOfWork.CompleteAsync();

			return Ok(mapper.Map<Photo, PhotoResource>(photo));
		}
    }
}