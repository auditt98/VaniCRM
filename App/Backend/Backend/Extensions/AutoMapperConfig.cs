using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Mappers;
using Backend.Models;
using Backend.Domain;

namespace Backend.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ACCOUNT, Account>();
            CreateMap<Account, ACCOUNT>();
            CreateMap<CALL, Call>();
            CreateMap<Call, CALL>();
            CreateMap<CONTACT, Contact>();
            CreateMap<Contact, CONTACT>();
            CreateMap<DEAL, Deal>();
            CreateMap<Deal, DEAL>();
            CreateMap<FILE, File>();
            CreateMap<File, FILE>();
            CreateMap<GROUP, Group>();
            CreateMap<Group, GROUP>();
            CreateMap<INDUSTRY, Industry>();
            CreateMap<Industry, INDUSTRY>();
            CreateMap<LEAD, Lead>();
            CreateMap<Lead, LEAD>();
            CreateMap<MEETING, Meeting>();
            CreateMap<Meeting, MEETING>();
            CreateMap<NOTE, Note>();
            CreateMap<Note, NOTE>();
            CreateMap<PERMISSION, Permission>();
            CreateMap<Permission, PERMISSION>();
            CreateMap<PERMISSION_ORDER, PermissionOrder>();
            CreateMap<PermissionOrder, PERMISSION_ORDER>();
            CreateMap<STAGE, Stage>();
            CreateMap<Stage, STAGE>();
            CreateMap<STAGE_HISTORY, StageHistory>();
            CreateMap<StageHistory, STAGE_HISTORY>();
            CreateMap<TAG, Tag>();
            CreateMap<Tag, TAG>();
            CreateMap<TAG_ITEM, TagItem>();
            CreateMap<TagItem, TAG_ITEM>();
            CreateMap<TASK, Task>();
            CreateMap<Task, TASK>();
            CreateMap<TASK_STATUS, TaskStatus>();
            CreateMap<TaskStatus, TASK_STATUS>();
            CreateMap<TASK_TEMPLATE, TaskTemplate>();
            CreateMap<TaskTemplate, TASK_TEMPLATE>();
            CreateMap<USER, User>();
            CreateMap<User, USER>();
        }
    }
}