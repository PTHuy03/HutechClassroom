using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using ProjectCS.Data;
using ProjectCS.Models;
using ProjectCS.Services;

namespace ProjectCS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SendEmail _sendEmail;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SendEmail sendEmail, ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
            _sendEmail = sendEmail;
        }

        // GET: Admin/Index
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<ApplicationUser, IList<string>>();
            var allRoles = await _roleManager.Roles.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user] = roles;
            }

            ViewBag.UserRoles = userRoles;
            ViewBag.AllRoles = allRoles;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SetUserRole(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                // Xử lý lỗi khi xóa role không thành công
                return RedirectToAction("Error", "Home");
            }

            result = await _userManager.AddToRoleAsync(user, selectedRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Xử lý lỗi khi thêm role không thành công
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var listStudents = _context.ListStudents.Where(s => s.UserId == user.Id).ToList();
            _context.ListStudents.RemoveRange(listStudents);

            var listAssigns = _context.ListAssigns.Where(a => a.UserId == user.Id).ToList();
            _context.ListAssigns.RemoveRange(listAssigns);

            var listFiles = _context.ListFiles.Where(f => f.UserId == user.Id).ToList();
            _context.ListFiles.RemoveRange(listFiles);

            await _context.SaveChangesAsync();

            // Xóa người dùng
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted successfully.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ResetPassword()
        {
            var users = _userManager.Users.ToList();

            var usersWithPasswordFalse = await _userManager.Users
                .Where(u => u.IsPassword == false)
                .ToListAsync();

            return View(usersWithPasswordFalse);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token == null)
            {
                // Xử lý lỗi khi không tạo được token
                return RedirectToAction("Error", "Home");
            }

            // Tạo mật khẩu mới ngẫu nhiên
            var newPassword = GenerateRandomPassword();

            // Đặt mật khẩu mới cho người dùng
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            
            if (result.Succeeded)
            {
                try
                {
                    user.IsPassword = true;
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        // Xử lý lỗi khi không cập nhật được user
                        foreach (var error in updateResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("Error");
                    }

                    _sendEmail.SendPassword(user.Email, newPassword);
                    _logger.LogInformation("Password reset email sent to {Email}", user.Email);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send password reset email to {Email}", user.Email);
                }
                return RedirectToAction(nameof(ResetPassword));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Error");
            }
            
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()";

            var random = new Random();

            var password = new char[10];
            password[0] = digits[random.Next(digits.Length)];
            password[1] = specialChars[random.Next(specialChars.Length)];
            for (int i = 2; i < password.Length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password.OrderBy(c => random.Next()).ToArray());
        }

    }
}