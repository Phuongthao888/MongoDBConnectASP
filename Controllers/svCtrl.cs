using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Mong.Models;
using MongoDB.Driver;

namespace Mong.Controllers
{
    public class svCtrl : Controller
    {
        private MongoClient client = new MongoClient("mongodb://127.0.0.1:27017/");
        public IActionResult Index()
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");
            var sv = table.Find(FilterDefinition<svMo>.Empty).ToList();
            return View(sv);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(svMo sv)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");
            sv.Id = Guid.NewGuid().ToString();
            table.InsertOne(sv);
            ViewBag.Mgs = "Thêm thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");

            // Tìm sinh viên theo ID
            var sv = table.Find(sv => sv.Id == id).FirstOrDefault();

            if (sv == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy sinh viên
            }

            return View(sv); // Trả về View "Details" với dữ liệu của sinh viên
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");

            // Tìm sinh viên theo ID
            var sv = table.Find(sv => sv.Id == id).FirstOrDefault();

            if (sv == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy sinh viên
            }

            return View(sv); // Trả về View "Delete" với dữ liệu của sinh viên để xác nhận xóa
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");

            // Xóa sinh viên theo ID
            table.DeleteOne(sv => sv.Id == id);

            return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách sau khi xóa thành công
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");

            // Tìm sinh viên theo ID
            var sv = table.Find(sv => sv.Id == id).FirstOrDefault();

            if (sv == null)
            {
                return NotFound(); // Trả về mã trạng thái 404 nếu không tìm thấy sinh viên
            }

            return View(sv); // Trả về View "Edit" với dữ liệu của sinh viên để chỉnh sửa
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(svMo sv)
        {
            var database = client.GetDatabase("QLSV");
            var table = database.GetCollection<svMo>("SV");

            if (ModelState.IsValid)
            {
                // Cập nhật thông tin sinh viên trong cơ sở dữ liệu
                table.ReplaceOne(s => s.Id == sv.Id, sv);
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách sau khi chỉnh sửa thành công
            }

            return View(sv);
        }

    }
}
