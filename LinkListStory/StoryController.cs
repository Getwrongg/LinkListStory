using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LinkListStory
{
    public class StoryController
    {
        private List<StoryNode> story;
        private StoryNode current;
        private Player player;

        public StoryController(string jsonFilePath, Player player)
        {
            story = new List<StoryNode>();
            this.player = player;

            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<StoryNode> loaded = JsonSerializer.Deserialize<List<StoryNode>>(json, options);

                if (loaded != null)
                {
                    story = loaded;
                }
            }

            current = null;
            for (int i = 0; i < story.Count; i++)
            {
                if (story[i].Id == 1)
                {
                    current = story[i];
                    break;
                }
            }
        }

        public StoryNode GetCurrentNode()
        {
            return current;
        }

        public List<StoryNode> GetChoices()
        {
            List<StoryNode> choices = new List<StoryNode>();

            if (current == null || current.NextIds == null || current.NextIds.Count == 0)
                return choices;

            for (int i = 0; i < current.NextIds.Count; i++)
            {
                int id = current.NextIds[i];
                for (int j = 0; j < story.Count; j++)
                {
                    if (story[j].Id == id)
                    {
                        choices.Add(story[j]);
                        break;
                    }
                }
            }

            return choices;
        }

        public bool MakeChoice(int choiceIndex)
        {
            List<StoryNode> choices = GetChoices();
            if (choiceIndex < 0 || choiceIndex >= choices.Count)
                return false;

            current = choices[choiceIndex];
            player.score += 1;
            return true;
        }

        public bool IsEndOfStory()
        {
            return current == null || current.NextIds == null || current.NextIds.Count == 0;
        }

        public int GetPlayerScore()
        {
            return player.score;
        }

        public string GetPlayerName()
        {
            return player.name ?? "Unknown";
        }
    }
}
