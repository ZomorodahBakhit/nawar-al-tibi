using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository repository, ILogger<CourseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public CourseDto GetById(int id)
        {
            _logger.LogInformation("Attempting to retrieve course with ID: {Id}", id);

            try
            {
                var course = _repository.GetById(id);
                if (course == null)
                {
                    _logger.LogWarning("Course with ID: {Id} not found", id);
                    throw new NotFoundException($"Course with ID {id} was not found.");
                }

                _logger.LogInformation("Successfully retrieved course with ID: {Id}", id);
                return MapToDto(course);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving course with ID: {Id}", id);
                throw;
            }
        }

        public List<CourseDto> GetAll()
        {
            _logger.LogInformation("Attempting to retrieve all courses");

            var courses = _repository.GetAll()
                .Select(MapToDto)
                .ToList();

            _logger.LogInformation("Successfully retrieved {Count} courses", courses.Count);
            return courses;
        }

        public void Create(CreateCourseForm form)
        {
            _logger.LogInformation("Attempting to create course with Name: {Name}, Weight: {Weight}", form.Name, form.Weight);

            try
            {
                ValidateForm(form);

                var course = new Course
                {
                    Name = form.Name,
                    Weight = form.Weight
                };
                _repository.Add(course);

                _logger.LogInformation("Successfully created course with ID: {Id}", course.Id);
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating course with Name: {Name}", form.Name);
                throw;
            }
        }

        public void Update(int id, UpdateCourseForm form)
        {
            _logger.LogInformation("Attempting to update course with ID: {Id}", id);

            try
            {
                ValidateForm(form);

                var course = _repository.GetById(id);
                if (course == null)
                {
                    _logger.LogWarning("Course with ID: {Id} not found for update", id);
                    throw new NotFoundException($"Course with ID {id} was not found.");
                }

                course.Name = form.Name;
                course.Weight = form.Weight;
                _repository.Update(course);

                _logger.LogInformation("Successfully updated course with ID: {Id}", id);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating course with ID: {Id}", id);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Attempting to delete course with ID: {Id}", id);

            try
            {
                var course = _repository.GetById(id);
                if (course == null)
                {
                    _logger.LogWarning("Course with ID: {Id} not found for deletion", id);
                    throw new NotFoundException($"Course with ID {id} was not found.");
                }

                _repository.Delete(id);
                _logger.LogInformation("Successfully deleted course with ID: {Id}", id);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting course with ID: {Id}", id);
                throw;
            }
        }

        private void ValidateForm(object form)
        {
            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed. Errors: {Errors}",
                    string.Join(", ", validationResult.Errors.SelectMany(e => e.Value)));
                throw new BusinessException(validationResult.Errors);
            }
        }

        private static CourseDto MapToDto(Course course) => new()
        {
            Id = course.Id,
            Name = course.Name,
            Weight = course.Weight
        };
    }

    public interface ICourseService
    {
        CourseDto GetById(int id);
        List<CourseDto> GetAll();
        void Create(CreateCourseForm form);
        void Update(int id, UpdateCourseForm form);
        void Delete(int id);
    }
}
