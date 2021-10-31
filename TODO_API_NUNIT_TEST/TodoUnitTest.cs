using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using TODO_API.Controllers;
using TODO_API.Models.Request;
using TODO_API.Models.Response;

namespace TODO_API_NUNIT_TEST
{
    public class Tests
    {
        private readonly TodoController _todoController;
        private readonly TodoServiceFake _todoServiceFake;

        public Tests()
        {
            _todoServiceFake = new TodoServiceFake();
            _todoController = new TodoController(_todoServiceFake);
        }

        #region Add Todo Test
        [Test]
        public void AddTodo_ValidObject_ReturnOk()
        {
            CreateTodoRequest request = new CreateTodoRequest() {
                title = "Notes",
                desc = "New Notes",
                perc = 0,
                exp_date = Convert.ToDateTime("28-10-2021")
            };

            var response = _todoController.CreateTodo(request);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void AddTodo_InvalidObject_ReturnBadRequest()
        {
            CreateTodoRequest request = new CreateTodoRequest()
            {
                title = "Notes",
                desc = "New Notes",
                perc = 0
            };

            _todoController.ModelState.AddModelError("exp_date", "Required");

            var response = _todoController.CreateTodo(request);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void AddTodo_ValidObject_ReturnCreatedItem()
        {
            CreateTodoRequest request = new CreateTodoRequest()
            {
                title = "Notes Tst",
                desc = "New Notes",
                perc = 0,
                exp_date = Convert.ToDateTime("28-10-2021")
            };

            var response = _todoController.CreateTodo(request) as OkObjectResult;
            var item = response.Value as GetTodoResponse;

            Assert.IsInstanceOf<GetTodoResponse>(item);
            Assert.AreEqual("Notes Tst", item.todo.Title);
        }
        #endregion

        #region Update Todo Test
        [Test]
        public void UpdateTodo_ValidObject_ReturnOk()
        {
            UpdateTodoRequest request = new UpdateTodoRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                title = "Grocery List Updated",
                desc = "1. Milk, 2. Eggs, 3.Butter",
                perc = 100,
                exp_date = Convert.ToDateTime("29/10/2021")
            };

            var response = _todoController.UpdateTodo(request);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void UpdateTodo_InvalidObject_ReturnBadRequest()
        {
            UpdateTodoRequest request = new UpdateTodoRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                desc = "1. Milk, 2. Eggs, 3.Butter",
                perc = 100,
                exp_date = Convert.ToDateTime("29/10/2021")
            };

            _todoController.ModelState.AddModelError("title", "Required");

            var response = _todoController.UpdateTodo(request);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void UpdateTodo_ValidObject_ReturnCreatedItem()
        {
            UpdateTodoRequest request = new UpdateTodoRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                title = "Grocery List Updated",
                desc = "1. Milk, 2. Eggs, 3.Butter",
                perc = 100,
                exp_date = Convert.ToDateTime("29/10/2021")
            };

            var response = _todoController.UpdateTodo(request) as OkObjectResult;
            var item = response.Value as GetTodoResponse;

            Assert.IsInstanceOf<GetTodoResponse>(item);
            Assert.AreEqual("Grocery List Updated", item.todo.Title);
        }
        #endregion

        #region Delete Todo Test
        [Test]
        public void DeleteTodo_NotExistingId_ReturnBadRequest()
        {
            ByIdRequest param = new ByIdRequest()
            {
                id = Guid.NewGuid()
            };

            var badResponse = _todoController.DeleteTodo(param);
            Assert.IsInstanceOf<BadRequestResult>(badResponse);
        }

        [Test]
        public void DeleteTodo_ValidId_ReturnOk()
        {
            ByIdRequest param = new ByIdRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            var badResponse = _todoController.DeleteTodo(param);
            Assert.IsInstanceOf<OkResult>(badResponse);
        }
        #endregion

        #region Set Todo Percentage Test
        [Test]
        public void SetPercentage_ValidObject_ReturnOk()
        {
            SetPercentageRequest request = new SetPercentageRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                perc = 89,
            };

            var response = _todoController.SetTodoPerc(request);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void SetPercentage_InvalidObject_ReturnBadRequest()
        {
            SetPercentageRequest request = new SetPercentageRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            _todoController.ModelState.AddModelError("perc", "Required");

            var response = _todoController.SetTodoPerc(request);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void SetPercentage_ValidObject_ReturnCreatedItem()
        {
            SetPercentageRequest request = new SetPercentageRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                perc = 40,
            };

            var response = _todoController.SetTodoPerc(request) as OkObjectResult;
            var item = response.Value as GetTodoResponse;

            Assert.IsInstanceOf<GetTodoResponse>(item);
            Assert.AreEqual(40, item.todo.Percentage);
        }
        #endregion

        #region Mark Todo Done Test
        [Test]
        public void MarkTodo_ValidObject_ReturnOk()
        {
            ByIdRequest request = new ByIdRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            var response = _todoController.MarkTodoDone(request);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void MarkTodo_ValidObject_ReturnCreatedItem()
        {
            ByIdRequest request = new ByIdRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            var response = _todoController.MarkTodoDone(request) as OkObjectResult;
            var item = response.Value as GetTodoResponse;

            Assert.IsInstanceOf<GetTodoResponse>(item);
        }
        #endregion

        #region Get All Todo Test
        [Test]
        public void GetAllTodo_ReturnsOkResult()
        {
            var response = _todoController.GetAllTodo();
            Assert.IsInstanceOf<OkObjectResult>(response as OkObjectResult);
        }

        [Test]
        public void GetAllTodo_ReturnsAllItems()
        {
            var response = _todoController.GetAllTodo() as OkObjectResult;
            var item = response.Value as AllTodoResponse;
            Assert.IsInstanceOf<AllTodoResponse>(item);
        }
        #endregion

        #region Get Incoming Todo Test
        [Test]
        public void GetIncomingTodo_ReturnsOkResult()
        {
            var response = _todoController.GetIncomingTodo();
            Assert.IsInstanceOf<OkObjectResult>(response as OkObjectResult);
        }

        [Test]
        public void GetIncomingTodo_ReturnsAllItems()
        {
            var response = _todoController.GetIncomingTodo() as OkObjectResult;
            var item = response.Value as IncomingTodoResponse;
            Assert.IsInstanceOf<IncomingTodoResponse>(item);
        }
        #endregion

        #region Get Specific Todo Test
        [Test]
        public void GetTodoById_InvalidGuid_ReturnsNotFound()
        {
            ByIdRequest request = new ByIdRequest()
            {
                id = new Guid()
            };

            var response = _todoController.GetTodoById(request);
            Assert.IsInstanceOf<NotFoundObjectResult>(response);
        }

        [Test]
        public void GetTodoById_ReturnsOkResult()
        {
            ByIdRequest request = new ByIdRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            var response = _todoController.GetTodoById(request);
            Assert.IsInstanceOf<OkObjectResult>(response as OkObjectResult);
        }

        [Test]
        public void GetTodoById_ReturnsTodoItems()
        {
            ByIdRequest request = new ByIdRequest()
            {
                id = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211")
            };

            var response = _todoController.GetTodoById(request) as OkObjectResult;
            var item = response.Value as GetTodoResponse;
            Assert.IsInstanceOf<GetTodoResponse>(item);
        }
        #endregion

    }
}