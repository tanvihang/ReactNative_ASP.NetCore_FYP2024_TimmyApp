﻿namespace webapi.Models.DTO
{
	public class PageDTO
	{
		public int PageSize { get; set; } = 10;
		public int CurrentPage { get; set; }

		public override string ToString()
		{
			return $"Page Size: {PageSize}, Current Page: {CurrentPage}";
		}
	}
}
