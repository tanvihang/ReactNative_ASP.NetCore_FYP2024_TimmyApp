namespace webapi.Utilities.Enums
{
    public enum ResponseStatusEnum
    {
		Success = 200,
        Error = 500,
        NotFound = 404,
        Unauthorized = 401,
        Forbidden = 403,
        BadRequest = 400,
        Conflict = 409,
        UnprocessableEntity = 422
    }
}
