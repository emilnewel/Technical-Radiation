using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository = new AuthorRepository();
        private NewsRepository _newsItemRepository = new NewsRepository();
        public IEnumerable<AuthorDto> GetAuthors(){
            var authors = _authorRepository.GetAuthors().ToList();
            authors.ForEach(a => {
                a.Links.AddReference("self", $"/api/authors/{a.Id}");
                a.Links.AddReference("edit", $"/api/authors/{a.Id}");
                a.Links.AddReference("delete", $"/api/authors/{a.Id}");
                a.Links.AddReference("newsItems", $"/api/authors/{a.Id}/newsItems");
                //Vantar listann getur þú fundið út úr því ?
                //þarft að nota a.Links.AddReferenceList fallið : https://github.com/arnarleifs/WebServices2019/blob/master/net-core/RentAWorld/RentAWorld.Repositories/OwnerRepository.cs
                //Þarft lika að utfæra getNewsItemsByAuthorId þar sem þú þarft að tengja 3 Entity saman YAY
            });

            return authors;
        }
        public IEnumerable<AuthorDto> GetAuthorsById(int Id) => _authorRepository.GetAuthorsById(Id);
        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int Id) => _authorRepository.GetNewsByAuthorId(Id);
   }
}