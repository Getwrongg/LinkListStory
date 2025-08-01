using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class StoryNode
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int? NextLeft { get; set; }
    public int? NextRight { get; set; }
}

class Program
{
    static void Main()
    {
        try
        {
            string json = File.ReadAllText("story.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<StoryNode> story = JsonSerializer.Deserialize<List<StoryNode>>(json, options);

            if (story == null || story.Count == 0)
            {
                Console.WriteLine("Failed to load story or story is empty.");
                return;
            }

            PlayStory(story);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void PlayStory(List<StoryNode> story)
    {
        StoryNode current = story.Find(s => s.Id == 1); // Start node

        while (current != null)
        {
            Console.WriteLine();
            Console.WriteLine(current.Text);

            if (current.NextLeft == null && current.NextRight == null)
            {
                break; // End of story
            }

            Console.Write("Choice (left/right): ");

            string choice = Console.ReadLine()?.ToLower();

            int? nextId;

            if (choice == "left")
            {
                nextId = current.NextLeft;
            }
            else
            {
                nextId = current.NextRight;
            }

            current = story.Find(s => s.Id == nextId);

            if (current == null)
            {
                Console.WriteLine("Invalid choice or missing story path. The story ends here.");
                break;
            }
        }
    }
}
}
