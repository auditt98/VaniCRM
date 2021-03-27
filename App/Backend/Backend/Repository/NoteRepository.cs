using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class NoteRepository
    {
        DatabaseContext db = new DatabaseContext();
        public NOTE Create(NoteApiModel apiModel)
        {
            var newNote = new NOTE();
            newNote.NoteBody = apiModel.body;
            if(apiModel.account != 0)
            {
                newNote.ACCOUNT_ID = apiModel.account;
            }
            if(apiModel.campaign != 0)
            {
                newNote.CAMPAIGN_ID = apiModel.campaign;
            }
            if(apiModel.contact != 0)
            {
                newNote.CONTACT_ID = apiModel.contact;
            }
            if(apiModel.deal != 0)
            {
                newNote.DEAL_ID = apiModel.deal;
            }
            if(apiModel.lead != 0)
            {
                newNote.LEAD_ID = apiModel.lead;
            }
            if(apiModel.taskTemplate != 0)
            {
                newNote.TASK_TEMPLATE_ID = apiModel.taskTemplate;
            }
            newNote.CreatedAt = DateTime.Now;
            newNote.CreatedBy = apiModel.createdBy.id.GetValueOrDefault();

            db.NOTEs.Add(newNote);
            db.SaveChanges();
            return newNote;
        }
    
        public NOTE GetOne(int id)
        {
            return db.NOTEs.Find(id);
        }

        public bool AddFile(int id, FileManager.File file)
        {
            var dbNote = db.NOTEs.Find(id);
            if (dbNote != null)
            {
                //create a file
                var newFile = new FILE();
                string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
                file.Rename();
                file.Save(targetFolder);
                newFile.FileName = file.FullName;
                var size = ByteSize.FromBytes(file.ContentLengthBytes);
                newFile.FileSize = Convert.ToInt32(size.MegaBytes);
                newFile.FileStorePath = Path.Combine(targetFolder, file.FullName);
                //link file
                newFile.NOTE_ID = dbNote.ID;
                db.FILEs.Add(newFile);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public USER FindOwner(int id)
        {
            var dbNote = db.NOTEs.Find(id);
            if(dbNote != null)
            {
                return dbNote.USER;
            }
            else
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            var dbNote = db.NOTEs.Find(id);
            if (dbNote != null)
            {
                db.NOTEs.Remove(dbNote);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}