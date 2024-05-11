﻿namespace E_Commerce.API.Errors
{
	public class ApiValidationResponse : ApiResponse
	{
		public ApiValidationResponse() : base(400)
		{
			Errors = new List<string>();
		}
		public IEnumerable<string> Errors { get; set; }
	}
}
