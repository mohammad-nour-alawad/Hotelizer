using System;
using Xunit;
using Moq;
using Hotelizer.Controllers;
using DBConnection.Models;
using DBConnection.Repos;
using DBConnection.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace testing
{
    public class CategoryControllerTests
    {
        #region snippet_Index_ReturnsAViewResult_WithAListofCategories
        [Fact]
        public void Index_ReturnsAViewResult_WithAListofCategories()
        {
            // Arrange
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetAll(null, null, ""))
                .Returns(GetTestCategories());
            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);
            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Catergory>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        #endregion

        #region snippet_GetTestCategories
        private List<Catergory> GetTestCategories()
        {
            var categories = new List<Catergory>();
            categories.Add(new Catergory()
            {
                Id = 1,
                Name = "Test 1",
                BasePrice = 2500,
                ImageUrl = "/images/categories/Double Room.jpg",

            });
            categories.Add(new Catergory()
            {
                Id = 2,
                Name = "Test 2",
                BasePrice = 2500,
                ImageUrl = "/images/categories/Single Room.jpg"
            });
            return categories;
        }
        #endregion

        #region snippet_GetTestServices
        private List<Service> GetTestServices()
        {
            var services = new List<Service>();
            services.Add(new Service()
            {
                Id = 1,
                Name = "Test 1",
                ImageUrl = "/images/services/Breakfast.jpg"

            });
            services.Add(new Service()
            {
                Id = 2,
                Name = "Test 2",
                ImageUrl = "/images/services/Breakfast.jpg"
            });
            return services;
        }
        #endregion

        #region snippet_Details_ReturnsARedirectToIndexCategory_WhenIdIsNull
        [Fact]
        public void Details_ReturnsARedirectToIndexCategory_WhenIdIsNull()
        {
            // Arrange
            var controller = new CatergoriesController(null, null, null, null);

            // Act
            var result = controller.Details(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Catergories", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        #endregion

        #region  snippet_Details_ReturnsARedirectToIndexCategory_WhenCategpryNotFound
        [Fact]
        public void Details_ReturnsARedirectToIndexCategory_WhenCategpryNotFound()
        {
            // Arrange
            int testCategoryId = -1;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns((Catergory)null);


            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Details(testCategoryId);

            // Assert
            var redirectToActionResult =
              Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Catergories", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        #endregion

        #region  snippet_Details_ReturnsACategory_WhenCategpryFound
        [Fact]
        public void Details_ReturnsACategory_WhenCategoryFound()
        {
            // Arrange
            int testCategoryId = 1;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns(GetTestCategories().FirstOrDefault(
                    s => s.Id == testCategoryId));

            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Details(testCategoryId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Catergory>(
                viewResult.ViewData.Model);

            Assert.Equal("Test 1", model.Name);
            Assert.Equal(testCategoryId, model.Id);
            Assert.Equal(2500, model.BasePrice);
            Assert.Equal("/images/categories/Double Room.jpg", model.ImageUrl);
        }

        #endregion

        #region snippet_Create_ReturnsAViewResult
        [Fact]
        public void Create_ReturnsAViewResult()
        {
            // Arrange
            var controller = new CatergoriesController(null , null, null, null);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);

        }
        #endregion

        #region snippet_Create_ReturnsToSameView_GivenInvalidModel
        [Fact]
        public async Task Create_ReturnsToSameView_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetAll(null, null, ""))
                .Returns(GetTestCategories());
            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);
            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);
            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.Create(new Catergory
            {
                Name = string.Empty
            });

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Catergory>(viewResult.ViewData.Model);
            Assert.Equal(string.Empty, model.Name);
        }
        #endregion

        #region snippet_CreatePost_ReturnsARedirectToIndexCategory_GivenValidModel
        [Fact]
        public async Task CreatePost_ReturnsARedirectToIndexCategory_GivenValidModel()
        {
            // Arrange
            int testCategoryId = 3;
            string name = "Test 3";
            string imageUrl = "/images/categories/Double Room.jpg";
            Catergory newCat = new Catergory { Id = testCategoryId, Name = name, ImageUrl = imageUrl };
            Catergory retCat = new Catergory { Id = testCategoryId, Name = name, ImageUrl = imageUrl };
       
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.Insert(newCat))
                .Returns(retCat);

            List<Catergory> res = GetTestCategories();
            res.Add(newCat);
            mockRepo.Setup(repo => repo.GetAll(null, null, ""))
                .Returns(res);

            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = await controller.Create(newCat);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Catergory> >(okObjectResult.Value);
            Assert.Equal(3, returnValue.Count());
            Assert.Equal("Test 3", returnValue.LastOrDefault().Name);
            Assert.Equal(3, returnValue.LastOrDefault().Id);
            Assert.Equal("/images/categories/Double Room.jpg", returnValue.LastOrDefault().ImageUrl);

        }
        #endregion

        #region snippet_Delete_ReturnsARedirectToIndexCategory_WhenIdIsNull
        [Fact]
        public void Delete_ReturnsARedirectToIndexCategory_WhenIdIsNull()
        {
            // Arrange
            var controller = new CatergoriesController(null, null, null, null);

            // Act
            var result = controller.Delete(id: null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region  snippet_Delete_ReturnsARedirectToIndexCategory_WhenCategpryNotFound
        [Fact]
        public void Delete_ReturnsARedirectToIndexCategory_WhenCategpryNotFound()
        {
            // Arrange
            int testCategoryId = 1000;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns((Catergory)null);


            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Delete(testCategoryId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(testCategoryId, notFoundObjectResult.Value);
        }
        #endregion

        #region  snippet_Delete_ReturnsACategory_WhenCategpryFound
        [Fact]
        public void Delete_ReturnsACategory_WhenCategpryFound()
        {
            // Arrange
            int testCategoryId = 1;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns(GetTestCategories().FirstOrDefault(
                    s => s.Id == testCategoryId));
            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Details(testCategoryId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Catergory>(
                viewResult.ViewData.Model);

            Assert.Equal("Test 1", model.Name);
            Assert.Equal(testCategoryId, model.Id);
            Assert.Equal(2500, model.BasePrice);
            Assert.Equal("/images/categories/Double Room.jpg", model.ImageUrl);
        }

        #endregion

        #region snippet_Edit_ReturnsNotFoundResult_WhenIdIsNull
        [Fact]
        public void Edit_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var controller = new CatergoriesController(null, null, null, null);

            // Act
            var result = controller.Edit(id: null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region snippet_Edit_ReturnsNotFoundResult_WhenCategoryNotFound
        [Fact]
        public void Edit_ReturnsNotFoundResult_WhenCategoryNotFound()
        {
            int testCategoryId = -1;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns((Catergory)null);

            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Edit(testCategoryId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region snippet_Edit_ReturnsACategory_WhenCategoryFound
        [Fact]
        public void Edit_ReturnsACategory_WhenCategoryFound()
        {
            // Arrange
            int testCategoryId = 1;
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(testCategoryId))
                .Returns(GetTestCategories().FirstOrDefault(
                    s => s.Id == testCategoryId));

            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.Edit(testCategoryId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Catergory>(
                viewResult.ViewData.Model);

            Assert.Equal("Test 1", model.Name);
            Assert.Equal(testCategoryId, model.Id);
            Assert.Equal(2500, model.BasePrice);
            Assert.Equal("/images/categories/Double Room.jpg", model.ImageUrl);
        }
        #endregion

        #region snippet_Edit_ReturnSameCategory_WhenModelisInvalid
        [Fact]
        public async Task Edit_ReturnSameCategory_WhenModelisInvalid()
        {
            //Arrange
            var controller = new CatergoriesController(null, null, null, null);
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = await controller.Edit(new Catergory { Name = string.Empty , Id = 1 });

            //Assign
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Catergory>(
                viewResult.ViewData.Model);
            Assert.Equal( 1 , model.Id);
            Assert.Equal(string.Empty , model.Name);
        }
        #endregion

        #region snippet_Edit_ReturnsAOkObject_WhenImageFileUploaded
        [Fact]
        public async Task Edit_ReturnsAOkObject_WhenImageFileUploaded()
        {
            //Arrange
            var file = new Mock<IFormFile>();
            var sourceImg = File.OpenRead(@"D:\\3d max\\web\\Single Room.jpg");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = "Single Room.jpg";
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();
            
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTestCategories().FirstOrDefault(x=>x.Id == 1));
            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(x => x.WebRootPath).Returns("C:\\Users\\Yarou\\Source\\Repos\\Hotelizer\\Hotelizer\\wwwroot");
            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, mockWebHostEnvironment.Object);
            var inputFile = file.Object;

            var testCategory = new Catergory { Id = 1, Name = "Single Room",
                ImageUrl = "/images/categories/Single Room.jpg",
                Imagefile = inputFile
            };
            var returnedCategory = new Catergory
            {
                Id = 1,
                Name = "Single Room",
                BasePrice = 2500,
                ImageUrl = "/images/categories/Single Room.jpg"
            };
            mockRepo.Setup(repo => repo.Update(testCategory))
                .Returns(returnedCategory)
                 .Verifiable();
            // Act.
            var result = await controller.Edit( testCategory );

            //Assign
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Catergory>(okResult.Value);
            mockRepo.Verify();
            file.Verify();
            Assert.Equal(returnedCategory.Name, returnValue.Name);
            Assert.Equal(returnedCategory.ImageUrl, returnValue.ImageUrl);
            Assert.Equal(returnedCategory.Id, returnValue.Id);
            Assert.Equal(returnedCategory.BasePrice, returnValue.BasePrice);
        }
        #endregion

        #region snippet_Edit_ReturnsAOkObject_WhenImageFileEmpty
        [Fact]
        public async Task Edit_ReturnsAOkObject_WhenImageFileEmpty()
        {
            //Arrange
            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.GetById(1))
                .Returns(GetTestCategories().FirstOrDefault(x => x.Id == 1));
            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            var testCategory = new Catergory
            {
                Id = 1,
                Name = "Single Room",
            };
            var returnedCategory = new Catergory
            {
                Id = 1,
                Name = "Single Room",
                BasePrice = 2500,
                ImageUrl = "/images/categories/Double Room.jpg"
            };
            mockRepo.Setup(repo => repo.Update(testCategory))
                .Returns(returnedCategory)
                 .Verifiable();
            // Act.
            var result = await controller.Edit(testCategory);

            //Assign
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Catergory>(okResult.Value);
            mockRepo.Verify();
            Assert.Equal(returnedCategory.Name, returnValue.Name);
            Assert.Equal(returnedCategory.ImageUrl, returnValue.ImageUrl);
            Assert.Equal(returnedCategory.Id, returnValue.Id);
            Assert.Equal(returnedCategory.BasePrice, returnValue.BasePrice);
        }
        #endregion

        #region snippet_DeleteConfirmedPost_ReturnsARedirectToIndexCategory_GivenValidModel
        [Fact]
        public void DeleteConfirmedPost_ReturnsARedirectToIndexCategory_GivenValidModel()
        {
            // Arrange
            int testCategoryId = 1;

            var temp = GetTestCategories();
            var ret = new Catergory();
            List<Catergory> res = new List<Catergory>();
            
            foreach(var cat in temp)
            {
                if (cat.Id != testCategoryId)
                    res.Add(cat);
                else
                    ret = cat;
            }

            var mockRepo = new Mock<ICategoryRepo>();
            mockRepo.Setup(repo => repo.Delete(testCategoryId));
            mockRepo.Setup(repo => repo.GetById(1)).Returns(ret);
            mockRepo.Setup(repo => repo.GetAll(null, null, ""))
                .Returns(res);

            var mockUnitOfWrok = new Mock<IUnitOfWork>();
            mockUnitOfWrok.Setup(x => x.CategoryRepo).Returns(mockRepo.Object);

            var controller = new CatergoriesController(mockUnitOfWrok.Object, null, null, null);

            // Act
            var result = controller.DeleteConfirmed(testCategoryId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Catergory>>(okObjectResult.Value);
            Assert.Single(returnValue);

        }
        #endregion
    }

}
