using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TodoManager.Dal.Cache;
using TodoManager.Models;
using TodoManager.Models.Dto;
using TodoManager.Models.Enum;
using Westwind.AspNetCore.Markdown;

namespace TodoManager.Controllers
{
    [Route("/")]
    [Route("/home")]
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private readonly IDisctributedCacheProvider cacheProvider;

        public HomeController(IConfiguration configuration,
            IDisctributedCacheProvider cacheProvider)
        {
            this.configuration = configuration;
            this.cacheProvider = cacheProvider;
        }

        [Route("")]
        public IActionResult Index()
        {
            return Content(GetFullMarkdown("README.md"), "text/html", Encoding.UTF8);
        }

        [Route("about")]
        public IActionResult About()
        {
            return Content(GetFullMarkdown("MD/About.md"), "text/html", Encoding.UTF8);
        }

        [Route("works")]
        public IActionResult Works()
        {
            string header = GetMarkdownFromMd("MD/Nav.md");
            string md = Markdown.ParseFromUrl("https://github.com/borllor/Works/blob/master/ReadMe.md");
            return Content(header + md, "text/html", Encoding.UTF8);
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return Content(GetFullMarkdown("MD/Contact.md"), "text/html", Encoding.UTF8);
        }

        [Route("testRedis")]
        public IActionResult TestRedis()
        {
            cacheProvider.Set("username", new UserDto() { Username = "borllor" });
            UserDto user = cacheProvider.Get<UserDto>("username");
            if (user != null)
            {
                return Content(JsonConvert.SerializeObject(SimpleResponseDto<bool>.OK(true)), "application/json", Encoding.UTF8);

            }
            return Content(JsonConvert.SerializeObject(SimpleResponseDto<bool>.Failed(ResponseCodeEnum.ResponseCode_100)), "application/json", Encoding.UTF8);
        }



        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok("Error, Please contact Borllor Li");
        }

        private string GetFullMarkdown(string fileName)
        {
            string header = GetMarkdownFromMd("MD/Nav.md");
            string tar = GetMarkdownFromMd(fileName);
            return header + tar;
            //return string.Format("{0}<p>{1}</p>{2}", header,
            //configuration.GetConnectionString("TimeManagerContext"), tar);
        }

        private string GetMarkdownFromMd(string fileName)
        {
            string content = "";
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(Path.Combine(GetRootPath(), fileName));
                content = streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
            return Markdown.ParseHtmlString(content).Value;
        }

        private string GetRootPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
