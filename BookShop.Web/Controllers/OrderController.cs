using EBook.Domain.DomainModels;
using EBook.Domain.Identity;
using EBook.Service.Interface;
using ClosedXML.Excel;
using EBook.Domain;

using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this.orderService.getAllOrders(userId);
            return View(result);
        }

        public IActionResult Details(Guid id)
        {
            var model = new BaseEntity
            {
                Id = id
            };
            var result = this.orderService.getOrderDetails(model);
            return View(result);
        }

        [HttpGet]
        public FileContentResult ExportAllOrders()
        {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Orders");
                worksheet.ColumnWidth = 50;

                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Costumer Email";

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = this.orderService.getAllOrders(userId);

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.User.Email;

                    for (int p = 0; p < item.BookInOrders.Count(); p++)
                    {
                        worksheet.Cell(1, p + 3).Value = "Ticket - " + (p + 1);
                        worksheet.Cell(i + 1, p + 3).Value = item.BookInOrders.ElementAt(p).Book.BookName;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }

        public FileContentResult CreateInvoice(Guid id)
        {
            var model = new BaseEntity
            {
                Id = id
            };
            var result = this.orderService.getOrderDetails(model);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "files//Invoice.docx");
            var document = DocumentModel.Load(templatePath);


            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.User.UserName);

            StringBuilder sb = new StringBuilder();

            var totalPrice = 0.0;

            foreach (var item in result.BookInOrders)
            {
                totalPrice += item.Quantity * item.Book.Price;
                sb.AppendLine(item.Quantity + " ticket\\s for the movie \'" + item.Book.BookName + "\' with price of: " + item.Book.Price+ "$");
            }


            document.Content.Replace("{{TicketList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString() + "$");


            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}