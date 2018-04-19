using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace be4care.MarkupExtensions
{
    public class EmbeddedImage : IMarkupExtension
    {
        public String resourceId { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrWhiteSpace(resourceId))
                return null;
            return Xamarin.Forms.ImageSource.FromResource(resourceId);
        }
    }
}
