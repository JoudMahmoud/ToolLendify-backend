namespace ToolLendify.Presentation
{
	public class Startup
	{
		public IConfiguration _configuration { get; set; }
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
	}
}
