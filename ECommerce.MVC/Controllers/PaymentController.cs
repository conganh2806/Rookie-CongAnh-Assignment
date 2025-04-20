using Microsoft.AspNetCore.Mvc;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;
using System;
using ECommerce.MVC.Models;
using ECommerce.MVC.Controllers;
using SQLitePCL;

namespace Backend_API_Testing.Controllers
{
    public class VnpayController : BaseController
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;

        public VnpayController(IVnpay vnPayservice, 
                                IConfiguration configuration,
                                ILogger<VnpayController> logger)
                : base(logger)
        {
            _vnpay = vnPayservice;
            _configuration = configuration;

            _vnpay.Initialize(_configuration["Vnpay:TmnCode"] ?? string.Empty,
                            _configuration["Vnpay:HashSecret"] ?? string.Empty,
                            _configuration["Vnpay:BaseUrl"] ?? string.Empty,
                            _configuration["Vnpay:CallbackUrl"] ?? string.Empty);
        }

        [HttpGet("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl(double money, string description)
        {
            try
            {
                var ipAddress = NetworkHelper.GetIpAddress(HttpContext);

                var request = new PaymentRequest
                {
                    PaymentId = DateTime.Now.Ticks,
                    Money = money,
                    Description = description,
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY, 
                    CreatedDate = DateTime.Now, 
                    Currency = Currency.VND, 
                    Language = DisplayLanguage.Vietnamese
                };

                var paymentUrl = _vnpay.GetPaymentUrl(request);

                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message }); 
            }
        }

        [HttpGet("payment-return")]
        public IActionResult Callback()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpay.GetPaymentResult(Request.Query);

                    if (paymentResult.IsSuccess)
                    {
                        return View("PaymentSuccess", paymentResult);
                    }

                    return View("PaymentFailure", paymentResult);
                }
                catch (Exception ex)
                {
                    return View("Error", new ErrorViewModel { Message = ex.Message });
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }
    }
}
