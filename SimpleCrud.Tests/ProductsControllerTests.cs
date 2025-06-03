using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Controllers;
using SimpleCrud.Data;
using SimpleCrud.Models;
using Xunit;
using System.Threading.Tasks;

namespace SimpleCrud.Tests
{
    public class ProductsControllerTests
    {
        private ApplicationDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb_" + System.Guid.NewGuid())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var context = GetInMemoryContext();
            context.Product.Add(new Product { Name = "Test", Price = 10 });
            context.SaveChanges();

            var controller = new ProductsController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<Product>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = new ProductsController(GetInMemoryContext());

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_AddsProductAndRedirects()
        {
            var context = GetInMemoryContext();
            var controller = new ProductsController(context);

            var product = new Product { Name = "New", Price = 12 };

            var result = await controller.Create(product);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Single(context.Product.ToListAsync().Result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenProductIdNotExists()
        {
            var controller = new ProductsController(GetInMemoryContext());

            var result = await controller.Edit(1, new Product { Id = 2, Name = "Wrong Id" });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesAndRedirects()
        {
            var context = GetInMemoryContext();
            var product = new Product { Name = "ToDelete", Price = 5 };
            context.Product.Add(product);
            context.SaveChanges();

            var controller = new ProductsController(context);

            var result = await controller.DeleteConfirmed(product.Id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Empty(context.Product.ToListAsync().Result);
        }
    }
}
