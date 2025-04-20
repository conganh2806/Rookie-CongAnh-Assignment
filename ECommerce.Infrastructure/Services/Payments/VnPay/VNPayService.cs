// using ECommerce.Application.DTOs;
// using ECommerce.Application.Interfaces;
// using ECommerce.ECommerce.Application.DTOs.Common;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Options;
// using VNPAY.NET;

// namespace ECommerce.Infrastructure.Services.Payments
// {
//     public class VNPayService : IPaymentService
//     {
//         private readonly VNPaySettings _settings;

//         public VNPayService(IOptions<VNPaySettings> settings)
//         {
//             _settings = settings.Value;
//         }

//         public Task<string> CreatePaymentUrlAsync(OrderDto order, HttpContext httpContext)
//         {
//             var client = new VNPayClient();

//             var requestData = new VNPayRequestData
//             {
//                 Version = VNPayConstants.VNP_VERSION,
//                 Command = VNPayConstants.VNP_COMMAND,
//                 TmnCode = _settings.TmnCode,
//                 Amount = (int)(order.TotalAmount * 100), // VNPay cần nhân 100
//                 CreateDate = DateTime.Now,
//                 CurrCode = "VND",
//                 IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
//                 Locale = "vn",
//                 OrderInfo = $"Thanh toán đơn hàng {order.Id}",
//                 OrderType = "other",
//                 ReturnUrl = _settings.ReturnUrl,
//                 TxnRef = Guid.Parse(order.Id).ToString("N") // Mã đơn hàng duy nhất
//             };

//             var paymentUrl = client.GenerateRequestUrl(
//                 requestData,
//                 _settings.HashSecret,
//                 _settings.VnpUrl
//             );

//             return Task.FromResult(paymentUrl);
//         }

//         public Task<PaymentResult> ProcessPaymentCallbackAsync(HttpRequest request)
//         {
//             var client = new VNPayClient();
//             var responseData = client.GetFullResponseData(request.Query);

//             bool isValid = client.ValidateSignature(responseData, _settings.HashSecret);

//             if (!isValid)
//             {
//                 return Task.FromResult(new PaymentResult
//                 {
//                     Success = false,
//                     Message = "Chữ ký không hợp lệ",
//                     OrderId = null
//                 });
//             }

//             var responseCode = responseData.ResponseCode;

//             return Task.FromResult(new PaymentResult
//             {
//                 Success = responseCode == "00",
//                 Message = responseCode == "00" ? "Thanh toán thành công" : $"Lỗi: Mã {responseCode}",
//                 OrderId = responseData.TxnRef
//             });
//         }
//     }
// }
