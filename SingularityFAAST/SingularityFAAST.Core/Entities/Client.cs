using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string StateName { get; set; }

        public string StateCode { get; set; }

        public string Zip { get; set; }

        public string County { get; set; }

        public string CountyFIPS { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string CellPhone { get; set; }

        public string WorkPhone { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public bool LoanEligibility { get; set; }

        public string Notes { get; set; }

        public int ClientCategoryId { get; set; }



    }
}









//CREATE TABLE[dbo].[Dummies]
//(
//	[DummyId]
//INT NOT NULL IDENTITY,

//[Name] VARCHAR(20) NOT NULL,

//[Age] INT NOT NULL,
//	[Description] VARCHAR(20) NULL,
//	--[DummyTypeId]
//INT NOT NULL

//CONSTRAINT[PK_Dummies] PRIMARY KEY(DummyId)
//	--CONSTRAINT[FK_Dummies_DummyTypes] FOREIGN KEY([DummyTypeId])
//)