using Microsoft.AspNetCore.Mvc;
using Mong.Models;
using MongoDB.Driver;

namespace Mong.Controllers
{
    public class lopCtrl : Controller
    {
        private MongoClient client = new MongoClient("mongodb://127.0.0.1:27017/");
        public IActionResult Index()
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");
            var lop = table.Find(FilterDefinition<LopMo>.Empty).ToList();
            return View(lop);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LopMo lop)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");
            lop.Id = Guid.NewGuid().ToString();
            table.InsertOne(lop);
            ViewBag.Mgs = "Thêm thành công!";
            return RedirectToAction("Index");
        }
        public IActionResult Details(string id)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");

            // Tìm sinh viên theo ID
            var lop = table.Find(lop => lop.Id == id).FirstOrDefault();

            if (lop == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy lớp
            }

            return View(lop); // Trả về View "Details" với dữ liệu của lớp
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");

            // Tìm sinh viên theo ID
            var lop = table.Find(lop => lop.Id == id).FirstOrDefault();

            if (lop == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy lớp
            }

            return View(lop); // Trả về View "Delete" với dữ liệu của lớp để xác nhận xóa
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");

            // Xóa sinh viên theo ID
            table.DeleteOne(lop => lop.Id == id);

            return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách sau khi xóa thành công
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");

            // Tìm sinh viên theo ID
            var lop = table.Find(lop => lop.Id == id).FirstOrDefault();

            if (lop == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy lớp
            }

            return View(lop); // Trả về View "Edit" với dữ liệu của lớp để chỉnh sửa
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LopMo lop)
        {
            var database = client.GetDatabase("QLL");
            var table = database.GetCollection<LopMo>("LopHoc");

            if (ModelState.IsValid)
            {
                // Cập nhật thông tin lớp học trong cơ sở dữ liệu
                table.ReplaceOne(s => s.Id == lop.Id, lop);
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách sau khi chỉnh sửa thành công
            }

            return View(lop);
        }
    }
}
