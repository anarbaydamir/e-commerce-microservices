using Catalog.API.Entities;
using Catalog.API.Repository.Inter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/catalog")]
    public class CatalogController:ControllerBase
    {
        private readonly IProductRepository productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Products()
        {
            var productList = await productRepository.getAll();
            return Ok(productList);
        }

        [HttpGet( Name = "GetProduct")]
        [Route("product/{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Product(string id)
        {
            var product = await productRepository.getById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        [Route("products/category/{categoryName}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Products(string categoryName)
        {
            var productList = await productRepository.getByCategoryName(categoryName);
            return Ok(productList);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            await productRepository.create(product);
            return CreatedAtRoute("Product", new { id = product.id }, product);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            return Ok(await productRepository.update(product));
        }

        [HttpPost]
        [Route("/delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await productRepository.delete(id));
        }
    }
}
