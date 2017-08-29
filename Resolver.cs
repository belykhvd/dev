using System;
using System.Collections.Generic;

namespace Resolver
{
    public class Resolver /*Singleton (anti-)pattern ^^)*/
    {
        private class HandHandleCases
        {
            public List<object> EmployeeNoDocument { get; set; } // tree -> list
            public List<object> InconsistentFullNames { get; set; } // branch -> out
            public List<object> WarrantPersonBadDocument { get; set; } // tree -> list
        }

        private class Backup
        {
            public List<object> WarrantPersons { get; set; } // backup
            public List<object> Employees { get; set; } // backup
        }

        private class ResolveResult
        {
            public List<object> ResolvedOneVariant { get; set; } // employeeGuid + issuerCode
            public List<object> ResolvedMostFrequent { get; set; } // eG + iC
            public List<object> ResolvedByFrequencyRule { get; set; } // eG + iC
            public List<object> Collisions { get; set; } // eG + variants + additional info
        }

        private readonly HandHandleCases handHandle;
        private readonly Backup backup;
        private readonly ResolveResult resolveResult;

        private Resolver(string commonPrefix = @"logs\")
        {
            var handHandleHelper = new BackupHelper(commonPrefix + nameof(HandHandleCases));
            handHandle = (HandHandleCases)handHandleHelper.CreateBackupInstance(new HandHandleCases().GetType(), BackupHelper.SerializationType.JsonSerializable);

            var backupHelper = new BackupHelper(commonPrefix + nameof(Backup));            
            backup = (Backup)backupHelper.CreateBackupInstance(new Backup().GetType(), BackupHelper.SerializationType.JsonSerializable);
            
            var resolveResultHelper = new BackupHelper(commonPrefix + nameof(ResolveResult));
            resolveResult = (ResolveResult)resolveResultHelper.CreateBackupInstance(new ResolveResult().GetType(), BackupHelper.SerializationType.Inline);
        }

        public void Resolve(IEnumerable<Tuple<WarrantPerson, Employee>> sendersPairs)
        {

        }

        private class WarrantPerson
        {

        }

        private class Employee
        {

        }
    }
}
