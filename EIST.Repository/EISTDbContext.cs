﻿using EIST.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace EIST.Repository
{
    public class EISTDbContext : DbContext
    {
        public EISTDbContext() : base("EISTConnStr")
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<LoginRecord> LoginRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
        public DbSet<Company> Companies  { get; set; }
        public DbSet<Project> Projects   { get; set; }
        public DbSet<AttachmentFile> AttachmentFiles    { get; set; }
        public DbSet<Issue> Issues    { get; set; }
        public DbSet<TicketAssign> TicketAssigns  { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<IssueCommentLog> IssueCommentLog { get; set; }
        public DbSet<CustomerUserProject> CustomerUserProjects { get; set; }
        public DbSet<IssueLabel> IssueLabels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasOptional(e => e.Position).WithMany(s => s.UserCollection);
        }
        public int SaveChanges(string occurrenceUserId)
        {
            if (!String.IsNullOrEmpty(occurrenceUserId))
            {
                int userId = Convert.ToInt32(occurrenceUserId);
                foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
                {
                    foreach (AuditLog x in GetAuditRecordsForChange(ent, userId))
                    {
                        this.AuditLogs.Add(x);
                    }
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        private List<AuditLog> GetAuditRecordsForChange(DbEntityEntry dbEntry, int? userId)
        {
            List<AuditLog> result = new List<AuditLog>();
            DateTime changeTime = DateTime.Now;
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault() as TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
            string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;
            if (dbEntry.State == EntityState.Added)
            {
                try
                {
                    base.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                result.Add(new AuditLog()
                {
                    AuditLogId = Guid.NewGuid(),
                    EventType = "Create",
                    TableName = tableName,
                    PrimaryKeyName = keyName,
                    PrimaryKeyValue = dbEntry.GetDatabaseValues().GetValue<object>(keyName).ToString(),
                    CreatedUser = userId,
                    UpdatedDate = changeTime,
                });
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    var originalValue = dbEntry.GetDatabaseValues().GetValue<object>(propertyName) == null ? null : dbEntry.GetDatabaseValues().GetValue<object>(propertyName).ToString();
                    var newValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString();
                    result.Add(new AuditLog()
                    {
                        AuditLogId = Guid.NewGuid(),
                        EventType = "Update",
                        TableName = tableName,
                        PrimaryKeyName = keyName,
                        PrimaryKeyValue = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                        ColumnName = propertyName,
                        OldValue = originalValue,
                        NewValue = newValue,
                        CreatedUser = userId,
                        UpdatedDate = changeTime
                    });
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                result.Add(new AuditLog()
                {
                    AuditLogId = Guid.NewGuid(),
                    EventType = "Delete",
                    TableName = tableName,
                    PrimaryKeyName = keyName,
                    PrimaryKeyValue = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                    CreatedUser = userId,
                    UpdatedDate = changeTime
                }
                );
            }
            return result;
        }
    }
}