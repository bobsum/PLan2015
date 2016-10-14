using System;

namespace Plan2015.Boxter.Import
{
    internal class BoxterImportDto
    {
        public int Id { get; set; }
        public string BoxId { get; set; }
        public string BoxIdFriendly { get; set; }
        public string Tag { get; set; }
        public string Hex { get; set; }
        public string AppMode { get; set; }
        public string AppResponse { get; set; }
        public DateTime CreateDate { get; set; }
    }
}