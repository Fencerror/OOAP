using System.IO;
using lab3.Collections;
using lab3.Models;

namespace lab3
{
    public class HtmlReportGenerator
    {
        public void GenerateReport(FormCollection formCollection)
        {
            var html = @"
<html>
<head>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            line-height: 1.6;
        }
        h1 {
            color: #333;
            text-align: center;
        }
        h2 {
            color: #555;
            border-bottom: 2px solid #ddd;
            padding-bottom: 5px;
        }
        ul {
            list-style-type: none;
            padding: 0;
        }
        li {
            background: #f9f9f9;
            margin: 5px 0;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        li b {
            color: #333;
        }
    </style>
</head>
<body>
    <h1>Результат опроса</h1>";

            foreach (var block in formCollection.GetBlocks())
            {
                html += $"<h2>{block.Title}</h2><ul>";
                foreach (var field in block.Fields)
                {
                    html += $"<li><b>{field.Label}:</b> {field.Value}</li>";
                }
                html += "</ul>";
            }

            html += @"
</body>
</html>";

            File.WriteAllText("SurveyReport.html", html);
        }
    }
}
