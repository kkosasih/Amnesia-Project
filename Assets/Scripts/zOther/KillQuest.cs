public class KillQuest : QuestTracking
{
    List<string> items;
    List<string> objectives;
    private int killamount = 0;

    public KillQuest(string newDescription, string newObjective, int ka)
    {
        description = newDescription;
        objective = newObjective;
    }

    public void MainQuest2()
    {
        mainquest.Add(new KillQuest("Defend against the monsters.", "Monsters are attacking you.", 5));
        items = new List<string>();
        items.Add("vest");
        mainquestloot.Add(items);
    }
}
