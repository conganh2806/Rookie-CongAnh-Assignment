using System.Net;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Common;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductsController : BaseController<Product>
    {
        private readonly IProductService _productService;
        private readonly IMediaFileService _mediaFileService;

        public ProductsController(IProductService productService, 
                                    ILogger<Product> logger,
                                    IMediaFileService mediaFileService)
                : base(logger)
        {
            _productService = productService;
            _mediaFileService = mediaFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Success(products, "Get all products successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) 
            {
                return Error("Product not found", HttpStatusCode.NotFound);
            }

            return Success(product, "Get product successfully.");
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(string categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);

            return Success(products, "Get products by category successfully.");
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var products = await _productService.GetBySlugAsync(slug);

            return Success(products, "Get products by slug successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            if(!ModelState.IsValid) 
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }   

            var product = await _productService.CreateAsync(request);

            if (product == null) 
            {
                return Error("Product not found", HttpStatusCode.NotFound);
            }

            return Success(product, "Create product successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductUpdateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return Error("Error validate", HttpStatusCode.BadRequest);
            }   

            var product = await _productService.UpdateAsync(id, request);

            if (product == null) 
            {
                return Error("Product not found", HttpStatusCode.NotFound);
            }

            return Success(product, "Update product successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteAsync(id);
            
            return Success<Product?>(null, "Delete product successfully.");
        }

        [HttpPost("upload-media")]
        public async Task<IActionResult> UploadProductMedia([FromForm] UploadMediaFileRequest request)
        {
            var file = request.File;

            if (file == null || file.Length == 0)
            {
                throw new NotFoundException("File is empty");
            }

            var createRequest = new CreateMediaFileRequest
            {
                FileName = Path.GetFileNameWithoutExtension(file.FileName),
                FileExtension = Path.GetExtension(file.FileName),
                ContentType = file.ContentType,
                ObjectType = typeof(Product).Name.ToLower(),
                ObjectId = request.ProductId,
                FileStream = file.OpenReadStream()
            };

            var fileUrl = await _mediaFileService.UploadFileAsync(createRequest);

            var response = new UploadMediaFileResponse
            {
                FileName = createRequest.FileName,
                FileExtension = createRequest.FileExtension,
                ContentType = createRequest.ContentType,
                FileUrl = fileUrl
            };

            return Success(response, "Upload media for product success !");
        }
    }
}
