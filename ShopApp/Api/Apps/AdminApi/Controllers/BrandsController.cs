using Api.Apps.AdminApi.Dtos.BrandDtos;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Apps.AdminApi.Controllers
{
	[ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("admin/api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IBrandRepository _brandRepository;

		public BrandsController(IMapper mapper,IBrandRepository brandRepository) 
		{
			_mapper = mapper;
			_brandRepository = brandRepository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			var data = await _brandRepository.GetAllAsync();
			List<BrandGetAllItemDto> items = _mapper.Map<List<BrandGetAllItemDto>>(data);
			return Ok(items);
		}

		[HttpPost("")]
		public async  Task<IActionResult> Create(BrandDto brandDto)
		{
			if(await _brandRepository.IsExistAsync(x=>x.Name== brandDto.Name))
			{
				ModelState.AddModelError("Name", "Brand Already Exist");
				return BadRequest(ModelState);
			}
			Brand brand= _mapper.Map<Brand>(brandDto);
			await _brandRepository.AddAsync(brand);
			await _brandRepository.SaveChangesAsync();
			return Ok(brandDto);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var data = await _brandRepository.GetAsync(x => x.Id == id,"Products");
			if(data==null) { NotFound(); }
			BrandGetDto dto= _mapper.Map<BrandGetDto>(data);
			return Ok(dto);
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> Update(int id,BrandDto brandDto)
		{
			var existBrand= await _brandRepository.GetAsync(x => x.Id == id);
			if (existBrand==null) { NotFound(); }
			if(existBrand.Name!= brandDto.Name &&  await _brandRepository.IsExistAsync(x => x.Name == brandDto.Name))
			{
				ModelState.AddModelError("Name", "Brand Already Exist");
				return BadRequest(ModelState);
			}
			existBrand.Name= brandDto.Name;
			await _brandRepository.SaveChangesAsync();
			return Ok(existBrand);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var data =await _brandRepository.GetAsync(x => x.Id == id);
			if (data == null) { NotFound(); }
			_brandRepository.Remove(data);
			await _brandRepository.SaveChangesAsync();
			return NoContent();
		}
	}
}
