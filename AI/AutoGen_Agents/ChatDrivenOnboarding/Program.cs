//using AutoGen.Core;
//using AutoGen.OpenAI;
//using AutoGen.OpenAI.Extension;
//using ChatDrivenOnboarding.Utile;

//namespace ChatDrivenOnboarding;

//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        var chatClient = ChatClientProvider.Create("eekelsgpt"); //This program is human-guided customer onboarding experience using OpenAI agents

//        // Create the agents
//        var onboardingPersonalInformationAgent = new OpenAIChatAgent(
//            chatClient: chatClient,
//            name: "Onboarding_Personal_Information_Agent",
//            systemMessage: """
//            You are a helpful customer onboarding agent.
//            Your job is to gather customer's name and location.
//            Do not ask for other information. Return 'TERMINATE' when you have gathered all the information.
//            """)
//            .RegisterMessageConnector();

//        var onboardingTopicPreferenceAgent = new OpenAIChatAgent(
//            chatClient: chatClient,
//            name: "Onboarding_Topic_Preference_Agent",
//            systemMessage: """
//            You are a helpful customer onboarding agent.
//            Your job is to gather customer's preferences on news topics.
//            Do not ask for other information.
//            Return 'TERMINATE' when you have gathered all the information.
//            """)
//            .RegisterMessageConnector();

//        var customerEngagementAgent = new OpenAIChatAgent(
//            chatClient: chatClient,
//            name: "Customer_Engagement_Agent",
//            systemMessage: """
//            You are a helpful customer service agent
//            here to provide fun for the customer based on the user's
//            personal information and topic preferences.
//            This could include fun facts, jokes, or interesting stories.
//            Make sure to make it engaging and fun!
//            Return 'TERMINATE' when you are done.
//            """)
//            .RegisterMessageConnector();

//        var summarizer = new OpenAIChatAgent(
//            chatClient: chatClient,
//            name: "Summarizer",
//            systemMessage: """
//            You are a helpful summarizer agent.
//            Your job is to summarize the conversation between the user and the customer service agent.
//            Return 'TERMINATE' when you are done.
//            """)
//            .RegisterMessageConnector();

//        // Step 1: Personal Info
//        var personalInfoConversation = await HumanInteractionAsync(
//            onboardingPersonalInformationAgent,
//            "Hello! I’m here to help you get started. Can you tell me your name and location?"
//        );

//        var summary = await summarizer.GenerateReplyAsync(personalInfoConversation);
//        Console.WriteLine("\nSummary after personal info:");
//        if (summary is TextMessage textSummary)
//        {
//            Console.WriteLine(textSummary.Content);
//        }

//        // Step 2: Topic Preferences
//        var topicPrefConversation = await HumanInteractionAsync(
//            onboardingTopicPreferenceAgent,
//            "Great! What topics are you interested in reading about?"
//        );

//        var topicSummaryInput = new List<IMessage> { summary! };
//        topicSummaryInput.AddRange(topicPrefConversation);
//        summary = await summarizer.GenerateReplyAsync(topicSummaryInput);
//        Console.WriteLine("\nSummary after topic preferences:");
//        if (summary is TextMessage textSummary2)
//        {
//            Console.WriteLine(textSummary2.Content);
//        }

//        // Step 3: Fun engagement
//        var funConversation = await HumanInteractionAsync(
//            customerEngagementAgent,
//            "Awesome! Let's find something fun to read!"
//        );

//        var finalSummaryInput = new List<IMessage> { summary! };
//        finalSummaryInput.AddRange(funConversation);
//        summary = await summarizer.GenerateReplyAsync(finalSummaryInput);
//        Console.WriteLine("\nFinal Summary:");
//        if (summary is TextMessage textSummary3)
//        {
//            Console.WriteLine(textSummary3.Content);
//        }
//    }

//    private static async Task<List<TextMessage>> HumanInteractionAsync(
//        IAgent agent,
//        string initialPrompt,
//        int maxRound = 3)
//    {
//        var conversation = new List<TextMessage>();

//        var message = new TextMessage(Role.Assistant, initialPrompt, from: agent.Name);
//        Console.WriteLine($"\n{agent.Name}: {message.Content}");
//        conversation.Add(message);

//        for (int i = 0; i < maxRound; i++)
//        {
//            Console.Write("You: ");
//            var userInput = Console.ReadLine();

//            if (string.IsNullOrWhiteSpace(userInput))
//                break;

//            var userMessage = new TextMessage(Role.User, userInput);
//            conversation.Add(userMessage);

//            var response = await agent.GenerateReplyAsync(conversation);
//            if (response is TextMessage agentMessage)
//            {
//                Console.WriteLine($"{agent.Name}: {agentMessage.Content}");
//                conversation.Add(agentMessage);

//                if (agentMessage.Content.Contains("TERMINATE", StringComparison.OrdinalIgnoreCase))
//                    break;
//            }
//        }

//        return conversation;
//    }
//}


using AutoGen.Core;
using AutoGen.OpenAI;
using AutoGen.OpenAI.Extension;
using ChatDrivenOnboarding.Utile;

namespace ChatDrivenOnboarding;

internal class Program
{
    static async Task Main(string[] args)
    {
        var chatClient = ChatClientProvider.Create("eekelsgpt");

        // Create the agents
        var onboardingPersonalInformationAgent = new OpenAIChatAgent(
            chatClient: chatClient,
            name: "Onboarding_Personal_Information_Agent",
            systemMessage: """
            You are a helpful customer onboarding agent,
            you are here to help new customers get started with our product.
            Your job is to gather customer's name and location.
            Do not ask for other information. Return 'TERMINATE' 
            when you have gathered all the information.
            """)
            .RegisterMessageConnector()
            .RegisterPrintMessage();

        var onboardingTopicPreferenceAgent = new OpenAIChatAgent(
            chatClient: chatClient,
            name: "Onboarding_Topic_Preference_Agent",
            systemMessage: """
            You are a helpful customer onboarding agent,
            you are here to help new customers get started with our product.
            Your job is to gather customer's preferences on news topics.
            Do not ask for other information.
            Return 'TERMINATE' when you have gathered all the information.
            """)
            .RegisterMessageConnector()
            .RegisterPrintMessage();

        var customerEngagementAgent = new OpenAIChatAgent(
            chatClient: chatClient,
            name: "Customer_Engagement_Agent",
            systemMessage: """
            You are a helpful customer service agent
            here to provide fun for the customer based on the user's
            personal information and topic preferences.
            This could include fun facts, jokes, or interesting stories.
            Make sure to make it engaging and fun!
            Return 'TERMINATE' when you are done.
            """)
            .RegisterMessageConnector()
            .RegisterPrintMessage();

        var summarizer = new OpenAIChatAgent(
            chatClient: chatClient,
            name: "Summarizer",
            systemMessage: """
            You are a helpful summarizer agent.
            Your job is to summarize the conversation between the user and the customer service agent.
            Return 'TERMINATE' when you are done.
            """)
            .RegisterMessageConnector()
            .RegisterPrintMessage();

        var user = new OpenAIChatAgent(
            chatClient: chatClient,
            name: "User",
            systemMessage: """
            Your name is Bobby and you live in Netherlands.
            You are reaching out to customer service to find out something fun.
            """)
            .RegisterMessageConnector()
            .RegisterPrintMessage();

        // Task 1: Gather personal info
        var greetingMessage = new TextMessage(Role.Assistant, """
            Hello, I'm here to help you get started with our product.
            Could you tell me your name and location?
            """, from: onboardingPersonalInformationAgent.Name);

        var conversation = await onboardingPersonalInformationAgent.SendAsync(
            receiver: user,
            new[] { greetingMessage },
            maxRound: 2)
            .ToListAsync();

        var summary = await summarizer.SendAsync("""
            Return the customer information into as JSON object only: {'name': '', 'location': ''}
            """, conversation);

        // Task 2: Get topic preferences
        var topicPreferenceMessage = new TextMessage(Role.Assistant, """
            Great! Could you tell me what topics you are interested in reading about?
            """, from: onboardingTopicPreferenceAgent.Name);

        conversation = await onboardingTopicPreferenceAgent.SendAsync(
            receiver: user,
            new[] { topicPreferenceMessage },
            maxRound: 1)
            .ToListAsync();

        summary = await summarizer.SendAsync(chatHistory: new[] { summary }.Concat(conversation));

        // Task 3: Engage with fun content
        var funFactMessage = new TextMessage(Role.User, "Let's find something fun to read.", from: user.Name);

        conversation = await user.SendAsync(
            receiver: customerEngagementAgent,
            chatHistory: conversation.Concat(new[] { funFactMessage }),
            maxRound: 1)
            .ToListAsync();

        summary = await summarizer.SendAsync(chatHistory: new[] { summary }.Concat(conversation));
    }
}