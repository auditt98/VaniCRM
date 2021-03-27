using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Backend.Extensions.FileManager;

namespace Backend.Services
{
    public class NoteService
    {
        NoteRepository _noteRepository = new NoteRepository();
        FileRepository _fileRepository = new FileRepository();

        public int Create(NoteApiModel apiModel)
        {
            var createdNote = _noteRepository.Create(apiModel);
            if(createdNote != null)
            {
                return createdNote.ID;
            }
            else
            {
                return 0;
            }
        }

        public bool AddFile(int noteId, File file)
        {
            return _noteRepository.AddFile(noteId, file);
        }

        public int FindOwner(int id)
        {
            var dbUser = _noteRepository.FindOwner(id);
            if (dbUser != null)
            {
                return dbUser.ID;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int id)
        {
            return _noteRepository.Delete(id);
        }

    }
}