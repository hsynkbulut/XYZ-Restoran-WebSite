using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnlineRestaurantProject.Extensions
{
    [HtmlTargetElement("custom-tag-button")]
    public class CustomTagHelper : TagHelper
    {
        public string Type { get; set; }
        public string ButtonClass { get; set; }
        public string IconClass { get; set; } // Yeni eklenen özellik

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("class", ButtonClass);

            //Ikonun dışarıdan alınan değerini kullanıyoruz
            output.Content.SetHtmlContent($"<i class='{IconClass}'></i>");

            //var content = context.AllAttributes["content"]?.Value;

            //if (content != null)
            //{
            //    //output.Content.SetContent(content.ToString());
            //    output.Content.SetHtmlContent("<i class='bi bi-cart text-white'></i>");
            //}
        }
    }
}
