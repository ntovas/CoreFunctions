async (context, next) =>
{
	if (context.Request.Path.ToString().Contains("pr0n"))
	{
		context.Response.Redirect("https://www.github.com/");
		return;
	}
	await next.Invoke();
}