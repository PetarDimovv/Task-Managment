using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfBug;

namespace Task_Management.Tests.Models.Tests
{
    [TestClass]
    public class Costructor_Should
    {
        [TestMethod]
        public void Constructor_Should_ReturnCorrectType()
        {
            // Arrange, Act
            Bug bug = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            // Assert
            Assert.IsInstanceOfType(bug, typeof(Bug));
        }
        [TestMethod]
        public void Bug_Should_BeOfTypeITask()
        {
            // Arrange, Act
            Bug bug = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            // Assert
            Assert.IsInstanceOfType(bug, typeof(ITask));
        }

        [TestMethod]
        public void Bug_Should_BeOfTypeIBug()
        {
            // Arrange, Act
            Bug bug = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            // Assert
            Assert.IsInstanceOfType(bug, typeof(IBug));
        }
        [TestMethod]
        public void Bug_Should_BeOfTypeIAssignableTask()
        {
            // Arrange, Act
            Bug bug = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            // Assert
            Assert.IsInstanceOfType(bug, typeof(IAssignableTask));
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectName()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(title, sut.Title);
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectDescription()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(description, sut.Description);
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectListOfSteps()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.IsInstanceOfType(sut.ListOfStepsToReproduceBug, typeof(IList<string>));
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectPriority()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(sut.Priority, priority);
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectSeverity()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(sut.Severity, severity);
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectStatus()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;
            StatusBug defaultStatus = StatusBug.Active;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(sut.Status, defaultStatus);
        }

        [TestMethod]
        public void Constructor_Should_Set_CorrectID()
        {
            //Arrange
            int id = 1;
            string title = "Loggin error";
            string description = "Cannot log in,page freezes";
            string listOfSteps = "1. Open application;2.Enter username and password;3.Click the log in button";
            Priority priority = Priority.High;
            Severity severity = Severity.Minor;
            StatusBug defaultStatus = StatusBug.Active;

            //Act
            var sut = new Bug(id, title, description, listOfSteps, priority, severity);

            //Assert
            Assert.AreEqual(sut.Id, id);
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Bug(1, "Log", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_NameLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Bug(1, "Log in is very slow when you log " +
                "from your personal computer at home", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_DescriptionLenghtIsTooShort()
        {
            Assert.ThrowsException<ArgumentException>(() => new Bug(1, "Log in", new string('a', 9),
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_DescriptionLenghtIsTooLong()
        {
            Assert.ThrowsException<ArgumentException>(() => new Bug(1, "Log in ", new string('a', 501),
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor));
        }

        [TestMethod]
        public void Method_Should_ChangeStatus()
        {
            //Arrange
            Bug sut = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            //Act
            var prevStatus = sut.Status;
            sut.ChangeBugStatus(StatusBug.Fixed);

            //Assert
            Assert.AreNotEqual(prevStatus, sut.Status);
        }

        [TestMethod]
        public void Method_Should_ChangePriority()
        {
            //Arrange
            Bug sut = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            //Act
            var prevPriority = sut.Priority;
            sut.ChangeBugPriority(Priority.Low);

            //Assert
            Assert.AreNotEqual(prevPriority, sut.Status);
        }

        [TestMethod]
        public void Method_Should_ChangeSeverity()
        {
            //Arrange
            Bug sut = new Bug
                (1, "Loggin error", "Cannot log in,page freezes",
                "1. Open application;2.Enter username and password;3.Click the log in button",
                Priority.High, Severity.Minor);

            //Act
            var prevSeverity = sut.Severity;
            sut.ChangeBugSeverity(Severity.Critical);

            //Assert
            Assert.AreNotEqual(prevSeverity, sut.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Method_Should_Throw_WhenStatusAlreadyAtActive()
        {
            //Arrange
            Bug sut = new Bug
               (1, "Loggin error", "Cannot log in,page freezes",
               "1. Open application;2.Enter username and password;3.Click the log in button",
               Priority.High, Severity.Minor);

            //Act
            sut.ChangeBugStatus(StatusBug.Active);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Method_Should_Throw_WhenPriorityIsAlreadyAt()
        {
            //Arrange
            Bug sut = new Bug
               (1, "Loggin error", "Cannot log in,page freezes",
               "1. Open application;2.Enter username and password;3.Click the log in button",
               Priority.High, Severity.Minor);

            //Act
            sut.ChangeBugPriority(Priority.High);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Method_Should_Throw_WhenSeverityIsAlreadyAt()
        {
            //Arrange
            Bug sut = new Bug
               (1, "Loggin error", "Cannot log in,page freezes",
               "1. Open application;2.Enter username and password;3.Click the log in button",
               Priority.High, Severity.Minor);

            //Act
            sut.ChangeBugSeverity(Severity.Minor);
        }

    }
}
