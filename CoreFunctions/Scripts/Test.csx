async (context, next) =>
{
	if (context.Request.Path.ToString().Contains("pr0n"))
	{
		var response = context.Response;
		response.Headers[HeaderNames.Location] = "http://www.github.com";
		response.StatusCode = 301;
		return;
	}
	await next();
}