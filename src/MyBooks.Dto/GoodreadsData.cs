using System;
using System.Xml.Serialization;

namespace MyBooks.Dto
{
    [Serializable]
    [XmlRoot("GoodreadsResponse")]
    public class Response
    {
        [XmlElement("search", typeof(SearchResult))]
        public SearchResult SearchResult { get; set; }
    }

    [Serializable]
    public class SearchResult
    {
        [XmlElement("total-results")]
        public string NumberOfResults { get; set; }

        [XmlArray("results")]
        [XmlArrayItem("work", typeof(Work), IsNullable = false)]
        public Work[] Works { get; set; }
    }

    [Serializable]
    public class Work
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("books_count")]
        public string NumberOfBooks { get; set; }

        [XmlElement("ratings_count")]
        public string NumberOfRatings { get; set; }

        [XmlElement("original_publication_year")]
        public string YearOfPublication { get; set; }

        [XmlElement("original_publication_month")]
        public string MonthOfPublication { get; set; }

        [XmlElement("original_publication_day")]
        public string DayOfPublication { get; set; }

        [XmlElement("average_rating", IsNullable = true)]
        public string AverageRating { get; set; }

        [XmlElement("best_book")]
        public Book Book { get; set; }
    }

    [Serializable]
    public class Book
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("image_url")]
        public string ImageUrl { get; set; }

        [XmlElement("small_image_url")]
        public string SmallImageUrl { get; set; }

        [XmlElement("author")]
        public Author[] Authors { get; set; }
    }

    [Serializable]
    public class Author
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}