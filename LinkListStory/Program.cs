using System;
using System.Collections.Generic;
using LinkListStory;

class Program
{
    static void Main()
    {
        Console.Write("Enter your name: ");
        string? name = Console.ReadLine();

        Player player = new Player { name = name, score = 0 };
        StoryController controller = new StoryController("story.json", player);

        while (!controller.IsEndOfStory())
        {
            StoryNode current = controller.GetCurrentNode();
            if (current == null)
            {
                Console.WriteLine("Story could not continue.");
                break;
            }

            Console.WriteLine();
            Console.WriteLine(current.Text);

            List<StoryNode> choices = controller.GetChoices();
            if (choices.Count == 0)
            {
                Console.WriteLine("\n[End of Story]");
                break;
            }

            Console.WriteLine();
            for (int i = 0; i < choices.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + choices[i].Title);
            }

            Console.Write("\nEnter your choice (number): ");
            string input = Console.ReadLine();

            int selected;
            if (int.TryParse(input, out selected) && selected > 0 && selected <= choices.Count)
            {
                bool moved = controller.MakeChoice(selected - 1);
                if (!moved)
                {
                    Console.WriteLine("Invalid choice.");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
                break;
            }
        }

        Console.WriteLine($"\nYou Died!, {player.name}! Your score: {player.score}");
    }
}
