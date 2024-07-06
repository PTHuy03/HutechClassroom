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
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StudentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Student/Student
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            return View(classes);
        }

        [HttpGet]
        public async Task<IActionResult> AddStudentToList()
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            ViewBag.ListClass = classes;

            return View(); // Trả về view AddStudentToList
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudentToList(string classId)
        {
            // Kiểm tra xem classId có tồn tại không
            var classExists = await _context.Classes.FindAsync(classId);
            if (classExists == null)
            {
                // Nếu không tồn tại, trả về NotFound hoặc thông báo lỗi phù hợp
                return NotFound();
            }

            // Lấy thông tin user hiện tại
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Nếu không tìm thấy user, có thể yêu cầu đăng nhập hoặc trả về thông báo lỗi
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // Kiểm tra xem user đã được thêm vào ListStudent chưa
                var existingEntry = _context.ListStudents
                                            .FirstOrDefault(ls => ls.ClassId == classId && ls.UserId == currentUser.Id);
                if (existingEntry != null)
                {
                    return RedirectToAction("Index"); // Hoặc trả về view thông báo tùy chọn
                }

                // Nếu chưa tồn tại, thêm user vào ListStudent
                var newListStudent = new ListStudent 
                { 
                    UserId = currentUser.Id, 
                    ClassId = classId 
                };

                _context.ListStudents.Add(newListStudent);
                await _context.SaveChangesAsync();

                var existingAssigns = _context.Assigns
                                              .Where(a => a.ClassId == classId)
                                              .ToList();

                // Thêm các bài tập hiện có vào ListAssign cho sinh viên mới
                foreach (var assignment in existingAssigns)
                {
                    var newListAssign = new ListAssign
                    {
                        UserId = currentUser.Id,
                        AssignId = assignment.AssignId,
                        LoaiId = assignment.LoaiId
                    };
                    _context.ListAssigns.Add(newListAssign);
                }
                await _context.SaveChangesAsync();

                // Chuyển hướng người dùng đến trang mong muốn (ví dụ: trang chi tiết lớp học)
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Xử lý nếu có lỗi xảy ra trong quá trình thêm user vào ListStudent
                // Trong trường hợp này, bạn có thể chọn phương thức xử lý lỗi phù hợp với ứng dụng của mình
                return RedirectToAction("Index");
            }
        }

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

            var listAssign = _context.Assigns
                                     .Include(a => a.ListAssigns) // Include ListAssigns để truy vấn đúng
                                     .Where(ls => ls.ListAssigns.Any(la => la.UserId == user.Id) && ls.ClassId == id)
                                     .ToList();

            ViewBag.ListClass = classes;
            ViewBag.ListAssign = listAssign;
            ViewBag.ClassId = id; // Sử dụng biến 'id' đúng cách

            return View(@class);
        }


        // GET: Student/Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            ViewBag.ListClass = classes;

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            var classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .ToList();
            try
            {
                // Tìm sinh viên liên quan đến lớp này và xóa chúng khỏi cơ sở dữ liệu
                var Students = _context.ListStudents
                                       .Where(ls => ls.UserId == user.Id && ls.ClassId == id);
                _context.ListStudents.RemoveRange(Students);
                //Tìm danh sách bài tập liên quan đến sinh viên đó và xóa chúng
                var Assigns = _context.ListAssigns
                                      .Where(ls => ls.UserId == user.Id && ls.Assign.ClassId == id);
                _context.ListAssigns.RemoveRange(Assigns);
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Trong trường hợp này, bạn có thể chọn phương thức xử lý lỗi phù hợp với ứng dụng của mình
                return RedirectToAction(nameof(Index));
            }

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

        public async Task<IActionResult> CountStudent(string Id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userClasses = _context.ListStudents
                                      .Where(ls => ls.UserId == user.Id)
                                      .Select(ls => ls.ClassId)
                                      .ToList();
            int classes = _context.Classes
                                  .Where(c => userClasses.Contains(c.ClassId))
                                  .Count();

            ViewData["TotalUsers"] = classes;

            return View();
        }

        private bool ClassExists(string id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
