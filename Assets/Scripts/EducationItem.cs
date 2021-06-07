using UnityEngine;

namespace DefaultNamespace
{
    public class EducationItem
    {
        public string Title { get; set; }
        public Categories Category { get; set; }
        public GameObject Model { get; set; }
        public string Description { get; set; }

        public EducationItem(string title, Categories category, GameObject model, string description)
        {
            Title = title;
            Category = category;
            Model = model;
            Description = description;
        }
    }

    public enum Categories
    {
        Astronomy,
        Anatotmy,
        Physics,
        Architecture
    }
}