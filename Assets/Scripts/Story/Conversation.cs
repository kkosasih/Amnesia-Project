using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {
    public List<Character> movers;
    public List<Statement> statements;
    public List<ConversationChar> charActions;
    public List<ConversationCamera> camActions;


}

public struct ConversationChar
{
    public int charIndex;
    public Vector2 moveTo;
}

public struct ConversationCamera
{
    public int zoom;
    public Vector2 moveTo;
}