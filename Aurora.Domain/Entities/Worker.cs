using Aurora.Domain.ValueTypes;
using Flunt.Validations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aurora.Domain.Entities
{
    public class Worker : BaseEntity<int>
    {
        public Worker(int id, Name name, DateTime birthDate, Nin nin, Password password) : base(id)
        {
            AddNotifications(
                name.contract,
                ValidateBirthDate(birthDate),
                nin.contract,
                password.contract);

            if (Valid)
            {
                Name = name;
                BirthDate = birthDate;
                Nin = nin;
                Password = password;
            }
        }

        public Worker(int id, Name name, DateTime birthDate, Nin nin) : base(id)
        {
            AddNotifications(
                name.contract,
                ValidateBirthDate(birthDate),
                nin.contract);

            if (Valid)
            {
                Name = name;
                BirthDate = birthDate;
                Nin = nin;
            }
        }

        protected Worker() { }

        public Name Name { get; }

        public DateTime BirthDate { get; }

        public Nin Nin { get; }

        public Password Password { get; }

        public virtual IEnumerable<PpePossession> PpePossessions { get; }

        public int CalculateAge()
        {
            var age = DateTime.Now.Year - BirthDate.Year;

            if (BirthDate.Date > DateTime.Now.AddDays(-age))
                age--;

            return age;
        }

        private Contract ValidateBirthDate(DateTime birthDate) =>
            new Contract()
                .IsLowerThan(new DateTime(1946, 12, 1), birthDate, nameof(birthDate), "You are elderly.")
                .IsGreaterThan(DateTime.Now.AddYears(-16), birthDate, nameof(birthDate), "You are so young.");
    }
}

