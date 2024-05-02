using Microsoft.SemanticKernel;

namespace poc.AIAghent
{
    internal class AgentFunctions
    {
        public Kernel? _kernel { get; set; }

        public AgentFunctions(Kernel? kernel)
        {
            _kernel = kernel;
        }

        public async Task GetDayOfWeek()
        {
            var result = await _kernel.InvokeAsync("TimePlugin", "DayOfWeek");

            Console.WriteLine(result);
        }

        public async Task GetItemsFromContext()
        {
            string history = @"In the heart of my bustling kitchen, I have embraced 
                the challenge of satisfying my family's diverse taste buds and 
                navigating their unique tastes. With a mix of picky eaters and 
                allergies, my culinary journey revolves around exploring a plethora 
                of vegetarian recipes.

                One of my kids is a picky eater with an aversion to anything green, 
                while another has a peanut allergy that adds an extra layer of complexity 
                to meal planning. Armed with creativity and a passion for wholesome 
                cooking, I've embarked on a flavorful adventure, discovering plant-based 
                dishes that not only please the picky palates but are also heathy and 
                delicious.";

            string functionPrompt = @"User Background: 
                {{ConversationSummaryPlugin.SummarizeConversation $history}}
                Given this user's background, provide a list of relevant recipes.";

            var suggestRecipes = _kernel.CreateFunctionFromPrompt(functionPrompt);

            var result = await _kernel.InvokeAsync(
                suggestRecipes,
                new KernelArguments
                {
                    { "history", history }
                });

                Console.WriteLine(result);
        }
    }
}
