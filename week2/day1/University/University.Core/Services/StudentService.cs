using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public StudentDto GetById(int id)
        {
            _logger.LogInformation("Attempting to retrieve student with ID: {Id}", id);

            try
            {
                var student = _repository.GetById(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID: {Id} not found", id);
                    throw new NotFoundException($"Student with ID {id} was not found.");
                }

                _logger.LogInformation("Successfully retrieved student with ID: {Id}", id);
                return MapToDto(student);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving student with ID: {Id}", id);
                throw;
            }
        }

        public List<StudentDto> GetAll()
        {
            _logger.LogInformation("Attempting to retrieve all students");

            var students = _repository.GetAll()
                .Select(MapToDto)
                .ToList();

            _logger.LogInformation("Successfully retrieved {Count} students", students.Count);
            return students;
        }

        public void Create(CreateStudentForm form)
        {
            _logger.LogInformation("Attempting to create student with Name: {Name}, Email: {Email}", form.Name, form.Email);

            try
            {
                ValidateForm(form);

                if (_repository.GetByEmail(form.Email) != null)
                {
                    _logger.LogWarning("Duplicate email submitted: {Email}", form.Email);
                    throw new BusinessException("A student with this email address already exists.");
                }

                var student = new Student
                {
                    Name = form.Name,
                    Email = form.Email
                };
                _repository.Add(student);

                _logger.LogInformation("Successfully created student with ID: {Id}", student.Id);
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating student with Email: {Email}", form.Email);
                throw;
            }
        }

        public void Update(int id, UpdateStudentForm form)
        {
            _logger.LogInformation("Attempting to update student with ID: {Id}", id);

            try
            {
                ValidateForm(form);

                var student = _repository.GetById(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID: {Id} not found for update", id);
                    throw new NotFoundException($"Student with ID {id} was not found.");
                }

                var existingWithEmail = _repository.GetByEmail(form.Email);
                if (existingWithEmail != null && existingWithEmail.Id != id)
                {
                    _logger.LogWarning("Duplicate email submitted on update: {Email}", form.Email);
                    throw new BusinessException("A student with this email address already exists.");
                }

                student.Name = form.Name;
                student.Email = form.Email;
                _repository.Update(student);

                _logger.LogInformation("Successfully updated student with ID: {Id}", id);
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
                _logger.LogError(ex, "Error occurred while updating student with ID: {Id}", id);
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Attempting to delete student with ID: {Id}", id);

            try
            {
                var student = _repository.GetById(id);
                if (student == null)
                {
                    _logger.LogWarning("Student with ID: {Id} not found for deletion", id);
                    throw new NotFoundException($"Student with ID {id} was not found.");
                }

                _repository.Delete(id);
                _logger.LogInformation("Successfully deleted student with ID: {Id}", id);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting student with ID: {Id}", id);
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

        private static StudentDto MapToDto(Student student) => new()
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email
        };
    }

    public interface IStudentService
    {
        StudentDto GetById(int id);
        List<StudentDto> GetAll();
        void Create(CreateStudentForm form);
        void Update(int id, UpdateStudentForm form);
        void Delete(int id);
    }
}
