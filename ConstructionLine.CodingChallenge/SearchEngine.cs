using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            var filterForShirts = _shirts.Where(x =>
                (!options.Colors.Any() || options.Colors.Any(y => y == x.Color)) 
                && (!options.Sizes.Any() || options.Sizes.Any(y => y == x.Size)))
                    .ToList();

            var sizeCounts = GetSizeCounts(filterForShirts);
            var colorCounts = GetColorCounts(filterForShirts);

            return new SearchResults
            {
                Shirts = filterForShirts,
                SizeCounts = sizeCounts,
                ColorCounts = colorCounts
            };
        }

        private static List<ColorCount> GetColorCounts(List<Shirt> filterForShirts)
        {
            var colorCountList = new List<ColorCount>();
            foreach (var color in Color.All)
            {
                var colorCount = new ColorCount
                    { Count = filterForShirts.Count(x => x.Color.Equals(color)), Color = color };
                colorCountList.Add(colorCount);
            }
            return colorCountList;
        }

        private static List<SizeCount> GetSizeCounts(List<Shirt> filterForShirts)
        {
            var sizeCountList = new List<SizeCount>();
            foreach (var size in Size.All)
            {
                var sizeCount = new SizeCount { Count = filterForShirts.Count(x => x.Size.Equals(size)), Size = size };
                sizeCountList.Add(sizeCount);
            }
            return sizeCountList;
        }
    }
}