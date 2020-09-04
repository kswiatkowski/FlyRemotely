using System;

namespace FlyRemotely.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Salary { get; set; }
        public string Requirements { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyPhotoSource { get; set; }
        public string ApplicationEmail { get; set; }

        public bool Featured { get; set; }
        public DateTime DateAdded { get; set; }
        public OfferStatus Status { get; set; }

        public virtual Category Category { get; set; }
    }

    public enum OfferStatus
    {
        Oczekuje,
        Aktywne,
        Nieaktywne,
        Zablokowane
    }
}