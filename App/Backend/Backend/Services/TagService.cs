using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class TagService
    {
        TagRepository _tagRepository = new TagRepository();

        public List<TagApiModel> GetTagList(string query = "")
        {
            var dbTags = _tagRepository.GetAllTags(query);
            var results = dbTags.Select(c => new TagApiModel() { id = c.ID, name = c.Name }).ToList();

            return results;
        }


    }
}