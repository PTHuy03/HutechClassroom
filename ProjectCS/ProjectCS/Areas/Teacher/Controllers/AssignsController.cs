using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjectCS.Data;
using ProjectCS.Models;

namespace ProjectCS.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class AssignsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AssignsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Teacher/Assigns
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
                                  .Where(a => a.ClassId == id && a.ListAssigns.Any(la => la.UserId == user.Id))
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

        // GET: Teacher/Assigns/Details/5
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

        // GET: Teacher/Assigns/Details/5
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

        // GET: Teacher/Assigns/Create
        public async Task<IActionResult> Create(string Id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách lớp học và thiết lập ViewData["ClassId"]
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "Name");

            // Tạo một mã ngẫu nhiên cho AssignId
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomAssignId = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Lấy danh sách các lớp mà người dùng hiện tại tham gia
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();

            // Nếu không có lớp nào, đặt ViewBag.ListClass thành một danh sách rỗng
            if (userClasses == null || !userClasses.Any())
            {
                ViewBag.ListClass = new List<Class>();
            }
            else
            {
                // Lấy thông tin của các lớp mà người dùng tham gia
                var classes = _context.Classes
                                      .Where(c => userClasses.Contains(c.ClassId))
                                      .ToList();

                ViewBag.ListClass = classes;
            }

            // Lấy danh sách các loại bài tập
            var loaiList = _context.Loais.ToList();
            if (loaiList == null)
            {
                loaiList = new List<Loai>();
            }

            // Đặt giá trị randomAssignId vào ViewBag để sử dụng trong view
            ViewBag.RandomAssignId = randomAssignId;
            ViewBag.ClassId = Id;
            // Đặt danh sách loại bài tập vào ViewBag để sử dụng trong view
            ViewBag.LoaiList = new SelectList(loaiList, "LoaiId", "LoaiName");
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assign assign, IFormFile AssignFile1, IFormFile AssignFile2)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.Remove("Class");
            ModelState.Remove("Loai");
            ModelState.Remove("AssignFile1");
            ModelState.Remove("AssignFile2");
            if (ModelState.IsValid)
            {
                string classId = assign.ClassId;
                if (string.IsNullOrEmpty(classId))
                {
                    return BadRequest("Invalid ClassId.");
                }

                var listStudents = _context.ListStudents
                                           .Where(p => p.ClassId == classId)
                                           .ToList();

                if (listStudents == null || !listStudents.Any())
                {
                    return BadRequest("No students found in this class.");
                }

                // Handle file uploads
                if (AssignFile1 != null && AssignFile1.Length > 0)
                {
                    var filePath1 = Path.Combine(_hostEnvironment.WebRootPath, "AssignFile", AssignFile1.FileName);
                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        await AssignFile1.CopyToAsync(stream);
                    }
                    assign.AssignFile1 = AssignFile1.FileName;
                }

                if (AssignFile2 != null && AssignFile2.Length > 0)
                {
                    var filePath2 = Path.Combine(_hostEnvironment.WebRootPath, "AssignFile", AssignFile2.FileName);
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        await AssignFile2.CopyToAsync(stream);
                    }
                    assign.AssignFile2 = AssignFile2.FileName;
                }

                assign.Posttime = DateTime.Now;

                _context.Add(assign);
                await _context.SaveChangesAsync();

                foreach (var student in listStudents)
                {
                    var newListAssign = new ListAssign
                    {
                        UserId = student.UserId,
                        AssignId = assign.AssignId,
                        LoaiId = assign.LoaiId
                    };
                    _context.Add(newListAssign);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Teacher", new { id = classId });
            }

            var loaiList = _context.Loais.ToList() ?? new List<Loai>();

            ViewBag.LoaiList = new SelectList(loaiList, "LoaiId", "LoaiName");

            return View(assign);
        }


        // GET: Teacher/Assigns/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
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
            
            var assign = await _context.Assigns.FindAsync(id);
            if (assign == null)
            {
                return NotFound();
            }

            ViewBag.ListClass = classes;
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", assign.ClassId);
            ViewData["LoaiId"] = new SelectList(_context.Loais, "LoaiId", "LoaiId", assign.LoaiId);
            ViewBag.Time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.File1 = assign.AssignFile1;
            ViewBag.File2 = assign.AssignFile2;
            ViewBag.Id = assign.ClassId;

            return View(assign);
        }

        // POST: Teacher/Assigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Assign assign)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != assign.AssignId)
            {
                return NotFound();
            }

            ModelState.Remove("Class");
            ModelState.Remove("Loai");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAssign = await _context.Assigns.FindAsync(id);
                    if (existingAssign == null)
                    {
                        return NotFound();
                    }

                    // Preserve ClassId and LoaiId
                    assign.ClassId = existingAssign.ClassId;
                    assign.LoaiId = existingAssign.LoaiId;

                    // Update other properties
                    existingAssign.AssignName = assign.AssignName ?? "Default Assign Name";
                    existingAssign.Description = assign.Description ?? "No description provided.";
                    existingAssign.AssignFile1 = assign.AssignFile1 ?? "No file";
                    existingAssign.AssignFile2 = assign.AssignFile2 ?? "No file";
                    existingAssign.Posttime = assign.Posttime == default ? DateTime.Now : assign.Posttime;

                    _context.Update(existingAssign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignExists(assign.AssignId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Assigns", new { id = assign.ClassId });
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", assign.ClassId);
            ViewData["LoaiId"] = new SelectList(_context.Loais, "LoaiId", "LoaiId", assign.LoaiId);

            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            // Tải lại danh sách lớp từ cơ sở dữ liệu
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            ViewBag.ListClass = classes;

            return View(assign);
        }

        // GET: Teacher/Assigns/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assign = await _context.Assigns
                                       .Include(a => a.Class)
                                       .Include(a => a.Loai)
                                       .FirstOrDefaultAsync(m => m.AssignId == id);
            if (assign == null)
            {
                return NotFound();
            }

            return View(assign);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var assign = await _context.Assigns
                                       .Include(a => a.ListAssigns)
                                            .ThenInclude(la => la.ListFiles) // Bao gồm các bản ghi liên quan trong ListFiles
                                       .FirstOrDefaultAsync(a => a.AssignId == id);

            if (assign != null)
            {
                // Xóa các bản ghi liên quan trong bảng ListFile
                foreach (var listAssign in assign.ListAssigns)
                {
                    _context.ListFiles.RemoveRange(listAssign.ListFiles);
                }

                // Xóa các bản ghi liên quan trong bảng ListAssign
                _context.ListAssigns.RemoveRange(assign.ListAssigns);

                // Xóa bản ghi Assign
                _context.Assigns.Remove(assign);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Assigns", new { id = assign.ClassId });
        }

        private bool AssignExists(string id)
        {
            return _context.Assigns.Any(e => e.AssignId == id);
        }

        public async Task<IActionResult> ListAssign(string Id, string ClassId)
        {
            var user = await _userManager.GetUserAsync(User);

            var teacherRoleId = _context.Roles
                                        .Where(r => r.Name == "Teacher")
                                        .Select(r => r.Id)
                                        .FirstOrDefault();

            var teacherUserIds = _context.UserRoles
                                        .Where(ur => ur.RoleId == teacherRoleId)
                                        .Select(ur => ur.UserId)
                                        .ToList();

            // Truy vấn danh sách ListAssigns có AssignId == Id
            var listAssigns = _context.ListAssigns
                                       .Include(la => la.Assign) // Include Assign
                                       .Include(la => la.User) // Include User liên quan
                                       .Include(la => la.ListFiles) // Include ListFile
                                       .Where(la => la.AssignId == Id && !teacherUserIds.Contains(la.UserId))
                                       .ToList();

            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();

            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();
            var className = _context.Classes
                                  .Where(c => c.ClassId == ClassId)
                                  .Select(c => c.Name)
                                  .FirstOrDefault();
            var classRoom = _context.Classes
                                  .Where(c => c.ClassId == ClassId)
                                  .Select(c => c.Room)
                                  .FirstOrDefault();

            ViewBag.ListName = className;
            ViewBag.ListRoom = classRoom;
            ViewBag.ClassId = ClassId;
            ViewBag.ListClass = classes;
            ViewBag.AssignId = Id; // Lưu ClassId vào ViewBag

            return View(listAssigns);
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
