using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using ProjectCS.Data;
using ProjectCS.Models;

namespace ProjectCS.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeacherController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
        }

        // GET: Teacher/Teacher
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            return View(classes);
        }

        // GET: Teacher/Teacher/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                                       .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
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

            ViewBag.ListClass = classes;
            var listAssign = _context.Assigns
                                     .Include(a => a.ListAssigns) // Include ListAssigns để truy vấn đúng
                                     .Where(ls => ls.ListAssigns.Any(la => la.UserId == user.Id) && ls.ClassId == id)
                                     .ToList();

            ViewBag.ListAssign = listAssign;
            ViewBag.ClassId = id; // Sử dụng biến 'id' đúng cách

            return View(@class);
        }

        // GET: Teacher/Teacher/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");

            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomClassId = new string(Enumerable.Repeat(chars, 5)
                                          .Select(s => s[random.Next(s.Length)]).ToArray());


            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();

            if (userClasses == null || !userClasses.Any())
            {
                ViewBag.ListClass = new List<Class>();
            }
            else
            {
                var classes = _context.Classes.Where(c => userClasses.Contains(c.ClassId)).ToList();
                ViewBag.ListClass = classes;
            }

            var loaiList = _context.Loais.ToList();
            if (loaiList == null)
            {
                loaiList = new List<Loai>();
            }

            ViewBag.RandomClassId = randomClassId;
            ViewBag.LoaiList = new SelectList(loaiList, "LoaiId", "LoaiName");

            return View();
        }

        // POST: Teacher/Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,Name,Titlle,Topic,Room,Image")] Class @class)
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();


            if (ModelState.IsValid)
            {
                var backgroundImages = GetBackgroundImages("wwwroot/Images");

                // Chọn một ảnh ngẫu nhiên từ danh sách
                string randomBackgroundImage = GetRandomBackgroundImage(backgroundImages);

                // Gán ảnh nền cho lớp học
                @class.Image = randomBackgroundImage.Replace("wwwroot/Images", "").Replace("\\", "");

                _context.Add(@class);
                var newList = new ListStudent { UserId = user.Id, ClassId = @class.ClassId };
                _context.Add(newList);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ListClass = classes;

            return View(@class);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssign(string ClassId, string Description, string AssignName)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang Login nếu chưa đăng nhập
            }

            // Kiểm tra tính hợp lệ của ClassId
            if (string.IsNullOrEmpty(ClassId))
            {
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính nếu ClassId không hợp lệ
            }

            // Lấy danh sách sinh viên trong lớp
            var listStudents = _context.ListStudents
                                       .Where(p => p.ClassId == ClassId)
                                       .ToList();
            if (listStudents == null || !listStudents.Any())
            {
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính nếu không tìm thấy sinh viên trong lớp
            }

            // Tạo mới một Assign
            var random = new Random();
            var assignId = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5)
                                 .Select(s => s[random.Next(s.Length)]).ToArray());
            var newAssign = new Assign
            {
                AssignId = assignId,
                ClassId = ClassId,
                LoaiId = "1", // Assuming LoaiId is of string type
                AssignName = AssignName,
                Description = Description,
                Posttime = DateTime.Now
            };

            // Lưu Assign mới vào cơ sở dữ liệu
            _context.Add(newAssign);
            await _context.SaveChangesAsync();

            // Tạo mới danh sách ListAssign cho mỗi sinh viên trong lớp
            foreach (var student in listStudents)
            {
                var newListAssign = new ListAssign
                {
                    UserId = student.UserId,
                    AssignId = newAssign.AssignId,
                    LoaiId = newAssign.LoaiId
                };
                _context.Add(newListAssign);
            }

            // Lưu danh sách ListAssign vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Chuyển hướng người dùng đến trang chi tiết của lớp học
            return RedirectToAction("Details", "Teacher", new { id = ClassId });
        }

        public List<string> GetBackgroundImages(string directoryPath)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var files = Directory.GetFiles(directoryPath)
                                 .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                                 .Select(file => file.Replace("wwwroot/Images", "").Replace("\\", ""))
                                 .ToList();
            return files;
        }

        public string GetRandomBackgroundImage(List<string> images)
        {
            if (images == null || images.Count == 0)
                return null;

            Random rand = new Random();
            int index = rand.Next(0, images.Count);
            return images[index];
        }

        // GET: Teacher/Teacher/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
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

            ViewBag.ListClass = classes;

            return View(@class);
        }

        // POST: Teacher/Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Class updatedClass)
        {
            if (id != updatedClass.ClassId)
            {
                return NotFound();
            }

            // Lấy thông tin lớp học từ cơ sở dữ liệu để lấy ảnh cũ
            var existingClass = await _context.Classes
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(c => c.ClassId == id);

            if (existingClass == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Giữ lại đường dẫn ảnh cũ
                    updatedClass.Image = existingClass.Image;

                    _context.Update(updatedClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(updatedClass.ClassId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            ViewBag.ListClass = classes;

            return View(updatedClass);
        }


        private bool ClassExists(string id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }


        // GET: Teacher/Teacher/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
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

            ViewBag.ListClass = classes;

            return View(@class);
        }

        // POST: Teacher/Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            // Tìm lớp dựa trên id được cung cấp
            var @class = await _context.Classes.FindAsync(id);
            // Kiểm tra xem lớp có tồn tại không
            if (@class == null)
            {
                // Nếu không tồn tại, trả về NotFound
                return NotFound();
            }

            try
            {
                // Xóa lớp khỏi cơ sở dữ liệu
                _context.Classes.Remove(@class);
                // Tìm danh sách sinh viên liên quan đến lớp này và xóa chúng khỏi cơ sở dữ liệu
                var listStudents = _context.ListStudents.Where(ls => ls.ClassId == id);
                _context.ListStudents.RemoveRange(listStudents);

                var Assigns = _context.Assigns.Where(ls => ls.ClassId == id);
                _context.Assigns.RemoveRange(Assigns);

                var listAssigns = _context.ListAssigns.Where(ls => ls.Assign.ClassId == id);
                _context.ListAssigns.RemoveRange(listAssigns);
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Xử lý nếu có lỗi xảy ra trong quá trình xóa
                return RedirectToAction(nameof(Index));
            }
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            ViewBag.ListClass = classes;
            // Chuyển hướng người dùng đến trang Index của lớp
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListStudent(string Id)
        {
            var user = await _userManager.GetUserAsync(User);

            var listStudent = _context.ListStudents
                                      .Where(ls => ls.ClassId == Id)
                                      .ToList();
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();
            var className = _context.Classes
                                    .Where(c => c.ClassId == Id).Select(c => c.Name)
                                    .FirstOrDefault();
            var classRoom = _context.Classes
                                    .Where(c => c.ClassId == Id).Select(c => c.Room)
                                    .FirstOrDefault();
            var classid = _context.Classes
                                  .Where(c => c.ClassId == Id).Select(c => c.ClassId)
                                  .FirstOrDefault();

            ViewBag.ListClass = classes;
            ViewBag.ListName = className;
            ViewBag.ListRoom = classRoom;
            ViewBag.ListId = classid;
            ViewBag.ClassId = Id; // Lưu ClassId vào ViewBag

            return View(listStudent);
        }

        public async Task<IActionResult> ListAssign(string Id)
        {
            var user = await _userManager.GetUserAsync(User);

            var teacherRoleId = _context.Roles
                                        .Where(r => r.Name == "Teacher")
                                        .Select(r => r.Id)
                                        .FirstOrDefault();

            // Lấy danh sách UserId của những người dùng có vai trò "Teacher"
            var teacherUserIds = _context.UserRoles
                                        .Where(ur => ur.RoleId == teacherRoleId)
                                        .Select(ur => ur.UserId)
                                        .ToList();

            var listAssign = _context.ListAssigns
                                      .Include(la => la.Assign)
                                        .ThenInclude(a => a.Class)  // Include Class của Assign
                                      .Include(la => la.User)
                                      .Include(la => la.ListFiles) // Include ListFile
                                      .Where(la => la.Assign.ClassId == Id && !teacherUserIds.Contains(la.UserId))
                                      .ToList();

            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                      .Where(c => userClasses.Contains(c.ClassId))
                                      .ToList();
            var className = _context.Classes
                                      .Where(c => c.ClassId == Id)
                                      .Select(c => c.Name)
                                      .FirstOrDefault();
            var classRoom = _context.Classes
                                      .Where(c => c.ClassId == Id)
                                      .Select(c => c.Room)
                                      .FirstOrDefault();
            var classid = _context.Classes
                                      .Where(c => c.ClassId == Id)
                                      .Select(c => c.ClassId)
                                      .FirstOrDefault();

            ViewBag.ListClass = classes;
            ViewBag.ListName = className;
            ViewBag.ListRoom = classRoom;
            ViewBag.ListId = classid;
            ViewBag.ClassId = Id; // Lưu ClassId vào ViewBag

            return View(listAssign);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePoint(string assignId, string userId, decimal point)
        {
            if (point < 0)
            {
                return BadRequest("Điểm nhập vào không phù hợp!");
            }

            // Kiểm tra phần thập phân của point
            var fractionalPart = point - Math.Truncate(point);
            if (fractionalPart != 0.0m && fractionalPart != 0.5m)
            {
                return BadRequest("Điểm nhập vào phải là số nguyên hoặc kết thúc bằng 0.5!");
            }

            var listAssign = await _context.ListAssigns
                                            .FirstOrDefaultAsync(la => la.AssignId == assignId && la.UserId == userId);
            if (listAssign == null)
            {
                return NotFound();
            }

            listAssign.Point = point;

            _context.Update(listAssign);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
