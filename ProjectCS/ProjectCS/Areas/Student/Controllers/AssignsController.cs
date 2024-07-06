using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using ProjectCS.Data;
using ProjectCS.Models;

namespace ProjectCS.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class AssignsController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AssignsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(string id)
		{
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();

            var assigns = _context.Assigns
                                  .Where(a => a.ClassId == id && a.ListAssigns
                                  .Any(la => la.UserId == user.Id))
                                  .ToList();

            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            var className = _context.Classes
                                    .Where(c => c.ClassId == id)
                                    .Select(c => c.Name)
                                    .FirstOrDefault();

            var classRoom = _context.Classes
                                    .Where(c => c.ClassId == id)
                                    .Select(c => c.Room)
                                    .FirstOrDefault();

            ViewBag.ListClass = classes;
            ViewBag.ListName = className;
            ViewBag.ListRoom = classRoom;
            ViewBag.ClassId = id;

            return View(assigns);
        }

		// GET: Student/Assigns/Details/5
		public async Task<IActionResult> ThongBaoDetails(string id)
		{
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var listAssign = await _context.Assigns
                                           .Include(l => l.Class)
                                           .FirstOrDefaultAsync(m => m.AssignId == id);
            if (listAssign == null)
            {
                return NotFound();
            }
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            var classRoom = _context.Classes
                                      .Where(c => c.ClassId == listAssign.ClassId)
                                      .Select(c => c.Room)
                                      .FirstOrDefault();
            var className = _context.Classes
                                      .Where(c => c.ClassId == listAssign.ClassId)
                                      .Select(c => c.Name)
                                      .FirstOrDefault();

            ViewBag.ListRoom = classRoom;
            ViewBag.ListName = className;
            ViewBag.ListClass = classes;

            return View(listAssign);
        }

        public async Task<IActionResult> BaiTapDetails(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var listAssign = await _context.Assigns
                                           .Include(l => l.Class)
                                           .FirstOrDefaultAsync(m => m.AssignId == id);
            if (listAssign == null)
            {
                return NotFound();
            }
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            var classRoom = _context.Classes
                                      .Where(c => c.ClassId == listAssign.ClassId)
                                      .Select(c => c.Room)
                                      .FirstOrDefault();

            var className = _context.Classes
                                      .Where(c => c.ClassId == listAssign.ClassId)
                                      .Select(c => c.Name)
                                      .FirstOrDefault();

            var uploadedFiles = await _context.ListFiles
                                              .Where(f => f.AssignId == id && f.UserId == user.Id)
                                              .ToListAsync();

            ViewBag.ListRoom = classRoom;
            ViewBag.ListName = className;
            ViewBag.ListClass = classes;
            ViewBag.AssignId = id;
            ViewBag.ClassId = listAssign.ClassId;
            ViewBag.UploadedFiles = uploadedFiles;

            return View(listAssign);
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile fileUpload, string assignId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (fileUpload == null || fileUpload.Length == 0 || string.IsNullOrEmpty(assignId))
            {
                return BadRequest("Invalid file or assignment ID.");
            }

            // Generate a random FileId
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var fileId = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

            // Get the LoaiId from the Assign table
            var loaiId = _context.Assigns
                                .Where(a => a.AssignId == assignId)
                                .Select(a => a.LoaiId)
                                .FirstOrDefault();

            if (string.IsNullOrEmpty(loaiId))
            {
                return NotFound("LoaiId not found for the given assignId.");
            }

            // Save the file to the server
            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, fileUpload.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(stream);
            }

            // Create and save the file record in the database
            var fileRecord = new ListFile
            {
                FilePath = $"/uploads/{fileUpload.FileName}",
                FileName = fileUpload.FileName,
                AssignId = assignId,
                UserId = user.Id,
                LoaiId = loaiId,
                FileId = fileId
            };

            _context.ListFiles.Add(fileRecord);
            await _context.SaveChangesAsync();

            return Ok(new { fileId });
        }

        private bool AssignExists(string id)
        {
            return _context.Assigns.Any(e => e.AssignId == id);
        }
    }
}
