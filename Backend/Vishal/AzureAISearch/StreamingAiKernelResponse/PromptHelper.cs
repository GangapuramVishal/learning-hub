namespace Api.Helper
{
    public class PromptHelper
    {
        public static string GenerateResponse()
        {
            return $@"
                You are an expert in network security, specializing in Sentinel logs, and external threat intelligence tools

                Instructions:
                - Please Understand the JSON Format first and don't skip any Key-value field in the JSON schema.
                - Make sure your output follows this exact JSON format.
                - Provide a **complete response** do not trim.
                Deliver the information in a structured, detailed, and readable format.";
        }
    }
}
