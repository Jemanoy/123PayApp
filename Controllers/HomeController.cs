using _123PayApp.Helper;
using _123PayApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace _123PayApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly db_123payContext _context;

		public HomeController(ILogger<HomeController> logger, db_123payContext context)
		{
			_logger = logger;
			_context = context;

			ViewData["pageNum"] = 1;
		}

		public IActionResult Index(string page = "")
		{
			var pageNum = Convert.ToInt32(ViewData["pageNum"]);
            if (page == "previous")
            {
				pageNum++;
			}
            else
            {
				pageNum++;
            }
			var result = _context.VwTransactionLists
				.OrderByDescending(a => a.TransactionDate)
				.PagedRecords(pageNum, 10)
				.ToList();
			return View(result);
		}

		[Route("[controller]/process")]
		public IActionResult ProcessTransaction(string refNo)
		{
			try
			{
				var result = _context.TblPaymentTransactions.Find(refNo);

				result.StatusId = 2;

				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
			return RedirectToAction("Index");
		}

		[Route("[controller]/done")]
		public IActionResult DoneTransaction([FromQuery]string refNo, [FromForm]IFormFile attachment)
		{
			try
			{
				var result = _context.TblPaymentTransactions.Find(refNo);

				result.StatusId = 3;

				using (var ms = new MemoryStream())
				{
					attachment.CopyTo(ms);
					var fileBytes = ms.ToArray();

					result.Attachments = fileBytes;
				}

				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
			return RedirectToAction("Index");
		}

		[Route("[controller]/fail")]
		public IActionResult FailedTransaction([FromQuery] string refNo, [FromForm] IFormFile attachment)
		{
			try
			{
				var result = _context.TblPaymentTransactions.Find(refNo);

				if (attachment != null)
                {
					using (var ms = new MemoryStream())
					{
						attachment.CopyTo(ms);
						var fileBytes = ms.ToArray();

						result.Attachments = fileBytes;
					}
				}

				result.StatusId = 4;

				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}
			return RedirectToAction("Index");
		}

		[Route("[controller]/attachment")]
		public IActionResult ViewAttachment([FromQuery]string refNo)
		{
			try
			{
				var result = _context.TblPaymentTransactions.Find(refNo).Attachments;
				return Ok(Convert.ToBase64String(result));
			}
            catch (Exception ex)
            {
				return StatusCode(500, ex);
            }
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
