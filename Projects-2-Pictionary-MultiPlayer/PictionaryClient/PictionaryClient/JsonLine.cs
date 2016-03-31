using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PictionaryClient
{


    [Serializable]
    public class JsonLine
    {
        public string Color { get; set; }
        public Double StrokeThickness { get; set; }
        public Double X1 { get; set; }
        public Double Y1 { get; set; }
        public Double X2 { get; set; }
        public Double Y2 { get; set; }

        public static Line LineDeserialize(JsonLine jsonLine)
        {
            var line = new Line();

            line.Stroke = (SolidColorBrush) new BrushConverter().ConvertFromString(jsonLine.Color);
            line.StrokeThickness = jsonLine.StrokeThickness;

            line.X1 = jsonLine.X1;
            line.Y1 = jsonLine.Y1;
            line.X2 = jsonLine.X2;
            line.Y2 = jsonLine.Y2;
            return line;
        }
    }
}
