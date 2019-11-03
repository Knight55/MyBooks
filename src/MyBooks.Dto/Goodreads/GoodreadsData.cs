using System;
using System.Xml.Serialization;

namespace MyBooks.Dto.Goodreads
{
    [Serializable]
    [XmlRoot("GoodreadsResponse")]
    public class GoodreadsResponse
    {
        [XmlElement("search", typeof(SearchResult))]
        public SearchResult SearchResult { get; set; }

        [XmlElement("book", typeof(GoodreadsBook))]
        public GoodreadsBook Book { get; set; }
    }

    [Serializable]
    public class SearchResult
    {
        [XmlElement("total-results")]
        public string NumberOfResults { get; set; }

        [XmlArray("results")]
        [XmlArrayItem("work", typeof(GoodreadsWork), IsNullable = false)]
        public GoodreadsWork[] Works { get; set; }
    }

    [Serializable]
    public class GoodreadsWork
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
        public GoodreadsBook Book { get; set; }
    }

    [Serializable]
    public class GoodreadsBook
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        public string Isbn { get; set; }

        [XmlElement("publication_year")]
        public string YearOfPublication { get; set; }

        [XmlElement("publication_month")]
        public string MonthOfPublication { get; set; }

        [XmlElement("publication_day")]
        public string DayOfPublication { get; set; }

        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("image_url")]
        public string ImageUrl { get; set; }

        [XmlElement("small_image_url")]
        public string SmallImageUrl { get; set; }

        [XmlArray("authors")]
        [XmlArrayItem("author", typeof(GoodreadsAuthor), IsNullable = true)]
        public GoodreadsAuthor[] Authors { get; set; }
    }

    [Serializable]
    public class GoodreadsAuthor
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("image_url")]
        public string ImageUrl { get; set; }
    }
}