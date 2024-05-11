﻿using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly IConfiguration _configuration;

		public PaymentController(IPaymentService paymentService, IConfiguration configuration)
		{
			_paymentService = paymentService;
			_configuration = configuration;
		}


		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> CreatePaymentIntent(CustomerBasketDto input)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input);
			return Ok(basket);
		}


		[HttpPost("webhook")]
		public async Task<IActionResult> Index()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], _configuration["Stripe:endpointSecret"]);
				var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

				// Handle the event
				if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
				{
					await _paymentService.UpdatePaymentStatusFailed(paymentIntent.Id);
				}
				else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
				{
					await _paymentService.UpdatePaymentStatusSucceded(paymentIntent.Id);
				}
				// ... handle other event types
				else
				{
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}

				return Ok();
			}
			catch (StripeException e)
			{
				return BadRequest();
			}
		}
	}
}
