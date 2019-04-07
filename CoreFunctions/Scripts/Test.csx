async (context, next) =>
{
	if (context.Request.Path.ToString().Contains("pr0n"))
	{
		var wc = new WebClient();
		var str = await wc.DownloadStringTaskAsync(
			new Uri("https://www.github.com"));
		var response = context.Response;
		await response.WriteAsync(str);
		response.StatusCode = 200;
		return;
	}
	await next();
}