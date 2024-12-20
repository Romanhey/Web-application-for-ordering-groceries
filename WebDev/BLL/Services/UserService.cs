﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Npgsql;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using WebDev.BLL.DTO;
using WebDev.DAL.Entities;

namespace WebDev.BLL.Services
{
    public class UserService : BaseService
    {
        public UserService(ApplicationDBContext context) : base(context) { }
        public async Task<LoginResponse?> Login(LoginDTO model)
        {
            var response = new LoginResponse();
            var user = await getUserFromDb(model.Login);
            if (user == null)
            {
                return response;
            }
            if (user.Password != GetHash(model.Password))
            {
                return response;
            }
            response.isLogged = true;
            response.user = user;
            return response;
        }

        public bool IsCorrectString(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            if (str.Length > 50) return false;
            return true;
        }
        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email)
                && System.Text.RegularExpressions.Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
        }
        private bool ContainsNullByte(string str)
        {
            return str != null && str.Contains('\0');
        }
        private string SanitizeInput(string input)
        {
            // Убираем все управляющие символы
            return string.Concat(input.Where(c => !char.IsControl(c)));
        }
        public async Task Register(RegisterDTO model)
        {
            if (ContainsNullByte(model.Login) || ContainsNullByte(model.Password) || ContainsNullByte(model.Address) || ContainsNullByte(model.FullName))
            {
                throw new ArgumentException("Input contains invalid characters (null byte).");
            }
            if (!IsCorrectString(model.Login))
            {
                throw new ArgumentException("Login is invalid. It must be non-empty and no longer than 50 characters.");
            }

            if (!IsCorrectString(model.Password))
            {
                throw new ArgumentException("Password is invalid. It must be non-empty and no longer than 50 characters.");
            }

            if (!IsCorrectString(model.Address))
            {
                throw new ArgumentException("Address is invalid. It must be non-empty and no longer than 50 characters.");
            }

            if (!IsCorrectString(model.FullName))
            {
                throw new ArgumentException("Full name is invalid. It must be non-empty and no longer than 50 characters.");
            }

            if (!IsValidEmail(model.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            
            var user = await getUserFromDb(model.Login);
            if (user != null)
            {
                throw new InvalidOperationException("Login isn't available.");
            }

            var newUser = new User()
            {
                Login = SanitizeInput(model.Login),
                Password = GetHash(model.Password),
                Address = SanitizeInput(model.Address), 
                Email = SanitizeInput(model.Email),
                FullName = SanitizeInput(model.FullName),
                isAdmin = model.Login == "admin"
            };
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            /*try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
            {
                Console.WriteLine($"PostgreSQL Error: {pgEx.SqlState}");
                Console.WriteLine($"Message: {pgEx.MessageText}");
                Console.WriteLine($"Detail: {pgEx.Detail}");
                Console.WriteLine($"Where: {pgEx.Where}");
                throw new Exception("Ошибка при сохранении данных в базу данных. Проверьте вводимые данные.", ex);
            }
            catch (Exception ex)
            {
                // Логирование всех остальных исключений
                Console.WriteLine($"Общая ошибка: {ex.Message}");
                throw;
            }*/
        }
        public static string GetHash(string pass)
        {
            var data = Encoding.UTF8.GetBytes(pass);
            data = SHA256.HashData(data);


            return Convert.ToBase64String(data);
        }/*
        public static string GetHash(string pass)
        {
            var data = Encoding.UTF8.GetBytes(pass); // Используйте UTF-8 вместо ASCII
            data = SHA256.HashData(data);
            return Convert.ToBase64String(data); // Безопасное представление хэша
        }*/

        public async Task<User> getUserFromDb(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                throw new InvalidOperationException("User not found");

            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(int id, UserUpdateDTO model)
        {
            // Проверка существования пользователя
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Проверка логина
            if (!IsCorrectString(model.Login))
            {
                throw new ArgumentException("Login is invalid. It must be non-empty and no longer than 50 characters.");
            }

            // Проверка email
            if (!IsValidEmail(model.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Проверка на уникальность логина
            var isLoginTaken = await _context.Users.AnyAsync(u => u.Login == model.Login && u.UserId != id);

            if (isLoginTaken)
            {
                throw new InvalidOperationException("The login is already taken by another user.");
            }

            // Проверка имени
            if (!IsCorrectString(model.FullName))
            {
                throw new ArgumentException("Full name is invalid. It must be non-empty and no longer than 50 characters.");
            }

            // Проверка адреса
            if (!IsCorrectString(model.Address))
            {
                throw new ArgumentException("Address is invalid. It must be non-empty and no longer than 50 characters.");
            }

            if (!IsCorrectString(model.Password) || model.Password.Length < 6)
            {
                throw new ArgumentException("Password is invalid. It must be non-empty and no longer than 50 characters.");
            }

            // Обновление данных пользователя
            user.Login = model.Login;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.Password = GetHash(model.Password);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
        }
    }
}
