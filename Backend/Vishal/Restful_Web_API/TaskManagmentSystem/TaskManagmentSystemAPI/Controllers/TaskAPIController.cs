using AutoMapper;
using TaskManagmentSystemAPI.Data;
using TaskManagmentSystemAPI.Models;
using TaskManagmentSystemAPI.Models.Dto;
using TaskManagmentSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace TaskManagmentSystemAPI.Controllers
{
    [Route("api/TaskManagerAPI")]
    [ApiController]
    public class TaskAPIController: ControllerBase 
    {
        protected APIResponse _response;
        private readonly ITaskRepository _dbTask;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskAPIController> _logger;
        public TaskAPIController(ITaskRepository dbTask, IMapper mapper, ILogger<TaskAPIController> logger)
        {
            _dbTask = dbTask;
            _mapper = mapper;
            this._response = new();
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetTasks()
        {
            try
            {

                IEnumerable<TaskModel> taskList = await _dbTask.GetAllAsync();
                _response.Result = _mapper.Map<List<TaskDTO>>(taskList);
                _response.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation("done");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name ="GetTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task <ActionResult<APIResponse>> GetTask(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var task = await _dbTask.GetAsync(u => u.Id == id);  //to retrive data equal to the ID without any SQL entity framework takes care of that 
                if (task == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TaskDTO>(task);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CreateTask([FromBody] TaskCreateDTO createDTO)
        {
            try
            {
                if (await _dbTask.GetAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Task already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                TaskModel task = _mapper.Map<TaskModel>(createDTO);
                await _dbTask.CreateAsync(task);

                _response.Result = _mapper.Map<TaskDTO>(task);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetTask", new { id = task.Id }, _response);  //This is to invoke the created route(get the url) 
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteTask")]

        public async Task<ActionResult<APIResponse>> DeleteTask(int id)   // if you use IActionResult no data type id required
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var task = await _dbTask.GetAsync(u => u.Id == id);
                if (task == null)
                {
                    return NotFound();
                }
                await _dbTask.RemoveAsync(task);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateTask")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] TaskUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                TaskModel model = _mapper.Map<TaskModel>(updateDTO);
                await _dbTask.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPatch("{id:int}", Name = "UpdatePartialTask")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialTask(int id, JsonPatchDocument<TaskUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var task = await _dbTask.GetAsync(u => u.Id == id, tracked: false);

            TaskUpdateDTO taskDTO = _mapper.Map<TaskUpdateDTO>(task);

            if (task == null)
            {                                     //check jsonpatch.com for more operations
                return BadRequest();
            }
            patchDTO.ApplyTo(taskDTO, ModelState);
            TaskModel model = _mapper.Map<TaskModel>(taskDTO);

            await _dbTask.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }
}
