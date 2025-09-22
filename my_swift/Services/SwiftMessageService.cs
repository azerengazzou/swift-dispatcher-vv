using System.Text.RegularExpressions;
using my_swift.Models;

namespace my_swift.Services
{
    public class SwiftMessageService : ISwiftMessageService
    {
        public List<SwiftCategory> GetAllCategories()
        {
            //TODO : READ FROM DB OR JSON
            return new List<SwiftCategory>
        {
            new SwiftCategory
            {
                Id = "system",
                Title = "0 - System Messages",
                Messages = new List<SwiftMessage>
                {
                    new() { Code = "MT 001 – 099", Description = "SWIFT system and network management messages." }
                }
            },
            new SwiftCategory
            {
                Id = "payments",
                Title = "1 - Customer Payments and Cheques",
                Messages = new List<SwiftMessage>
                {
                    new() { Code = "MT 101", Description = "Request for Transfer." },
                    new() { Code = "MT 102", Description = "Multiple Customer Credit Transfer." }
                }
            }
        };
        }

        public List<ParsedField> Parse(string fileName, string rawContent)
        {
            if (string.IsNullOrWhiteSpace(rawContent))
                return new List<ParsedField>();

            // Decide parser by filename / extension / header
            if (fileName.Contains("101"))
                return ParseMt101(rawContent);

            // fallback
            return new List<ParsedField>
            {
                new() { Tag = "N/A", Description = "Unknown format", Value = rawContent }
            };
        }

        private List<ParsedField> ParseMt101(string text)
        {
            text = text.Replace("\r", "");

            var regex = new Regex(
                @"(?<tag>:\d{2}[A-Z]?:)(?<val>.*?)(?=:\d{2}[A-Z]?:|\Z)",
                RegexOptions.Singleline);

            var list = new List<ParsedField>();
            foreach (Match m in regex.Matches(text))
            {
                var tag = m.Groups["tag"].Value.Trim(':');
                var val = m.Groups["val"].Value.Trim();

                list.Add(new ParsedField
                {
                    Tag = tag,
                    Description = GetDescription(tag),
                    Value = val
                });
            }

            return list;
        }

        private static string GetDescription(string tag) =>
            tag switch
            {
                "20" => "Transaction Reference Number",
                "21R" => "Customer Reference",
                "28D" => "Message Index/Total",
                "50H" or "50K" => "Ordering Customer",
                "30" => "Requested Execution Date",
                "32B" => "Currency / Amount",
                "57A" or "57D" => "Account With Institution",
                "59" => "Beneficiary",
                "70" => "Remittance Information",
                "71A" => "Details of Charges",
                "72" => "Sender to Receiver Information",
                _ => "Unknown"
            };
    }
}