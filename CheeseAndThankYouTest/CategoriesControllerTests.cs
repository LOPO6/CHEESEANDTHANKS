using CheeseAndThankYou.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheeseAndThankYou.Controllers;
using Microsoft.EntityFrameworkCore;
using CheeseAndThankYou.Models;
using Microsoft.AspNetCore.Mvc;


namespace CheeseAndThankYouTest
{
    [TestClass]
    public class CategoriesControllerTests
    {
        //shared mock db object used in all tests
        private ApplicationDbContext _context;
        CategoriesController controller;


        //startup method that creates db automatically before each test runs
        [TestInitialize]

        public void TestInitialize()
        {
            //create new in-memory db to pass as dependecy to our controller 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            //add some data to the mock db
            _context.Categories.Add(new Category { CategoryId = 56, Name = "Cat 56" });
            _context.Categories.Add(new Category { CategoryId = 32, Name = "Cat 32" });
            _context.Categories.Add(new Category { CategoryId = 91, Name = "Cat 91" });
            _context.SaveChanges();

            //instantiate instance of CategoriesController and pass mock db as dependency to contructor
            controller = new CategoriesController(_context);
        }

        #region "Index"
        [TestMethod]
        public void IndexReturnsView()
        {
            // no arrange - done by TestInitialize() automatically

            //act. Have to add .Result property as Index() is async
            var result = (ViewResult)controller.Index().Result;

            //Assert
            Assert.AreEqual("Index",result.ViewName);


        }

        [TestMethod]
        public void IndexReturnsCategories()
        {
            //no arrange - done by TestInitialize() automatically

            //act
            var result = (ViewResult)controller.Index().Result;
            var dataModel = (List<Category>)result.Model;

            //assert
            CollectionAssert.AreEqual(_context.Categories.ToList(), dataModel); 
        }
        #endregion

        
        [TestMethod]
        public void DetailsNoIdReturns404()
        {
            //act
            var result = (ViewResult)controller.Details(null).Result;

            //assert
            Assert.AreEqual("404", result.ViewName);

        }
        [TestMethod]
        public void DetailsInvalidIdReturns404()
        {
            // act
            var result = (ViewResult)controller.Details(-1).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DetailsValidIdReturnsView()
        {
            //act, passing one of the ids used in the mock db above
            var result = (ViewResult)controller.Details(91).Result;

            //assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdReturnCategory()
        {
            //arrange - set valid id from mock id
            int id = 91;

            //act
            var result = (ViewResult)controller.Details(id).Result;
            var category = (Category)result.Model;

            //Assert
            Assert.AreEqual(_context.Categories.Find(id), category);
        }

        #region "Edit"

        [TestMethod]
        public void EditGetNoIdReturns404()
        {
            // act
            var result = (NotFoundResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void EditGetInvalidIdReturnsNotFound()
        {
            // act
            var result = (NotFoundResult)controller.Edit(-1).Result;

            // assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void EditGetValidIdReturnsView()
        {
            // act
            var result = (ViewResult)controller.Edit(91).Result;

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditPostInvalidCategoryIdReturnsNotFound()
        {
            // arrange
            var category = new Category { CategoryId = -1, Name = "Invalid" };

            // act
            var result = (NotFoundResult)controller.Edit(category.CategoryId, category).Result;

            // assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void EditPostValidCategoryUpdatesCategory()
        {
            // arrange
            var category = _context.Categories.First(c => c.CategoryId == 56);
            category.Name = "Updated Category Name";

            // act
            var result = (RedirectToActionResult)controller.Edit(category.CategoryId, category).Result;

            // assert
            Assert.AreEqual("Index", result.ActionName);
            var updatedCategory = _context.Categories.Find(category.CategoryId);
            Assert.AreEqual("Updated Category Name", updatedCategory.Name);
        }

        #endregion

        #region "Delete"

        [TestMethod]
        public void DeleteGetNoIdReturnsNotFound()
        {
            // act
            var result = (NotFoundResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DeleteGetInvalidIdReturnsNotFound()
        {
            // act
            var result = (NotFoundResult)controller.Delete(-1).Result;

            // assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DeleteGetValidIdReturnsView()
        {
            // act
            var result = (ViewResult)controller.Delete(91).Result;

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedRemovesCategory()
        {
            // arrange
            var categoryToDelete = _context.Categories.First(c => c.CategoryId == 56);

            // act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(categoryToDelete.CategoryId).Result;

            // assert
            Assert.AreEqual("Index", result.ActionName);
            var deletedCategory = _context.Categories.Find(categoryToDelete.CategoryId);
            Assert.IsNull(deletedCategory);
        }

        #endregion
    }



}
