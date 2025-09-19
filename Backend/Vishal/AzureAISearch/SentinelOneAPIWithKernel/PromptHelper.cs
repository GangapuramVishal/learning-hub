namespace SentinelOneAPIWithKernel
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

//return $@"
//                You are an expert in network security, specializing in Fortigate firewall logs, Sentinel logs, and external threat intelligence tools

//                Instructions:
//                - Provide a **complete response** to the query.
//                - Make sure your output follows this exact JSON format.
//                - If the response is too large for a single message, use a continuation approach:
//                    - Label the current response as 'Part 1', 'Part 2', etc., as needed.
//                    - Ensure all details are included across the parts.

//                Deliver the information in a structured, detailed, and readable format.";