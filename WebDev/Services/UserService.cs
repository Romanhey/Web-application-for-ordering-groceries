
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using WebDev.DTO;
using WebDev.Models;

namespace WebDev.services
{
    public class UserService
    {
        public ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
            { _context = context; }
        public async Task<LoginResponse?> Login(LoginDTO model) 
        {
            var response = new LoginResponse();
            var user = await getUserFromDb(model.Login);
            if (user == null)
            {
                return response;
            }
            if (user.Password != GetHash(model.Password)) {
                return response;
            }
            response.isLogged = true;
            response.user = user;
            return response;
        }

        public bool IsCorrectString(string str)
        {
            if (String.IsNullOrEmpty(str)) return false;
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

        public async Task Register(RegisterDTO model)
        {
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
                Login = model.Login,
                Password = GetHash(model.Password), 
                Address = model.Address,
                Email = model.Email,
                FullName = model.FullName,
                isAdmin = model.Login == "admin" 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public static string GetHash(string pass)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(pass);
            data = System.Security.Cryptography.SHA256.HashData(data);

            return Encoding.ASCII.GetString(data);
        }

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

            // Обновление данных пользователя
            user.Login = model.Login;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.Address = model.Address;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
        }
    }
}
