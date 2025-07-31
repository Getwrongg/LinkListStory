using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class StoryNode
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public List<int> NextIds { get; set; } = new List<int>();
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

            if (current.NextIds == null || current.NextIds.Count == 0)
                return; // End of story

            int choiceCounter = 1;

            Dictionary<int, int> choiceMap = new Dictionary<int, int>();

            foreach (var x in current.NextIds)
            {
                StoryNode NodeTitle = story.Find(s => s.Id == x);
                choiceMap.Add(choiceCounter, NodeTitle.Id);
                Console.Write($"{choiceCounter} {NodeTitle.Title} \t");
                choiceCounter++;

            }

            Console.WriteLine("Choose choice!: ");
            string choice = Console.ReadLine()?.ToLower();

            if (int.TryParse(choice, out int choiceId))
            {
                if (choiceMap[choiceId] != null)
                {
                    current = story.Find(s => s.Id == choiceMap[choiceId]);
                }

            }



            if (current == null)
            {
                Console.WriteLine("Invalid choice or missing story path. The story ends here.");
                break;
            }
        }
    }
}
