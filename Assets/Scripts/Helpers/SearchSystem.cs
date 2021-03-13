using System.Collections.Generic;
using UnityEngine;

public static class SearchSystem
{
    public static Transform FindUpByTag(Transform t, string tag)
    {
        if (tag == t.tag)
        {
            return t;
        }
        while (t.parent != null)
        {
            t = t.parent;
            if (tag == t.tag)
            {
                return t;
            }
        }
        return null;
    }

    public static Transform FindUpByTags(Transform t, List<string> tags)
    {
        if (tags.Contains(t.tag))
        {
            return t;
        }
        while (t.parent != null)
        {
            t = t.parent;
            if (tags.Contains(t.tag))
            {
                return t;
            }
        }
        return null;
    }
}
