namespace Backend.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<ACCOUNT_TYPE> ACCOUNT_TYPE { get; set; }
        public virtual DbSet<CALL> CALLs { get; set; }
        public virtual DbSet<CALL_REASON> CALL_REASON { get; set; }
        public virtual DbSet<CALL_RESULT> CALL_RESULT { get; set; }
        public virtual DbSet<CALL_TYPE> CALL_TYPE { get; set; }
        public virtual DbSet<CAMPAIGN> CAMPAIGNs { get; set; }
        public virtual DbSet<CAMPAIGN_STATUS> CAMPAIGN_STATUS { get; set; }
        public virtual DbSet<CAMPAIGN_TARGET> CAMPAIGN_TARGET { get; set; }
        public virtual DbSet<CAMPAIGN_TYPE> CAMPAIGN_TYPE { get; set; }
        public virtual DbSet<COMPETITOR> COMPETITORs { get; set; }
        public virtual DbSet<CONTACT> CONTACTs { get; set; }
        public virtual DbSet<DEAL> DEALs { get; set; }
        public virtual DbSet<DEAL_COMPETITOR> DEAL_COMPETITOR { get; set; }
        public virtual DbSet<FILE> FILEs { get; set; }
        public virtual DbSet<GROUP> GROUPs { get; set; }
        public virtual DbSet<INDUSTRY> INDUSTRies { get; set; }
        public virtual DbSet<LEAD> LEADs { get; set; }
        public virtual DbSet<LEAD_SOURCE> LEAD_SOURCE { get; set; }
        public virtual DbSet<LEAD_STATUS> LEAD_STATUS { get; set; }
        public virtual DbSet<LOST_REASON> LOST_REASON { get; set; }
        public virtual DbSet<MEETING> MEETINGs { get; set; }
        public virtual DbSet<MEETING_PARTICIPANT> MEETING_PARTICIPANT { get; set; }
        public virtual DbSet<NOTE> NOTEs { get; set; }
        public virtual DbSet<NOTIFICATION> NOTIFICATIONs { get; set; }
        public virtual DbSet<PERMISSION> PERMISSIONs { get; set; }
        public virtual DbSet<PERMISSION_ORDER> PERMISSION_ORDER { get; set; }
        public virtual DbSet<PRIORITY> PRIORITies { get; set; }
        public virtual DbSet<REFRESH_TOKEN> REFRESH_TOKEN { get; set; }
        public virtual DbSet<STAGE> STAGEs { get; set; }
        public virtual DbSet<STAGE_HISTORY> STAGE_HISTORY { get; set; }
        public virtual DbSet<TAG> TAGs { get; set; }
        public virtual DbSet<TAG_ITEM> TAG_ITEM { get; set; }
        public virtual DbSet<TASK> TASKs { get; set; }
        public virtual DbSet<TASK_STATUS> TASK_STATUS { get; set; }
        public virtual DbSet<TASK_TEMPLATE> TASK_TEMPLATE { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<USER_NOTIFICATION> USER_NOTIFICATION { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .Property(e => e.BankAccount)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.RELATED_ACCOUNT);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.CONTACTs)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.ACCOUNT_ID);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.DEALs)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.ACCOUNT_ID);

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.ACCOUNT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.ACCOUNT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ACCOUNT>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.ACCOUNT)
                .HasForeignKey(e => e.RELATED_ACCOUNT);

            modelBuilder.Entity<ACCOUNT_TYPE>()
                .HasMany(e => e.ACCOUNTs)
                .WithOptional(e => e.ACCOUNT_TYPE)
                .HasForeignKey(e => e.ACCOUNT_TYPE_ID);

            modelBuilder.Entity<CALL>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.CALL)
                .HasForeignKey(e => e.CALL_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CALL_REASON>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.CALL_REASON)
                .HasForeignKey(e => e.CALLREASON_ID);

            modelBuilder.Entity<CALL_RESULT>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.CALL_RESULT)
                .HasForeignKey(e => e.CALLRESULT_ID);

            modelBuilder.Entity<CALL_TYPE>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.CALL_TYPE)
                .HasForeignKey(e => e.CALLTYPE_ID);

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.RELATED_CAMPAIGN);

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.CAMPAIGN_TARGET)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.CAMPAIGN_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.DEALs)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.CAMPAIGN_ID);

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.CAMPAIGN_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.CAMPAIGN_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CAMPAIGN>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.CAMPAIGN)
                .HasForeignKey(e => e.RELATED_CAMPAIGN);

            modelBuilder.Entity<CAMPAIGN_STATUS>()
                .HasMany(e => e.CAMPAIGNs)
                .WithOptional(e => e.CAMPAIGN_STATUS)
                .HasForeignKey(e => e.CAMPAIGN_STATUS_ID);

            modelBuilder.Entity<CAMPAIGN_TYPE>()
                .HasMany(e => e.CAMPAIGNs)
                .WithOptional(e => e.CAMPAIGN_TYPE)
                .HasForeignKey(e => e.CAMPAIGN_TYPE_ID);

            modelBuilder.Entity<COMPETITOR>()
                .HasMany(e => e.DEAL_COMPETITOR)
                .WithRequired(e => e.COMPETITOR)
                .HasForeignKey(e => e.COMPETITOR_ID);

            modelBuilder.Entity<CONTACT>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACT>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACT>()
                .Property(e => e.AssistantPhone)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID);

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.CAMPAIGN_TARGET)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.DEALs)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.Contact_ID);

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.MEETING_PARTICIPANT)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTACT>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.CONTACT)
                .HasForeignKey(e => e.CONTACT_ID);

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.DEAL)
                .HasForeignKey(e => e.RELATED_DEAL);

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.DEAL_COMPETITOR)
                .WithRequired(e => e.DEAL)
                .HasForeignKey(e => e.DEAL_ID);

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.DEAL)
                .HasForeignKey(e => e.DEAL_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.STAGE_HISTORY)
                .WithRequired(e => e.DEAL)
                .HasForeignKey(e => e.DEAL_ID);

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.DEAL)
                .HasForeignKey(e => e.DEAL_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DEAL>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.DEAL)
                .HasForeignKey(e => e.RELATED_DEAL);

            modelBuilder.Entity<GROUP>()
                .HasMany(e => e.USERs)
                .WithOptional(e => e.GROUP)
                .HasForeignKey(e => e.GROUP_ID);

            modelBuilder.Entity<GROUP>()
                .HasMany(e => e.PERMISSIONs)
                .WithMany(e => e.GROUPs)
                .Map(m => m.ToTable("GROUP_PERMISSION"));

            modelBuilder.Entity<INDUSTRY>()
                .HasMany(e => e.ACCOUNTs)
                .WithOptional(e => e.INDUSTRY)
                .HasForeignKey(e => e.INDUSTRY_ID);

            modelBuilder.Entity<INDUSTRY>()
                .HasMany(e => e.LEADs)
                .WithOptional(e => e.INDUSTRY)
                .HasForeignKey(e => e.INDUSTRY_ID);

            modelBuilder.Entity<LEAD>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<LEAD>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.ACCOUNTs)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.ConvertFrom);

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID);

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.CAMPAIGN_TARGET)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.MEETING_PARTICIPANT)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LEAD>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.LEAD)
                .HasForeignKey(e => e.LEAD_ID);

            modelBuilder.Entity<LEAD_SOURCE>()
                .HasMany(e => e.LEADs)
                .WithOptional(e => e.LEAD_SOURCE)
                .HasForeignKey(e => e.LeadSource);

            modelBuilder.Entity<LEAD_STATUS>()
                .HasMany(e => e.LEADs)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.LeadStatus);

            modelBuilder.Entity<LOST_REASON>()
                .HasMany(e => e.DEALs)
                .WithOptional(e => e.LOST_REASON)
                .HasForeignKey(e => e.LOST_REASON_ID);

            modelBuilder.Entity<MEETING>()
                .HasMany(e => e.MEETING_PARTICIPANT)
                .WithOptional(e => e.MEETING)
                .HasForeignKey(e => e.MEETING_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MEETING>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.MEETING)
                .HasForeignKey(e => e.MEETING_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NOTE>()
                .HasMany(e => e.FILEs)
                .WithOptional(e => e.NOTE)
                .HasForeignKey(e => e.NOTE_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NOTIFICATION>()
                .HasMany(e => e.USER_NOTIFICATION)
                .WithRequired(e => e.NOTIFICATION)
                .HasForeignKey(e => e.NOTIFICATION_ID);

            modelBuilder.Entity<PERMISSION_ORDER>()
                .HasMany(e => e.PERMISSIONs)
                .WithOptional(e => e.PERMISSION_ORDER)
                .HasForeignKey(e => e.PERMISSION_ORDER_ID);

            modelBuilder.Entity<PRIORITY>()
                .HasMany(e => e.CONTACTs)
                .WithOptional(e => e.PRIORITY)
                .HasForeignKey(e => e.PRIORITY_ID);

            modelBuilder.Entity<PRIORITY>()
                .HasMany(e => e.DEALs)
                .WithOptional(e => e.PRIORITY)
                .HasForeignKey(e => e.PRIORITY_ID);

            modelBuilder.Entity<PRIORITY>()
                .HasMany(e => e.LEADs)
                .WithOptional(e => e.PRIORITY)
                .HasForeignKey(e => e.PRIORITY_ID);

            modelBuilder.Entity<PRIORITY>()
                .HasMany(e => e.TASK_TEMPLATE)
                .WithOptional(e => e.PRIORITY)
                .HasForeignKey(e => e.PRIORITY_ID);

            modelBuilder.Entity<REFRESH_TOKEN>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<STAGE>()
                .HasMany(e => e.STAGE_HISTORY)
                .WithRequired(e => e.STAGE)
                .HasForeignKey(e => e.STAGE_ID);

            modelBuilder.Entity<TAG>()
                .HasMany(e => e.TAG_ITEM)
                .WithRequired(e => e.TAG)
                .HasForeignKey(e => e.TAG_ID);

            modelBuilder.Entity<TASK>()
                .HasMany(e => e.TAG_ITEM)
                .WithOptional(e => e.TASK)
                .HasForeignKey(e => e.TASK_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TASK_STATUS>()
                .HasMany(e => e.TASK_TEMPLATE)
                .WithOptional(e => e.TASK_STATUS)
                .HasForeignKey(e => e.TASK_STATUS_ID);

            modelBuilder.Entity<TASK_TEMPLATE>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.TASK_TEMPLATE)
                .HasForeignKey(e => e.TASK_TEMPLATE_ID);

            modelBuilder.Entity<TASK_TEMPLATE>()
                .HasMany(e => e.MEETINGs)
                .WithOptional(e => e.TASK_TEMPLATE)
                .HasForeignKey(e => e.TASK_TEMPLATE_ID);

            modelBuilder.Entity<TASK_TEMPLATE>()
                .HasMany(e => e.NOTEs)
                .WithOptional(e => e.TASK_TEMPLATE)
                .HasForeignKey(e => e.TASK_TEMPLATE_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TASK_TEMPLATE>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.TASK_TEMPLATE)
                .HasForeignKey(e => e.TASK_TEMPLATE_ID);

            modelBuilder.Entity<USER>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Hash)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.RememberMeToken)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.AccountsAsOwner)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.AccountOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.AccountsAsCollaborator)
                .WithOptional(e => e.Collaborator)
                .HasForeignKey(e => e.AccountCollaborator);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.AccountsCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.AccountsModified)
                .WithOptional(e => e.ModifiedUser)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CALLs)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.CallOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CampaignsAsOwner)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.CampaignOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CampaignsCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.CampaignsModified)
                .WithOptional(e => e.ModifiedUser)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ContactsAsOwner)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.ContactOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ContactsAsCollaborator)
                .WithOptional(e => e.Collaborator)
                .HasForeignKey(e => e.ContactCollaborator);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ContactsCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ContactsModified)
                .WithOptional(e => e.ModifiedUser)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.DealsCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.DealsAsOwner)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.DealOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.DealsModified)
                .WithOptional(e => e.ModifiedUser)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.LeadsCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.LeadsAsOwner)
                .WithOptional(e => e.Owner)
                .HasForeignKey(e => e.LeadOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ModifiedLeads)
                .WithOptional(e => e.ModifiedUser)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.MeetingsAsHost)
                .WithOptional(e => e.HostUser)
                .HasForeignKey(e => e.Host);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.MEETING_PARTICIPANT)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<USER>()
                .HasMany(e => e.NOTEs)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.REFRESH_TOKEN)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<USER>()
                .HasMany(e => e.STAGE_HISTORY)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete();

            modelBuilder.Entity<USER>()
                .HasMany(e => e.TASKs)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.TaskOwner);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.TaskTemplateCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.TaskTemplateModified)
                .WithOptional(e => e.ModifiedUSer)
                .HasForeignKey(e => e.ModifiedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.UsersCreated)
                .WithOptional(e => e.CreatedUser)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.USER_NOTIFICATION)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.USER_ID);
        }
    }
}
