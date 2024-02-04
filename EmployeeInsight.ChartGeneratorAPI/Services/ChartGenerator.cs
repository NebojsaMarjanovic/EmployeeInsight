using EmployeeInsight.Crawler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using EmployeeInsight.Crawler.Repository;


namespace EmployeeInsight.ChartGeneratorAPI.Services
{
    public class ChartGenerator : IChartGenerator
    {
        private readonly IEmployeeCatalogProvider _employeeCatalogProvider;
        public ChartGenerator(IEmployeeCatalogProvider employeeCatalogProvider)
        {
            _employeeCatalogProvider = employeeCatalogProvider;
        }

        public async Task<FileStreamResult> GeneratePieChart(CancellationToken cancellationToken)
        {
            var employees = await _employeeCatalogProvider.GetEmployeesData(cancellationToken);

            if (employees is null)
            {
                return new FileStreamResult(Stream.Null, "application/octet-stream");
            }

            string[] names = employees.Select(e => e.Name).ToArray();
            double[] durations = employees.Select(e => e.Duration).ToArray();

            double totalDuration = durations.Sum();

            //create a new bitmap with a specified size
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);

            //generate colors for the pie slices and legend dynamically
            Color[] colors = GenerateColors(names.Length);

            //draw pie chart
            float startAngle = 0;
            for (int i = 0; i < names.Length; i++)
            {
                float sweepAngle = (float)(360 * durations[i] / totalDuration);
                graphics.FillPie(new SolidBrush(colors[i]), 200, 150, 300, 300, startAngle, sweepAngle);
                startAngle += sweepAngle;
            }

            //create a legend
            int legendX = 550;
            int legendY = 50;
            int legendWidth = 200;
            int legendHeight = names.Length * 20 + 10; //calculated legend height based on the number of employees

            //draw legend background
            graphics.FillRectangle(Brushes.LightGray, legendX, legendY, legendWidth, legendHeight);

            for (int i = 0; i < names.Length; i++)
            {
                Rectangle legendRect = new Rectangle(legendX + 10, legendY + i * 20, 10, 10);
                graphics.FillRectangle(new SolidBrush(colors[i]), legendRect);
                double percentage = (double)durations[i] / totalDuration * 100;
                graphics.DrawString($"{names[i]} - {percentage:F1}%", new Font("Arial", 10), Brushes.Black, legendX + 30, legendY + i * 20);
            }

            //save char as png image
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            stream.Position = 0;


            return new FileStreamResult(stream, "image/png")
            {
                FileDownloadName = "piechart.png"
            };
        }

        private Color[] GenerateColors(int count)
        {
            Color[] colors = new Color[count];
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                colors[i] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }
            return colors;
        }
    }
}
